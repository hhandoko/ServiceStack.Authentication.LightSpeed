// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IUnitOfWork.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using System.Data;

    /// <summary>
    /// The UnitOfWork interface.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Begin a database transaction.
        /// </summary>
        /// <returns>The <see cref="IDbTransaction"/>.</returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// Begin a database transaction.
        /// </summary>
        /// <param name="isolationLevel">The isolation level.</param>
        /// <returns>The <see cref="IDbTransaction"/>.</returns>
        IDbTransaction BeginTransaction(IsolationLevel isolationLevel);

        /// <summary>
        /// Persist changes to the database.
        /// </summary>
        void SaveChanges();

        /// <summary>
        /// Persist changes to the database.
        /// </summary>
        /// <param name="reset"><c>True</c> to clear existing entities in the unit of work.</param>
        void SaveChanges(bool reset);
    }
}
