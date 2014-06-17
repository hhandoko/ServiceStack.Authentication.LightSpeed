// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserAuthModelUnitOfWork.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using ServiceStack.Text;

    /// <summary>
    /// The UserAuth model unit of work.
    /// </summary>
    public partial class UserAuthModelUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Gets or sets the string serializer.
        /// </summary>
        public IStringSerializer Serializer { get; set; }
    }
}
