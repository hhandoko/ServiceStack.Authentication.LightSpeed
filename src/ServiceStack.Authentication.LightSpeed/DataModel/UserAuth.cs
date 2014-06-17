// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuth.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System.Collections.Generic;

    using ServiceStack.Auth;
    using ServiceStack.Text;

    /// <summary>
    /// The user authentication data entity.
    /// </summary>
    public partial class UserAuth : IUserAuth
    {
        /// <summary>
        /// Gets the string serializer.
        /// </summary>
        private IStringSerializer Serializer
        {
            get
            {
                return
                    ((UserAuthModelUnitOfWork)UnitOfWork).Serializer
                    ?? new JsvStringSerializer();
            }
        }

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public List<string> Permissions
        {
            get { return this.Serializer.DeserializeFromString<List<string>>(_permissions); }
            set { Set(ref _permissions, this.Serializer.SerializeToString(value)); }
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<string> Roles
        {
            get { return this.Serializer.DeserializeFromString<List<string>>(_roles); }
            set { Set(ref _roles, this.Serializer.SerializeToString(value)); }
        }

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        public Dictionary<string, string> Meta
        {
            get { return this.Serializer.DeserializeFromString<Dictionary<string, string>>(_meta); }
            set { Set(ref _meta, this.Serializer.SerializeToString(value)); }
        }
    }
}
