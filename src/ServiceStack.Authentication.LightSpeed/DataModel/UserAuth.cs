// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuth.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System.Collections.Generic;

    using ServiceStack.Auth;

    using DictConvert = ServiceStack.Authentication.LightSpeed.Helpers.DictionaryStringPairTypeConverter;
    using ListConvert = ServiceStack.Authentication.LightSpeed.Helpers.StringListTypeConverter;

    /// <summary>
    /// The user authentication data entity.
    /// </summary>
    public partial class UserAuth : IUserAuth
    {
        /// <summary>
        /// Gets or sets the permissions.
        /// </summary>
        public List<string> Permissions
        {
            get { return ListConvert.ConvertFromDatabase(_permissions); }
            set { Set(ref _permissions, ListConvert.ConvertToDatabase(value)); }
        }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public List<string> Roles
        {
            get { return ListConvert.ConvertFromDatabase(_roles); }
            set { Set(ref _roles, ListConvert.ConvertToDatabase(value)); }
        }

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        public Dictionary<string, string> Meta
        {
            get { return DictConvert.ConvertFromDatabase(_meta); }
            set { Set(ref _meta, DictConvert.ConvertToDatabase(value)); }
        }
    }
}
