// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringListTypeConverter.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// The Dictionary type converter.
    /// </summary>
    internal static class StringListTypeConverter
    {
        /// <summary>
        /// Convert a database string field into Dictionary&lt;string, string&gt; type.
        /// </summary>
        /// <param name="databaseValue">The database value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static List<string> ConvertFromDatabase(string databaseValue)
        {
            return 
                databaseValue.IsNullOrEmpty()
                    ? null
                    : databaseValue.FromJsv<List<string>>();
        }

        /// <summary>
        /// Convert a Dictionary&lt;string, string&gt; type into a string to save into a database field.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static string ConvertToDatabase(IList<string> value)
        {
            return 
                value == null
                    ? null
                    : value.ToJsv();
        }
    }
}
