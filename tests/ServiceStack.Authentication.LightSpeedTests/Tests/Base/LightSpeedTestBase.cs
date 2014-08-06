// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedTestBase.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System;
    using System.IO;

    using Mindscape.LightSpeed;

    using NUnit.Framework;

    using ServiceStack.Authentication.LightSpeed;
    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.Sqlite;
    using ServiceStack.Text;

    /// <summary>
    /// The LightSpeed test base class.
    /// </summary>
    public class LightSpeedTestBase
    {
        /// <summary>
        /// The test email domain.
        /// </summary>
        public const string EmailDomain = "test.com";

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        public static string DbConnStr { get; private set; }

        #region LightSpeed session
        /// <summary>
        /// Gets the database connection context.
        /// </summary>
        public static LightSpeedContext<UserAuthModelUnitOfWork> AuthContext { get; private set; }

        /// <summary>
        /// Gets the unit of work scope.
        /// </summary>
        public static SimpleUnitOfWorkScope<UserAuthModelUnitOfWork> AuthScope { get; private set; }
        #endregion

        /// <summary>
        /// Gets or sets the LightSpeed repository.
        /// </summary>
        public LightSpeedUserAuthRepository LightSpeedRepository { get; set; }

        /// <summary>
        /// Gets the unit of work.
        /// </summary>
        public UserAuthModelUnitOfWork UnitOfWork
        {
            get { return AuthScope.Current; }
        }

        /// <summary>
        /// Test fixture setup.
        /// Run once before all tests in the fixture is run.
        /// </summary>
        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            // Initiate database connection, and
            // refresh the tables to a blank slate.
            CreateDbContext();
            DropAndCreateTables();
        }

        /// <summary>
        /// Test setup.
        /// Runs everytime before each test is run.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            OpenDbConn();
        }

        /// <summary>
        /// Test fixture teardown.
        /// Run once after all tests in the fixture finished running.
        /// </summary>
        [TestFixtureTearDown]
        public void FixtureTearDown()
        {
        }

        /// <summary>
        /// Test tear down.
        /// Runs everytime after each test is finished.
        /// </summary>
        [TearDown]
        public void TestTearDown()
        {
            if (AuthScope.HasCurrent)
            {
                AuthScope.Dispose();
            }
        }

        /// <summary>
        /// Create a new user using LightSpeedAuthRepository.
        /// </summary>
        /// <param name="username">The username.</param>
        public void CreateUserWithLightSpeed(string username)
        {
            this.LightSpeedRepository.CreateUserAuth(
                new Auth.UserAuth
                {
                    UserName = username,
                    Email = string.Format("{0}@{1}", username, EmailDomain)
                },
                "Abc!123");
        }

        /// <summary>
        /// Create a new user registration.
        /// </summary>
        /// <param name="autoLogin">The auto login.</param>
        /// <returns>The <see cref="Register"/>.</returns>
        public static Register CreateNewUserRegistration(bool? autoLogin = null)
        {
            var userId = Environment.TickCount % 10000;
            var newUserRegistration =
                new Register
                    {
                        UserName = "UserName" + userId,
                        DisplayName = "DisplayName" + userId,
                        Email = string.Format("user{0}@{1}", userId, EmailDomain),
                        FirstName = "FirstName" + userId,
                        LastName = "LastName" + userId,
                        Password = "Password" + userId,
                        AutoLogin = autoLogin,
                    };

            return newUserRegistration;
        }

        /// <summary>
        /// Create the database connection context.
        /// </summary>
        private static void CreateDbContext()
        {
            DbConnStr =
                string.Format(
                    "Data Source={0};Version=3;",
                    Path.GetFullPath(string.Format("{0}/Data/ss_auth.sqlite", TestContext.CurrentContext.WorkDirectory)));
            
            AuthContext =
                new LightSpeedContext<UserAuthModelUnitOfWork>
                {
                    ConnectionString = DbConnStr,
                    DataProvider = DataProvider.SQLite3,
                    UnitOfWorkFactory = new UserAuthModelUnitOfWorkFactory(new JsvStringSerializer()),
                    IdentityMethod = IdentityMethod.IdentityColumn
                };
        }

        /// <summary>
        /// Open a new database connection.
        /// </summary>
        private static void OpenDbConn()
        {
            // LightSpeed UnitOfWork
            AuthScope = new SimpleUnitOfWorkScope<UserAuthModelUnitOfWork>(AuthContext);
        }

        /// <summary>
        /// Drop and recreate ServiceStack auth tables.
        /// </summary>
        private static void DropAndCreateTables()
        {
            var factory =
                new OrmLiteConnectionFactory(
                    DbConnStr,
                    new SqliteOrmLiteDialectProvider());

            using (var conn = factory.Open())
            {
                conn.DropTable<ServiceStack.Auth.UserAuthRole>();
                conn.DropTable<ServiceStack.Auth.UserAuthDetails>();
                conn.DropTable<ServiceStack.Auth.UserAuth>();

                conn.CreateTable<ServiceStack.Auth.UserAuth>();
                conn.CreateTable<ServiceStack.Auth.UserAuthDetails>();
                conn.CreateTable<ServiceStack.Auth.UserAuthRole>();
            }
        }
    }
}
