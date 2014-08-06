// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedManageRoles.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using ServiceStack.Auth;

    /// <summary>
    /// The LightSpeed ORM user authentication repository, IManageRoles implementation.
    /// </summary>
    public partial class LightSpeedUserAuthRepository
        : IManageRoles
    {
        /// <summary>
        /// Gets or sets a value indicating whether distinct role table is used.
        /// </summary>
        public bool UseDistinctRoleTables { get; set; }

        /// <summary>
        /// Get all roles for the user.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <returns>The <see cref="ICollection{string}"/>.</returns>
        public ICollection<string> GetRoles(string userAuthId)
        {
            if (!this.UseDistinctRoleTables)
            {
                var userAuth = this.GetUserAuth(userAuthId);

                return userAuth.Roles;
            }
            else
            {
                var roles =
                    this.unitOfWork.UserAuthRoles
                        .Where(x =>
                            x.UserAuthId == int.Parse(userAuthId)
                            && x.Role != null);

                return roles.ToList().ConvertAll(x => x.Role);
            }
        }

        /// <summary>
        /// Get all permissions for the user.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <returns>The <see cref="ICollection{string}"/>.</returns>
        public ICollection<string> GetPermissions(string userAuthId)
        {
            if (!this.UseDistinctRoleTables)
            {
                var userAuth = this.GetUserAuth(userAuthId);

                return userAuth.Permissions;
            }
            else
            {
                var roles =
                    this.unitOfWork.UserAuthRoles
                        .Where(x =>
                            x.UserAuthId == int.Parse(userAuthId)
                            && x.Permission != null);

                return roles.ToList().ConvertAll(x => x.Permission);
            }
        }

        /// <summary>
        /// Check if user has the role.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <param name="role">The role.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if role argument is null.
        /// </exception>
        public bool HasRole(string userAuthId, string role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }

            if (userAuthId == null)
            {
                return false;
            }

            if (!this.UseDistinctRoleTables)
            {
                var userAuth = this.GetUserAuth(userAuthId);

                return
                    userAuth.Roles != null
                    && userAuth.Roles.Contains(role);
            }
            else
            {
                var totalRoles = 
                    this.unitOfWork.UserAuthRoles
                        .Count(x =>
                            x.UserAuthId == int.Parse(userAuthId)
                            && x.Role == role);

                return totalRoles > 0;
            }
        }

        /// <summary>
        /// Check if user has the permission.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <param name="permission">The permission.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        /// <exception cref="ArgumentNullException">
        /// Exception thrown if permission argument is null.
        /// </exception>
        public bool HasPermission(string userAuthId, string permission)
        {
            if (permission == null)
            {
                throw new ArgumentNullException("permission");
            }

            if (userAuthId == null)
            {
                return false;
            }

            if (!this.UseDistinctRoleTables)
            {
                var userAuth = this.GetUserAuth(userAuthId);

                return
                    userAuth.Roles != null
                    && userAuth.Permissions.Contains(permission);
            }
            else
            {
                var totalPermissions =
                    this.unitOfWork.UserAuthRoles
                        .Count(x =>
                            x.UserAuthId == int.Parse(userAuthId)
                            && x.Permission == permission);

                return totalPermissions > 0;
            }
        }

        /// <summary>
        /// Assign role(s) to the user.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="permissions">The permissions.</param>
        public void AssignRoles(string userAuthId, ICollection<string> roles = null, ICollection<string> permissions = null)
        {
            var userAuth = this.GetUserAuth(userAuthId) as UserAuth;
            if (userAuth == null)
            {
                throw new Exception("No user found in the database with the given id.");
            }

            if (!this.UseDistinctRoleTables)
            {
                if (roles != null && !roles.IsEmpty())
                {
                    var userRoles = userAuth.Roles;
                    foreach (var role in roles)
                    {
                        userRoles.AddIfNotExists(role);
                    }

                    userAuth.Roles = userRoles;
                }

                if (permissions != null && !permissions.IsEmpty())
                {
                    var userPermissions = userAuth.Permissions;
                    foreach (var permission in permissions)
                    {
                        userPermissions.AddIfNotExists(permission);
                    }

                    userAuth.Permissions = userPermissions;
                }

                this.SaveUserAuth(userAuth);
            }
            else
            {
                var now = DateTime.UtcNow;
                var rolesAndPermissions =
                    this.unitOfWork.UserAuthRoles
                        .Where(x => x.UserAuthId == userAuth.Id);

                if (roles != null && !roles.IsEmpty())
                {
                    var roleSet =
                        rolesAndPermissions
                            .Where(x => x.Role != null)
                            .Select(x => x.Role)
                            .ToHashSet();

                    foreach (
                        var role
                        in roles
                            .Where(role => !roleSet.Contains(role)))
                    {
                        this.unitOfWork.Add(
                            new UserAuthRole
                                {
                                    UserAuthId = userAuth.Id,
                                    Role = role,
                                    CreatedDate = now,
                                    ModifiedDate = now
                                });
                    }
                }

                if (permissions != null && !permissions.IsEmpty())
                {
                    var permissionSet =
                        rolesAndPermissions
                            .Where(x => x.Permission != null)
                            .Select(x => x.Permission)
                            .ToHashSet();

                    foreach (
                        var permission
                        in permissions
                            .Where(permission => !permissionSet.Contains(permission)))
                    {
                        this.unitOfWork.Add(
                            new UserAuthRole
                                {
                                    UserAuthId = userAuth.Id,
                                    Permission = permission,
                                    CreatedDate = now,
                                    ModifiedDate = now
                                });
                    }
                }
            }

            this.unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Unassign role(s) from the user.
        /// </summary>
        /// <param name="userAuthId">The UserAuth id.</param>
        /// <param name="roles">The roles.</param>
        /// <param name="permissions">The permissions.</param>
        public void UnAssignRoles(string userAuthId, ICollection<string> roles = null, ICollection<string> permissions = null)
        {
            var userAuth = this.GetUserAuth(userAuthId);

            if (!this.UseDistinctRoleTables)
            {
                if (roles != null && !roles.IsEmpty())
                {
                    var userRoles = userAuth.Roles;
                    foreach (var role in roles)
                    {
                        userRoles.Remove(role);
                    }

                    userAuth.Roles = userRoles;
                }

                if (permissions != null && !permissions.IsEmpty())
                {
                    var userPermissions = userAuth.Permissions;
                    foreach (var permission in permissions)
                    {
                        userPermissions.Remove(permission);
                    }

                    userAuth.Permissions = userPermissions;
                }

                if (roles != null || permissions != null)
                {
                    this.SaveUserAuth(userAuth);
                }
            }
            else
            {
                var hasChanged = false;
                var rolesAndPermissions =
                    this.unitOfWork.UserAuthRoles
                        .Where(x => x.UserAuthId == userAuth.Id)
                        .ToList();

                if (!roles.IsEmpty())
                {
                    var rolesToRemove =
                        rolesAndPermissions.Where(q =>
                            roles.Contains(q.Role));

                    this.unitOfWork.Remove(rolesToRemove);
                    hasChanged = true;
                }

                if (!permissions.IsEmpty())
                {
                    var permissionsToRemove =
                        rolesAndPermissions.Where(q =>
                            permissions.Contains(q.Permission));

                    this.unitOfWork.Remove(permissionsToRemove);
                    hasChanged = true;
                }

                if (hasChanged)
                {
                    this.unitOfWork.SaveChanges();
                }
            }
        }
    }
}
