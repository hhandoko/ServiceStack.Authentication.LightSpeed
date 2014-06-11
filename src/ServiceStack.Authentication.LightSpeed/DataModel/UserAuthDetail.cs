// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuthDetail.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System.Collections.Generic;

    using ServiceStack.Auth;

    using DictConvert = ServiceStack.Authentication.LightSpeed.Helpers.DictionaryStringPairTypeConverter;

    /// <summary>
    /// The user authentication details data entity.
    /// </summary>
    public partial class UserAuthDetail : IUserAuthDetails
    {
        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public Dictionary<string, string> Items
        {
            get { return DictConvert.ConvertFromDatabase(_items); }
            set { Set(ref _items, DictConvert.ConvertToDatabase(value)); }
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
