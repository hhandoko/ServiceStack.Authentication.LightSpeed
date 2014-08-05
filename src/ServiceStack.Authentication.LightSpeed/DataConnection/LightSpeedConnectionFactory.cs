// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedConnectionFactory.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System;
    using System.Data;

    using ServiceStack.Data;

    /// <summary>
    /// The LightSpeed ORM connection factory.
    /// </summary>
    public class LightSpeedConnectionFactory
        : IDbConnectionFactory
    {
        /// <summary>
        /// Create a new DB connection
        /// </summary>
        /// <returns>The <see cref="IDbConnection"/>.</returns>
        public IDbConnection CreateDbConnection()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Open a DB connection.
        /// </summary>
        /// <returns>The <see cref="IDbConnection"/>.</returns>
        public IDbConnection OpenDbConnection()
        {
            throw new NotImplementedException();
        }
    }
}
