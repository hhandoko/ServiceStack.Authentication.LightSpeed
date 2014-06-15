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
        /// The serializer.
        /// </summary>
        private static readonly IStringSerializer Serializer = new JsvStringSerializer();

        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public List<string> Permissions
        {
            get { return Serializer.DeserializeFromString<List<string>>(_permissions); }
            set { Set(ref _permissions, Serializer.SerializeToString(value)); }
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<string> Roles
        {
            get { return Serializer.DeserializeFromString<List<string>>(_roles); }
            set { Set(ref _roles, Serializer.SerializeToString(value)); }
        }

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        public Dictionary<string, string> Meta
        {
            get { return Serializer.DeserializeFromString<Dictionary<string, string>>(_meta); }
            set { Set(ref _meta, Serializer.SerializeToString(value)); }
        }
    }
}
