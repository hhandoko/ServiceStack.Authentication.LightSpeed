// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedManageRolesTest.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Linq;

    using Mindscape.LightSpeed;

    using NUnit.Framework;

    using ServiceStack.Auth;
    using ServiceStack.Authentication.LightSpeed;
    using ServiceStack.Host;
    using ServiceStack.Testing;

    /// <summary>
    /// The LightSpeed ORM ServiceStack Auth manage roles test.
    /// </summary>
    [TestFixture]
    public class LightSpeedManageRolesTest
        : LightSpeedTestBase
    {
        /// <summary>
        /// The administrators' secret key.
        /// </summary>
        private const string AdminAuthSecret = "S3cret";

        /// <summary>
        /// The role name for testing.
        /// </summary>
        private const string TestRoleName = "TestRole";

        /// <summary>
        /// The permission name for testing.
        /// </summary>
        private const string TestPermissionName = "TestPermission";

        /// <summary>
        /// Check role assignment in UserAuth table test using admin auth secret key.
        /// </summary>
        [Test]
        public void CheckRoleAssignmentInUserAuthTableUsingSecretKey()
        {
            using (var appHost = new BasicAppHost
            {
                ConfigureAppHost = host =>
                {
                    host.Config.AdminAuthSecret = AdminAuthSecret;
                },
                ConfigureContainer = container =>
                {
                    container.Register<IUnitOfWork>(c => UnitOfWork);
                    container.Register<IAuthRepository>(c =>
                        new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>()));
                }
            }.Init())
            {
                // Arrange
                LightSpeed.UserAuth userAuth;
                AssignRolesResponse assignRolesResponse;
                var newRegistration = CreateNewUserRegistration();
                var request = new BasicRequest(newRegistration);
                request.QueryString["authSecret"] = AdminAuthSecret;
                var response = (RegisterResponse)appHost.ExecuteService(newRegistration, request);
                
                // Test #1: Check role and permission assignment
                // ---------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var assignRoleRequest = 
                    new AssignRoles
                    {
                        UserName = userAuth.UserName,
                        Roles = { TestRoleName },
                        Permissions = { TestPermissionName },
                    };
                
                // Assert #1.1: 
                // Check AssignRoles response to contain roles and permissions
                assignRolesResponse = (AssignRolesResponse)appHost.ExecuteService(assignRoleRequest, request);
                Assert.That(assignRolesResponse.AllRoles[0], Is.EqualTo(TestRoleName));
                Assert.That(assignRolesResponse.AllPermissions[0], Is.EqualTo(TestPermissionName));
                
                // Assert #1.2: 
                // Check UserAuth to contain roles and permissions
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId);
                Assert.That(userAuth.Roles[0], Is.EqualTo(TestRoleName));
                Assert.That(userAuth.Permissions[0], Is.EqualTo(TestPermissionName));

                // Test #2: Check role and permission un-assignment
                // ------------------------------------------------
                // Act
                var unassignRolesRequest =
                    new UnAssignRoles
                        {
                            UserName = userAuth.UserName,
                            Roles = { TestRoleName },
                            Permissions = { TestPermissionName },
                        };
                appHost.ExecuteService(unassignRolesRequest, request);

                // Assert #2.1:
                // Check UserAuth not to contain roles and permissions above
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId);
                Assert.That(userAuth.Roles.Count, Is.EqualTo(0));
                Assert.That(userAuth.Permissions.Count, Is.EqualTo(0));
            }
        }

        /// <summary>
        /// Check role assignment in UserRole table test using admin auth secret key.
        /// </summary>
        [Test]
        public void CheckRoleAssignmentInUserRoleTableUsingSecretKey()
        {
            using (var appHost = new BasicAppHost
            {
                ConfigureAppHost = host =>
                {
                    host.Config.AdminAuthSecret = AdminAuthSecret;
                },
                ConfigureContainer = container =>
                {
                    container.Register<IUnitOfWork>(c => UnitOfWork);
                    container.Register<IAuthRepository>(c =>
                        new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>())
                            {
                                UseDistinctRoleTables = true
                            });
                }
            }.Init())
            {
                // Arrange
                LightSpeed.UserAuth userAuth;
                AssignRolesResponse assignRolesResponse;
                var newRegistration = CreateNewUserRegistration();
                var request = new BasicRequest(newRegistration);
                request.QueryString["authSecret"] = AdminAuthSecret;
                var response = (RegisterResponse)appHost.ExecuteService(newRegistration, request);

                // Test #1: Check role and permission assignment
                // ---------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var assignRoleRequest =
                    new AssignRoles
                        {
                            UserName = userAuth.UserName,
                            Roles = { TestRoleName },
                            Permissions = { TestPermissionName },
                        };

                // Assert #1.1: 
                // Check AssignRoles response to contain roles and permissions
                assignRolesResponse = (AssignRolesResponse)appHost.ExecuteService(assignRoleRequest, request);
                Assert.That(assignRolesResponse.AllRoles[0], Is.EqualTo(TestRoleName));
                Assert.That(assignRolesResponse.AllPermissions[0], Is.EqualTo(TestPermissionName));

                // Assert #1.2: 
                // Check that roles and permissions are not persisted to UserAuth table
                Assert.That(userAuth.Roles.Count, Is.EqualTo(0));
                Assert.That(userAuth.Permissions.Count, Is.EqualTo(0));

                // Assert #1.3:
                // Check UserRoles table to contain roles and permissions
                var manageRoles = (IManageRoles)appHost.Container.Resolve<IAuthRepository>();
                Assert.That(manageRoles.HasRole(userAuth.Id.ToString(), TestRoleName));
                Assert.That(manageRoles.HasPermission(userAuth.Id.ToString(), TestPermissionName));

                // Test #2: Check role and permission un-assignment
                // ------------------------------------------------
                // Act
                var unassignRolesRequest =
                    new UnAssignRoles
                        {
                            UserName = userAuth.UserName,
                            Roles = { TestRoleName },
                            Permissions = { TestPermissionName },
                        };
                appHost.ExecuteService(unassignRolesRequest, request);

                // Assert #2.1:
                // Check UserRole table not to contain roles and permissions above
                Assert.That(!manageRoles.HasRole(userAuth.Id.ToString(), TestRoleName));
                Assert.That(!manageRoles.HasPermission(userAuth.Id.ToString(), TestPermissionName));
            }
        }


        /// <summary>
        /// Check role assignment in UserAuth table test using IAuthRepository.
        /// </summary>
        [Test]
        public void CheckRoleAssignmentInUserAuthTableUsingIAuthRepository()
        {
            using (var appHost = new BasicAppHost
            {
                ConfigureContainer = container =>
                {
                    container.Register<IUnitOfWork>(c => UnitOfWork);
                    container.Register<IAuthRepository>(c =>
                        new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>()));
                }
            }.Init())
            {
                // Arrange
                LightSpeed.UserAuth userAuth;
                var newRegistration = CreateNewUserRegistration();
                var request = new BasicRequest(newRegistration);
                var response = (RegisterResponse)appHost.ExecuteService(newRegistration, request);
                var authRepo = appHost.Resolve<IAuthRepository>();

                // Test #1: Check role and permission assignment
                // ---------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var assignRoleRequest =
                    new AssignRoles
                    {
                        UserName = userAuth.UserName,
                        Roles = { TestRoleName },
                        Permissions = { TestPermissionName },
                    };

                userAuth = (LightSpeed.UserAuth)authRepo.GetUserAuthByUserName(assignRoleRequest.UserName);
                authRepo.AssignRoles(userAuth, assignRoleRequest.Roles, assignRoleRequest.Permissions);

                // Assert #1.1: 
                // Check UserAuth to contain roles and permissions
                Assert.That(authRepo.GetRoles(userAuth).FirstOrDefault(), Is.EqualTo(TestRoleName));
                Assert.That(authRepo.GetPermissions(userAuth).FirstOrDefault(), Is.EqualTo(TestPermissionName));

                // Test #2: Check role and permission un-assignment
                // ------------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var unassignRolesRequest =
                    new UnAssignRoles
                    {
                        UserName = userAuth.UserName,
                        Roles = { TestRoleName },
                        Permissions = { TestPermissionName },
                    };

                userAuth = (LightSpeed.UserAuth)authRepo.GetUserAuthByUserName(assignRoleRequest.UserName);
                authRepo.UnAssignRoles(userAuth, unassignRolesRequest.Roles, unassignRolesRequest.Permissions);

                // Assert #2.1:
                // Check UserAuth not to contain roles and permissions above
                Assert.That(authRepo.GetRoles(userAuth).FirstOrDefault(), Is.Null);
                Assert.That(authRepo.GetPermissions(userAuth).FirstOrDefault(), Is.Null);
            }
        }

        /// <summary>
        /// Check role assignment in UserRole table test using IAuthRepository.
        /// </summary>
        [Test]
        public void CheckRoleAssignmentInUserRoleTableUsingIAuthRepository()
        {
            using (var appHost = new BasicAppHost
            {
                ConfigureContainer = container =>
                {
                    container.Register<IUnitOfWork>(c => UnitOfWork);
                    container.Register<IAuthRepository>(c =>
                        new LightSpeedUserAuthRepository(c.Resolve<IUnitOfWork>())
                        {
                            UseDistinctRoleTables = true
                        });
                }
            }.Init())
            {
                // Arrange
                LightSpeed.UserAuth userAuth;
                AssignRolesResponse assignRolesResponse;
                var newRegistration = CreateNewUserRegistration();
                var request = new BasicRequest(newRegistration);
                var response = (RegisterResponse)appHost.ExecuteService(newRegistration, request);
                var authRepo = appHost.Resolve<IAuthRepository>();

                // Test #1: Check role and permission assignment
                // ---------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var assignRoleRequest =
                    new AssignRoles
                    {
                        UserName = userAuth.UserName,
                        Roles = { TestRoleName },
                        Permissions = { TestPermissionName },
                    };

                userAuth = (LightSpeed.UserAuth)authRepo.GetUserAuthByUserName(assignRoleRequest.UserName);
                authRepo.AssignRoles(userAuth, assignRoleRequest.Roles, assignRoleRequest.Permissions);

                // Assert #1.1: 
                // Check UserAuth to contain roles and permissions
                Assert.That(authRepo.GetRoles(userAuth).FirstOrDefault(), Is.EqualTo(TestRoleName));
                Assert.That(authRepo.GetPermissions(userAuth).FirstOrDefault(), Is.EqualTo(TestPermissionName));

                // Assert #1.2: 
                // Check that roles and permissions are not persisted to UserAuth table
                Assert.That(userAuth.Roles.Count, Is.EqualTo(0));
                Assert.That(userAuth.Permissions.Count, Is.EqualTo(0));

                // Assert #1.3:
                // Check UserRoles table to contain roles and permissions
                var manageRoles = (IManageRoles)appHost.Container.Resolve<IAuthRepository>();
                Assert.That(manageRoles.HasRole(userAuth.Id.ToString(), TestRoleName));
                Assert.That(manageRoles.HasPermission(userAuth.Id.ToString(), TestPermissionName));

                // Test #2: Check role and permission un-assignment
                // ------------------------------------------------
                // Act
                userAuth = UnitOfWork.FindById<LightSpeed.UserAuth>(response.UserId); // Hydrate userAuth

                var unassignRolesRequest =
                    new UnAssignRoles
                    {
                        UserName = userAuth.UserName,
                        Roles = { TestRoleName },
                        Permissions = { TestPermissionName },
                    };

                userAuth = (LightSpeed.UserAuth)authRepo.GetUserAuthByUserName(assignRoleRequest.UserName);
                authRepo.UnAssignRoles(userAuth, unassignRolesRequest.Roles, unassignRolesRequest.Permissions);

                // Assert #2.1:
                // Check UserAuth to contain roles and permissions
                Assert.That(authRepo.GetRoles(userAuth).FirstOrDefault(), Is.Null);
                Assert.That(authRepo.GetPermissions(userAuth).FirstOrDefault(), Is.Null);

                // Assert #2.2:
                // Check UserRole table not to contain roles and permissions above
                Assert.That(!manageRoles.HasRole(userAuth.Id.ToString(), TestRoleName));
                Assert.That(!manageRoles.HasPermission(userAuth.Id.ToString(), TestPermissionName));
            }
        }
    }
}
