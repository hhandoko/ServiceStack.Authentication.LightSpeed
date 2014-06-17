// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuthDetail.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System.Collections.Generic;

    using ServiceStack.Auth;
    using ServiceStack.Text;

    /// <summary>
    /// The user authentication details data entity.
    /// </summary>
    public partial class UserAuthDetail : IUserAuthDetails
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
        /// Gets or sets the items.
        /// </summary>
        public Dictionary<string, string> Items
        {
            get { return this.Serializer.DeserializeFromString<Dictionary<string, string>>(_items); }
            set { Set(ref _items, this.Serializer.SerializeToString(value)); }
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
