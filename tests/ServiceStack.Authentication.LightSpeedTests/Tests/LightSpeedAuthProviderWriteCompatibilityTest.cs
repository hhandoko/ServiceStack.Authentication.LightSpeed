// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedAuthProviderWriteCompatibilityTest.cs" company="ServiceStack.Authentication.LightSpeed">
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
    public class LightSpeedAuthProviderWriteCompatibilityTest
        : LightSpeedTestBase
    {
        /// <summary>
        /// Check LightSpeed write compatibility for OrmLite-created entries.
        /// </summary>
        /// <param name="username">The username.</param>
        [TestCase("wGetByUsername1")]
        [TestCase("wGetByUsername2")]
        [TestCase("wGetByUsername3")]
        public void CheckWriteGetByUsername(string username)
        {
            // Arrange
            this.CreateUserWithLightSpeed(username);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed write compatibility for OrmLite-created entries.
        /// </summary>
        /// <param name="username">The username.</param>
        [TestCase("wGetByEmail1")]
        [TestCase("wGetByEmail2")]
        [TestCase("wGetByEmail3")]
        public void CheckWriteGetByEmail(string username)
        {
            // Arrange
            this.CreateUserWithLightSpeed(username);
            var email = string.Format("{0}@{1}", username, EmailDomain);

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(email);
            var lightSpeedUser = this.LightSpeedRepository.GetUserAuthByUserName(email);

            // Assert
            Assert.AreEqual(ormLiteUser.Id, lightSpeedUser.Id);
        }

        /// <summary>
        /// Check LightSpeed write assigned user role(s).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="role">The role.</param>
        [TestCase("wGetRole1", "SuperAdmin")]
        [TestCase("wGetRole2", "Marketing")]
        public void CheckWriteAssignRole(string username, string role)
        {
            // Arrange
            this.CreateUserWithLightSpeed(username);
            var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);
            this.LightSpeedRepository.AssignRoles(lightspeedUser, roles: new Collection<string> { role });

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            var hasRole = ormLiteUser.Roles.Contains(role);

            // Assert
            Assert.IsTrue(hasRole);
        }

        /// <summary>
        /// Check LightSpeed write assigned user role(s).
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="permission">The permission.</param>
        [TestCase("wGetPermission1", "AddItem")]
        [TestCase("wGetPermission2", "EditItem")]
        public void CheckWriteAssignPermission(string username, string permission)
        {
            // Arrange
            this.CreateUserWithLightSpeed(username);
            var lightspeedUser = this.LightSpeedRepository.GetUserAuthByUserName(username);
            this.LightSpeedRepository.AssignRoles(lightspeedUser, permissions: new Collection<string> { permission });

            // Act
            var ormLiteUser = this.OrmLiteRepository.GetUserAuthByUserName(username);
            var hasRole = ormLiteUser.Permissions.Contains(permission);

            // Assert
            Assert.IsTrue(hasRole);
        }
    }
}
