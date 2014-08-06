// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LightSpeedClearable.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed
{
    using Mindscape.LightSpeed.Querying;

    using ServiceStack.Auth;

    /// <summary>
    /// The LightSpeed ORM user authentication repository, IClearable implementation.
    /// </summary>
    public partial class LightSpeedUserAuthRepository
        : IClearable
    {
        /// <summary>
        /// Clear / delete the existing UserAuth table data.
        /// </summary>
        public void Clear()
        {
            if (!this.UseDistinctRoleTables)
            {
                this.unitOfWork.Remove(new Query(typeof(LightSpeed.UserAuthRole)));
            }

            this.unitOfWork.Remove(new Query(typeof(LightSpeed.UserAuthDetail)));
            this.unitOfWork.Remove(new Query(typeof(LightSpeed.UserAuth)));
            this.unitOfWork.SaveChanges(true);
        }
    }
}
