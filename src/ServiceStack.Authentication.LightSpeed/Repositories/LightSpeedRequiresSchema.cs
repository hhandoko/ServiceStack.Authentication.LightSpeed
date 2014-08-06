// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedRequiresSchema.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System;

    using Mindscape.LightSpeed;
    using Mindscape.LightSpeed.Migrations;

    using ServiceStack.Auth;

    /// <summary>
    /// The LightSpeed ORM user authentication repository, RequiresSchema implementation.
    /// </summary>
    public partial class LightSpeedUserAuthRepository
        : IRequiresSchema
    {
        /// <summary>
        /// Initialise the database schema.
        /// </summary>
        public void InitSchema()
        {
            this.InitSchema(false);
        }

        /// <summary>
        /// Initialise the database schema, optionally running in preview mode.
        /// </summary>
        /// <param name="preview">The preview mode flag.</param>
        public void InitSchema(bool preview)
        {
            var source = new AssemblyMigrationLoader(typeof(InitialSchema).Assembly);
            var provider = GetProviderType(this.unitOfWork.Context.DataProvider);
            var connection = this.unitOfWork.Context.ConnectionString;
            var version = MigrationVersion.FromString("20140806081722");

            var migrator = Migrator.CreateMigrator(source, provider, connection, version, preview);
            migrator.Execute();

            //Migrator.Migrate(source, provider, connection, null);
        }

        /// <summary>
        /// Drop and recreate UserAuth tables.
        /// </summary>
        public void DropAndReCreateTables()
        {
            this.DropTables();
            this.InitSchema(false);
        }

        /// <summary>
        /// Drop all UserAuth tables.
        /// </summary>
        private void DropTables()
        {
            var source = new AssemblyMigrationLoader(typeof(InitialSchema).Assembly);
            var provider = GetProviderType(this.unitOfWork.Context.DataProvider);
            var connection = this.unitOfWork.Context.ConnectionString;
            var version = MigrationVersion.FromString("0");

            var migrator = Migrator.CreateMigrator(source, provider, connection, version, false);
            migrator.Execute();

            //Migrator.Migrate(source, provider, connection, null);
        }

        /// <summary>
        /// Get the database migration ProviderType from Unit of Work DataProvider.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <returns>The <see cref="ProviderType"/>.</returns>
        /// <exception cref="NotImplementedException">
        /// Throws an exception for Amazon SimpleDB and AzureTable DataProvider.
        /// </exception>
        private static ProviderType GetProviderType(DataProvider provider)
        {
            switch (provider)
            {
                case DataProvider.DB2:
                    return ProviderType.DB2;

                case DataProvider.MySql5:
                    return ProviderType.MySql5;

                case DataProvider.Oracle9:
                    return ProviderType.Oracle9;

                case DataProvider.PostgreSql8:
                    return ProviderType.PostgreSql8;

                case DataProvider.SQLite3:
                    return ProviderType.Sqlite3;

                case DataProvider.SqlServer2000:
                    return ProviderType.SqlServer2000;

                case DataProvider.SqlServer2005:
                    return ProviderType.SqlServer2005;

                case DataProvider.SqlServer2008:
                    return ProviderType.SqlServer2008;

                case DataProvider.SqlServer2012:
                    return ProviderType.SqlServer2012;

                case DataProvider.SqlServerCE:
                    return ProviderType.SqlServerCE;

                case DataProvider.SqlServerCE4:
                    return ProviderType.SqlServerCE4;

                case DataProvider.VistaDB4:
                    return ProviderType.VistaDB4;

                default:
                    throw new NotImplementedException(string.Format("The database migration for {0} is not available.", provider));
            }
        }
    }
}
