// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedAuthProviderReadCompatibilityTest.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Collections.ObjectModel;

    using NUnit.Framework;

    using ServiceStack.Auth;

    /// <summary>
    /// The LightSpeed ORM auth provider compatibility with OrmLite auth provider test.
    /// </summary>
    [TestFixture]
    public class LightSpeedAuthProviderReadCompatibilityTest
        : LightSpeedCompatibilityTestBase
    {
        /// <summary>
        /// Check LightSpeed read compatibility for OrmLite-created entries.
        /// </summary>
        /// <param name="username">The username.</param>
        [TestCase("rGetByUsername1")]
        [TestCase("rGetByUsername2")]
        [TestCase("rGetByUsername3")]
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
        [TestCase("rGetByEmail1")]
        [TestCase("rGetByEmail2")]
        [TestCase("rGetByEmail3")]
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
        [TestCase("rGetRole1", "SuperAdmin")]
        [TestCase("rGetRole2", "Marketing")]
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
        [TestCase("rGetPermission1", "AddItem")]
        [TestCase("rGetPermission2", "EditItem")]
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
    }
}
