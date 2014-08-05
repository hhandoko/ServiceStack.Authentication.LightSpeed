// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedTestBase.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System;
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

        #region OrmLite session
        /// <summary>
        /// Gets the database connection.
        /// </summary>
        public static IDbConnection DbConn { get; private set; }

        /// <summary>
        /// Gets the database connection factory.
        /// </summary>
        public static IDbConnectionFactory DbFactory { get; private set; }
        #endregion

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
        /// Gets or sets the OrmLite repository.
        /// </summary>
        public OrmLiteAuthRepository OrmLiteRepository { get; set; }

        /// <summary>
        /// Gets or sets the LightSpeed repository.
        /// </summary>
        public LightSpeedUserAuthRepository LightSpeedRepository { get; set; }

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
        /// The tear down.
        /// </summary>
        [TestFixtureTearDown]
        public void TearDown()
        {
            if (AuthScope.HasCurrent)
            {
                AuthScope.Dispose();
            }

            DbConn.Close();
        }

        /// <summary>
        /// Create a new user using OrmLiteAuthRepository.
        /// </summary>
        /// <param name="username">The username.</param>
        public void CreateUserWithOrmLite(string username)
        {
            this.OrmLiteRepository.CreateUserAuth(
                new Auth.UserAuth
                {
                    UserName = username,
                    Email = string.Format("{0}@{1}", username, EmailDomain)
                },
                "Abc!123");
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
        /// Initiate database connection.
        /// </summary>
        private static void InitDbConn()
        {
            DbConnStr =
                string.Format(
                    "Data Source={0};Version=3;",
                    Path.GetFullPath(string.Format("{0}/Data/ss_auth.sqlite", TestContext.CurrentContext.WorkDirectory)));

            DbFactory =
                new OrmLiteConnectionFactory(
                    DbConnStr,
                    new SqliteOrmLiteDialectProvider());

            DbConn = DbFactory.Open();

            AuthContext =
                new LightSpeedContext<UserAuthModelUnitOfWork>
                {
                    ConnectionString = DbConnStr,
                    DataProvider = DataProvider.SQLite3,
                    UnitOfWorkFactory = new UserAuthModelUnitOfWorkFactory(new JsvStringSerializer()),
                    IdentityMethod = IdentityMethod.IdentityColumn
                };
            AuthScope = new SimpleUnitOfWorkScope<UserAuthModelUnitOfWork>(AuthContext);
        }

        /// <summary>
        /// Drop and recreate ServiceStack auth tables.
        /// </summary>
        private static void DropAndCreateTables()
        {
            DbConn.DropTable<ServiceStack.Auth.UserAuthRole>();
            DbConn.DropTable<ServiceStack.Auth.UserAuthDetails>();
            DbConn.DropTable<ServiceStack.Auth.UserAuth>();

            DbConn.CreateTable<ServiceStack.Auth.UserAuth>();
            DbConn.CreateTable<ServiceStack.Auth.UserAuthDetails>();
            DbConn.CreateTable<ServiceStack.Auth.UserAuthRole>();
        }

        /// <summary>
        /// Initialise the repositories.
        /// </summary>
        private void InitRepositories()
        {
            // Init OrmLite and LightSpeed repository
            this.OrmLiteRepository = new OrmLiteAuthRepository(DbFactory);
            this.LightSpeedRepository = new LightSpeedUserAuthRepository(AuthScope.Current);
        }
    }
}
