// --------------------------------------------------------------------------------------------------------------------
// <copyright file="20140806081722_InitialSchema.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed.Migration
{
    using System.ComponentModel;

    using Mindscape.LightSpeed.Migrations;

    /// <summary>
    /// The initial database schema creation script.
    /// </summary>
    [Migration("20140806081722")]
    [Description("Initial ServiceStack UserAuth database schema")]
    public class InitialSchema
        : Migration
    {
        /// <summary>
        /// The database up-versioning.
        /// </summary>
        public override void Up()
        {
            // UserAuth table
            this.AddTable(
                "UserAuth",
                null,
                new Field("Id", ModelDataType.Int32, false),
                new Field[]
                    {
                        new Field("UserName", ModelDataType.String, true).WithSize(8000),
                        new Field("Email", ModelDataType.String, true).WithSize(8000),
                        new Field("PrimaryEmail", ModelDataType.String, true).WithSize(8000),
                        new Field("PhoneNumber", ModelDataType.String, true).WithSize(8000),
                        new Field("FirstName", ModelDataType.String, true).WithSize(8000),
                        new Field("LastName", ModelDataType.String, true).WithSize(8000),
                        new Field("DisplayName", ModelDataType.String, true).WithSize(8000),
                        new Field("Company", ModelDataType.String, true).WithSize(8000),
                        new Field("BirthDate", ModelDataType.DateTime, true),
                        new Field("BirthDateRaw", ModelDataType.String, true).WithSize(8000),
                        new Field("Address", ModelDataType.String, true).WithSize(8000),
                        new Field("Address2", ModelDataType.String, true).WithSize(8000),
                        new Field("City", ModelDataType.String, true).WithSize(8000),
                        new Field("State", ModelDataType.String, true).WithSize(8000),
                        new Field("Country", ModelDataType.String, true).WithSize(8000),
                        new Field("Culture", ModelDataType.String, true).WithSize(8000),
                        new Field("FullName", ModelDataType.String, true).WithSize(8000),
                        new Field("Gender", ModelDataType.String, true).WithSize(8000),
                        new Field("Language", ModelDataType.String, true).WithSize(8000),
                        new Field("MailAddress", ModelDataType.String, true).WithSize(8000),
                        new Field("Nickname", ModelDataType.String, true).WithSize(8000),
                        new Field("PostalCode", ModelDataType.String, true).WithSize(8000),
                        new Field("TimeZone", ModelDataType.String, true).WithSize(8000),
                        new Field("Salt", ModelDataType.String, true).WithSize(8000),
                        new Field("PasswordHash", ModelDataType.String, true).WithSize(8000),
                        new Field("DigestHa1Hash", ModelDataType.String, true).WithSize(8000),
                        new Field("Roles", ModelDataType.String, true).WithSize(8000),
                        new Field("Permissions", ModelDataType.String, true).WithSize(8000),
                        new Field("CreatedDate", ModelDataType.DateTime, false),
                        new Field("ModifiedDate", ModelDataType.DateTime, false),
                        new Field("InvalidLoginAttempts", ModelDataType.Int32, false),
                        new Field("LastLoginAttempt", ModelDataType.DateTime, true),
                        new Field("LockedDate", ModelDataType.DateTime, true),
                        new Field("RecoveryToken", ModelDataType.String, true).WithSize(8000),
                        new Field("RefId", ModelDataType.Int32, true),
                        new Field("RefIdStr", ModelDataType.String, true).WithSize(8000),
                        new Field("Meta", ModelDataType.String, true).WithSize(8000)
                    });

            // Create UserAuthDetails table
            this.AddTable(
                "UserAuthDetails",
                null,
                new Field("Id", ModelDataType.Int32, false),
                new Field[]
                    {
                        new Field("Provider", ModelDataType.String, true).WithSize(8000),
                        new Field("UserId", ModelDataType.String, true).WithSize(8000),
                        new Field("UserName", ModelDataType.String, true).WithSize(8000),
                        new Field("FullName", ModelDataType.String, true).WithSize(8000),
                        new Field("DisplayName", ModelDataType.String, true).WithSize(8000),
                        new Field("FirstName", ModelDataType.String, true).WithSize(8000),
                        new Field("LastName", ModelDataType.String, true).WithSize(8000),
                        new Field("Company", ModelDataType.String, true).WithSize(8000),
                        new Field("Email", ModelDataType.String, true).WithSize(8000),
                        new Field("PhoneNumber", ModelDataType.String, true).WithSize(8000),
                        new Field("BirthDate", ModelDataType.DateTime, true),
                        new Field("BirthDateRaw", ModelDataType.String, true).WithSize(8000),
                        new Field("Address", ModelDataType.String, true).WithSize(8000),
                        new Field("Address2", ModelDataType.String, true).WithSize(8000),
                        new Field("City", ModelDataType.String, true).WithSize(8000),
                        new Field("State", ModelDataType.String, true).WithSize(8000),
                        new Field("Country", ModelDataType.String, true).WithSize(8000),
                        new Field("Culture", ModelDataType.String, true).WithSize(8000),
                        new Field("Gender", ModelDataType.String, true).WithSize(8000),
                        new Field("Language", ModelDataType.String, true).WithSize(8000),
                        new Field("MailAddress", ModelDataType.String, true).WithSize(8000),
                        new Field("Nickname", ModelDataType.String, true).WithSize(8000),
                        new Field("PostalCode", ModelDataType.String, true).WithSize(8000),
                        new Field("TimeZone", ModelDataType.String, true).WithSize(8000),
                        new Field("RefreshToken", ModelDataType.String, true).WithSize(8000),
                        new Field("RefreshTokenExpiry", ModelDataType.DateTime, true),
                        new Field("RequestToken", ModelDataType.String, true).WithSize(8000),
                        new Field("RequestTokenSecret", ModelDataType.String, true).WithSize(8000),
                        new Field("Items", ModelDataType.String, true).WithSize(8000),
                        new Field("AccessToken", ModelDataType.String, true).WithSize(8000),
                        new Field("AccessTokenSecret", ModelDataType.String, true).WithSize(8000),
                        new Field("CreatedDate", ModelDataType.DateTime, false),
                        new Field("ModifiedDate", ModelDataType.DateTime, false),
                        new Field("RefId", ModelDataType.Int32, true),
                        new Field("RefIdStr", ModelDataType.String, true).WithSize(8000),
                        new Field("Meta", ModelDataType.String, true).WithSize(8000),
                        new ForeignKeyField("UserAuthId", ModelDataType.Int32, false, "UserAuth", null, "Id")
                    });

            // Create UserAuthRole table
            this.AddTable(
                "UserAuthRole",
                null,
                new Field("Id", ModelDataType.Int32, false),
                new Field[]
                    {
                        new Field("Role", ModelDataType.String, true).WithSize(8000),
                        new Field("Permission", ModelDataType.String, true).WithSize(8000),
                        new Field("CreatedDate", ModelDataType.DateTime, false),
                        new Field("ModifiedDate", ModelDataType.DateTime, false),
                        new Field("RefId", ModelDataType.Int32, true),
                        new Field("RefIdStr", ModelDataType.String, true).WithSize(8000),
                        new Field("Meta", ModelDataType.String, true).WithSize(8000),
                        new ForeignKeyField("UserAuthId", ModelDataType.Int32, false, "UserAuth", null, "Id")
                    });
        }

        /// <summary>
        /// The database down-versioning.
        /// </summary>
        public override void Down()
        {
            // Drop Foreign Key constraints
            this.DropColumn("UserAuthRole", null, "UserAuthId", true);
            this.DropColumn("UserAuthDetails", null, "UserAuthId", true);

            // Drop database tables
            this.DropTable("UserAuthDetails", null);
            this.DropTable("UserAuthRole", null);
            this.DropTable("UserAuth", null);
        }
    }
}