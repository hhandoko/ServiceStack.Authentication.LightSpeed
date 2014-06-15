// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedAuthProviderCompatibilityTest.cs" company="ServiceStack.Authentication.LightSpeed">
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

    /// <summary>
    /// The LightSpeed ORM auth provider compatibility with OrmLite auth provider test.
    /// </summary>
    [TestFixture]
    public class LightSpeedAuthProviderCompatibilityTest
    {
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
        private static LightSpeedContext<UserAuthModelUnitOfWork> context;

        /// <summary>
        /// The unit of work scope.
        /// </summary>
        private static PerThreadUnitOfWorkScope<UserAuthModelUnitOfWork> scope;
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
            this.SeedDbWithUsers();
        }

        /// <summary>
        /// Check LightSpeed read compatibility for OrmLite-created entries.
        /// </summary>
        [TestCase("readTestUser0001", "readtestuser0001@test.com", "Abc!123")]
        [TestCase("readTestUser0002", "readtestuser0002@test.com", "Abc!234")]
        [TestCase("readTestUser0003", "readtestuser0003@test.com", "Abc!345")]
        public void CheckReadGetByUsername(string username, string email, string password)
        {
            // Arrange
            var newUser =
                new ServiceStack.Auth.UserAuth
                    {
                        UserName = username,
                        Email = email
                    };
            this.OrmLiteRepository.CreateUserAuth(newUser, password);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed read compatibility for OrmLite-created entries.
        /// </summary>
        [TestCase("readTestUser0004", "readtestuser0004@test.com", "Abc!456")]
        [TestCase("readTestUser0005", "readtestuser0005@test.com", "Abc!567")]
        [TestCase("readTestUser0006", "readtestuser0006@test.com", "Abc!678")]
        public void CheckReadGetByEmail(string username, string email, string password)
        {
            // Arrange
            var newUser =
                new ServiceStack.Auth.UserAuth
                {
                    UserName = username,
                    Email = email
                };
            this.OrmLiteRepository.CreateUserAuth(newUser, password);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(email);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(email);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed read assigned user role(s).
        /// </summary>
        /// <param name="role">The role.</param>
        [TestCase("SuperAdmin")]
        public void CheckReadAssignRole(string role)
        {
            // Arrange
            var prepareUser = this.OrmLiteRepository.GetUserAuthByUserName("lsUser01");
            this.OrmLiteRepository.AssignRoles(prepareUser, roles: new Collection<string> { role });

            // Act
            var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName("lsUser01");
            var hasRole = lightspeedUser.Roles.Contains(role);

            // Assert
            Assert.IsTrue(hasRole);
        }

        /// <summary>
        /// The tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            if (scope.HasCurrent)
            {
                scope.Dispose();
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

            context =
                new LightSpeedContext<UserAuthModelUnitOfWork>
                    {
                        ConnectionString = dbConnStr,
                        DataProvider = DataProvider.SQLite3
                    };
            
            scope = new PerThreadUnitOfWorkScope<UserAuthModelUnitOfWork>(context);
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
            this.LightSpeedRepository = new LightSpeedUserAuthRepository(scope.Current);
        }

        /// <summary>
        /// Seed the test db with users.
        /// </summary>
        private void SeedDbWithUsers()
        {
            this.OrmLiteRepository.CreateUserAuth(
                new Auth.UserAuth
                    {
                        UserName = "lsUser01",
                        Email = "lsUser01@test.com"
                    },
                    "Abc!123");
        }
    }
}
