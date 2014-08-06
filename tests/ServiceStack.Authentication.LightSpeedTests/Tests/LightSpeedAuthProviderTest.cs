// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedAuthProviderTest.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Linq;
    using System.Net;

    using Mindscape.LightSpeed;

    using NUnit.Framework;

    using ServiceStack.Auth;
    using ServiceStack.Authentication.LightSpeed;
    using ServiceStack.Testing;

    /// <summary>
    /// The LightSpeed ORM auth provider test.
    /// </summary>
    /// <remarks>
    /// See: https://github.com/ServiceStack/ServiceStack/blob/master/tests/ServiceStack.Common.Tests/OAuth/OrmLiteUserAuthRepositoryTests.cs
    /// </remarks>
    [TestFixture]
    public class LightSpeedAuthProviderTest
        : LightSpeedTestBase
    {
        /// <summary>
        /// The default password.
        /// </summary>
        private const string DefaultPassword = "P@55word";

        /// <summary>
        /// Gets or sets the ServiceStack app host.
        /// </summary>
        private ServiceStackHost AppHost { get; set; }

        /// <summary>
        /// The local test set up.
        /// </summary>
        [SetUp]
        public void LocalFixtureSetUp()
        {
            // Configure the basic app host and start it
            this.AppHost = new BasicAppHost
            {
                ConfigureAppHost = host =>
                {
                    host.Plugins.Add(
                        new AuthFeature(() =>
                            new AuthUserSession(),
                            new IAuthProvider[] {  
                                new CredentialsAuthProvider() 
                    }));
                },
                ConfigureContainer = container =>
                {
                    container.Register<IUnitOfWork>(c => this.UnitOfWork);
                    container.Register<IAuthRepository>(c =>
                        new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>()));
                }
            }.Init();
        }

        /// <summary>
        /// The local test teardown.
        /// </summary>
        [TearDown]
        public void LocalFixtureTearDown()
        {
            // Shut down the app host
            this.AppHost.Dispose();
        }

        /// <summary>
        /// Check for multiple invalid login attempts without being locked out.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="attempts">The number of attempts.</param>
        [TestCase("invalidLogin1", 3)]
        [TestCase("invalidLogin2", 5)]
        [TestCase("invalidLogin3", 10)]
        public void CanAttemptMultipleInvalidLoginsWithoutBeingLockedOut(string username, int attempts = 3)
        {
            // Arrange
            this.RegisterUser(username);
            var email = string.Format("{0}@{1}", username, EmailDomain);
            
            // Act
            attempts.Times(() =>
            {
                this.AppHost.ExecuteService(
                    new Authenticate
                        {
                            UserName = username,
                            Password = "wrongpassword"
                        });
            });

            // Assert
            var user =
                this.UnitOfWork.UserAuths
                    .First(q => q.Email == email);
            Assert.That(user.LockedDate, Is.Null);
        }

        /// <summary>
        /// Check if user account is locked out after reaching max invalid logins limit.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="attempts">The number of attempts.</param>
        [TestCase("lockedLogin1", 3)]
        [TestCase("lockedLogin2", 5)]
        [TestCase("lockedLogin3", 10)]
        public void DoesLockoutUserAfterReachingMaxInvalidLoginsLimit(string username, int attempts = 3)
        {
            // Arrange
            LightSpeed.UserAuth user;
            this.RegisterUser(username);
            var email = string.Format("{0}@{1}", username, EmailDomain);
            var authRepo = (LightSpeedUserAuthRepository)this.AppHost.Resolve<IAuthRepository>();
            authRepo.MaxLoginAttempts = attempts;

            // Act
            attempts.Times(i =>
            {
                this.AppHost.ExecuteService(
                    new Authenticate
                    {
                        UserName = username,
                        Password = "wrongpassword"
                    });

                user = this.UnitOfWork.UserAuths.First(q => q.Email == email);

                // Assert #1:
                // Check that last login attempt and invalid login attempt count is updated accordingly
                Assert.That(user.LastLoginAttempt, Is.Not.Null);
                Assert.That(user.InvalidLoginAttempts, Is.EqualTo(i + 1)); // Zero-based index
            });

            // Assert #2:
            // Check that user account lock date is populated accordingly
            user = this.UnitOfWork.UserAuths.First(q => q.Email == email);
            Assert.That(user.LockedDate, Is.Not.Null);

            // Assert #3:
            // Check that subsequent login attempts will return HTTP 401 Unauthorized due to account lockout
            var authenticateResponse =
                this.AppHost.ExecuteService(
                    new Authenticate
                        {
                            UserName = username,
                            Password = DefaultPassword
                        });
            var errorResponse = (HttpError)authenticateResponse;
            Assert.That(errorResponse.Message, Is.EqualTo("This account has been locked"));
            Assert.That(errorResponse.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
        }


        /// <summary>
        /// Register a new user via the service.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The <see cref="object"/>.</returns>
        private object RegisterUser(string username)
        {
            // Arrange
            var userToRemove = this.UnitOfWork.UserAuths.FirstOrDefault(q => q.Email == username);
            if (userToRemove != null)
            {
                this.UnitOfWork.Remove(userToRemove);
                this.UnitOfWork.SaveChanges();
            }

            // Act
            var registrationResponse =
                this.AppHost.ExecuteService(
                    new Register
                        {
                            UserName = username,
                            Password = DefaultPassword,
                            Email = string.Format("{0}@{1}", username, EmailDomain),
                            DisplayName = "DisplayName",
                            FirstName = "FirstName",
                            LastName = "LastName",
                        });
            
            // Assert
            Assert.That(
                registrationResponse as RegisterResponse,
                Is.Not.Null,
                registrationResponse.ToString());
            
            return registrationResponse;
        }
    }
}
