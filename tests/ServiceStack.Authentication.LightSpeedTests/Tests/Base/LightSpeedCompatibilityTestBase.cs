// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedCompatibilityTestBase.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Data;

    using NUnit.Framework;

    using ServiceStack.Auth;
    using ServiceStack.Authentication.LightSpeed;
    using ServiceStack.Data;
    using ServiceStack.OrmLite;
    using ServiceStack.OrmLite.Sqlite;

    /// <summary>
    /// The LightSpeed compatibility test base class.
    /// </summary>
    public class LightSpeedCompatibilityTestBase
        : LightSpeedTestBase
    {
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
        
        /// <summary>
        /// Gets or sets the OrmLite repository.
        /// </summary>
        public OrmLiteAuthRepository OrmLiteRepository { get; set; }

        /// <summary>
        /// Compatibility test fixture setup.
        /// Run once after global rest fixture setup finished runing.
        /// </summary>
        [TestFixtureSetUp]
        public void CompatibilityFixtureSetup()
        {
            CreateDbContext();
        }

        /// <summary>
        /// Test setup.
        /// Runs everytime before each test is run.
        /// </summary>
        [SetUp]
        public void CompatibilityTestSetup()
        {
            OpenDbConn();
            this.InitRepositories();
        }

        /// <summary>
        /// Compatibility test fixture teardown.
        /// Run once prior the global test fixture teardown runs.
        /// </summary>
        [TestFixtureTearDown]
        public void CompatibilityFixtureTearDown()
        {
        }

        /// <summary>
        /// Test tear down.
        /// Runs everytime after each test is finished.
        /// </summary>
        [TearDown]
        public void CompatibilityTestTearDown()
        {
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
        /// Create the database connection context.
        /// </summary>
        private static void CreateDbContext()
        {
            DbFactory =
                new OrmLiteConnectionFactory(
                    DbConnStr,
                    new SqliteOrmLiteDialectProvider());
        }

        /// <summary>
        /// Open a new database connection.
        /// </summary>
        private static void OpenDbConn()
        {
            // OrmLite connection
            DbConn = DbFactory.Open();
        }

        /// <summary>
        /// Initialise the repositories.
        /// </summary>
        private void InitRepositories()
        {
            // Init OrmLite and LightSpeed repository
            this.OrmLiteRepository = new OrmLiteAuthRepository(DbFactory);
            this.LightSpeedRepository = new LightSpeedUserAuthRepository(this.UnitOfWork);
        }
    }
}
