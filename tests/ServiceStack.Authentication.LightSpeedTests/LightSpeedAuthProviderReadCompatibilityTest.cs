// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedAuthProviderReadCompatibilityTest.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Collections.ObjectModel;
    using System.Data;
    using System.IO;

    using Mindscape.LightSpeed;

    using NUnit.Framework;

    using ServiceStack.Auth;
    using ServiceStack.Authentication.LightSpeed;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.Sqlite;
    using ServiceStack.Text;

    /// <summary>
    /// The LightSpeed ORM auth provider compatibility with OrmLite auth provider test.
    /// </summary>
    [TestFixture]
    public class LightSpeedAuthProviderReadCompatibilityTest
    {
        /// <summary>
        /// The test email domain.
        /// </summary>
        private const string EmailDomain = "@test.com";

        #region OrmLite session
        /// <summary>
        /// The database connection.
        /// </summary>
        private static IDbConnection dbConn;

        /// <summary>
        /// The database connection string.
        /// </summary>
        private static string dbConnStr;

        /// <summary>
        /// The database connection factory.
        /// </summary>
        private static IDbConnectionFactory dbFactory;
        #endregion

        #region LightSpeed session
        /// <summary>
        /// The database connection context.
        /// </summary>
        private static LightSpeedContext<UserAuthModelUnitOfWork> authContext;

        /// <summary>
        /// The unit of work scope.
        /// </summary>
        private static PerThreadUnitOfWorkScope<UserAuthModelUnitOfWork> authScope;
        #endregion

        /// <summary>
        /// Gets or sets the OrmLite repository.
        /// </summary>
        private OrmLiteAuthRepository OrmLiteRepository { get; set; }

        /// <summary>
        /// Gets or sets the LightSpeed repository.
        /// </summary>
        private LightSpeedUserAuthRepository LightSpeedRepository { get; set; }

        /// <summary>
        /// The setup.
        /// </summary>
        [TestFixtureSetUp]
        public void Setup()
        {
            // Initiate database connection,
            // refresh the tables to a blank slate,
            // and initialise the repositories
            InitDbConn();
            DropAndCreateTables();
            this.InitRepositories();
        }

        /// <summary>
        /// Check LightSpeed read compatibility for OrmLite-created entries.
        /// </summary>
        /// <param name="username">The username.</param>
        [TestCase("getByUsername1")]
        [TestCase("getByUsername2")]
        [TestCase("getByUsername3")]
        public void CheckReadGetByUsername(string username)
        {
            // Arrange
            this.CreateUserWithOrmLite(username);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed read compatibility for OrmLite-created entries.
        /// </summary>
        /// <param name="username">The username.</param>
        [TestCase("getByEmail1")]
        [TestCase("getByEmail2")]
        [TestCase("getByEmail3")]
        public void CheckReadGetByEmail(string username)
        {
            // Arrange
            this.CreateUserWithOrmLite(username);
            var email = string.Format("{0}@{1}", username, EmailDomain);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(email);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(email);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed read assigned user role(s).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        [TestCase("getRole1", "SuperAdmin")]
        [TestCase("getRole2", "Marketing")]
        public void CheckReadAssignRole(string username, string role)
        {
            // Arrange
            this.CreateUserWithOrmLite(username);
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            this.OrmLiteRepository.AssignRoles(ormLiteUser, roles: new Collection<string> { role });

            // Act
            var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);
            var hasRole = lightspeedUser.Roles.Contains(role);

            // Assert
            Assert.IsTrue(hasRole);
        }

        /// <summary>
        /// Check LightSpeed read assigned user role(s).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="permission">The permission.</param>
        [TestCase("getPermission1", "AddItem")]
        [TestCase("getPermission2", "EditItem")]
        public void CheckReadAssignPermission(string username, string permission)
        {
            // Arrange
            this.CreateUserWithOrmLite(username);
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            this.OrmLiteRepository.AssignRoles(ormLiteUser, permissions: new Collection<string> { permission });

            // Act
            var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);
            var hasRole = lightspeedUser.Permissions.Contains(permission);

            // Assert
            Assert.IsTrue(hasRole);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            if (authScope.HasCurrent)
            {
                authScope.Dispose();
            }

            dbConn.Close();
        }

        /// <summary>
        /// Initiate database connection.
        /// </summary>
        private static void InitDbConn()
        {
            dbConnStr =
                string.Format(
                    "Data Source={0};Version=3;",
                    Path.GetFullPath(string.Format("{0}/Data/ss_auth.sqlite", TestContext.CurrentContext.WorkDirectory)));

            dbFactory = 
                new OrmLiteConnectionFactory(
                    dbConnStr,
                    new SqliteOrmLiteDialectProvider());

            dbConn = dbFactory.Open();

            authContext =
                new LightSpeedContext<UserAuthModelUnitOfWork>
                    {
                        ConnectionString = dbConnStr,
                        DataProvider = DataProvider.SQLite3,
                        UnitOfWorkFactory = new UserAuthModelUnitOfWorkFactory(new JsvStringSerializer())
                    };
            authScope = new PerThreadUnitOfWorkScope<UserAuthModelUnitOfWork>(authContext);
        }

        /// <summary>
        /// Drop and recreate ServiceStack auth tables.
        /// </summary>
        private static void DropAndCreateTables()
        {
            dbConn.DropTable<ServiceStack.Auth.UserAuthRole>();
            dbConn.DropTable<ServiceStack.Auth.UserAuthDetails>();
            dbConn.DropTable<ServiceStack.Auth.UserAuth>();

            dbConn.CreateTable<ServiceStack.Auth.UserAuth>();
            dbConn.CreateTable<ServiceStack.Auth.UserAuthDetails>();
            dbConn.CreateTable<ServiceStack.Auth.UserAuthRole>();
        }

        /// <summary>
        /// Initialise the repositories.
        /// </summary>
        private void InitRepositories()
        {
            // Init OrmLite and LightSpeed repository
            this.OrmLiteRepository = new OrmLiteAuthRepository(dbFactory);
            this.LightSpeedRepository = new LightSpeedUserAuthRepository(authScope.Current);
        }

        private void CreateUserWithOrmLite(string username)
        {
            this.OrmLiteRepository.CreateUserAuth(
                new Auth.UserAuth
                {
                    UserName = username,
                    Email = string.Format("{0}@{1}", username, EmailDomain)
                },
                "Abc!123");
        }
    }
}
