// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryStringPairTypeConverter.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed.Helpers
{
    using System.Collections.Generic;

    /// <summary>
    /// The Dictionary type converter.
    /// </summary>
    public static class DictionaryStringPairTypeConverter
    {
        /// <summary>
        /// Convert a database string field into Dictionary&lt;string, string&gt; type.
        /// </summary>
        /// <param name="databaseValue">The database value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static Dictionary<string, string> ConvertFromDatabase(string databaseValue)
        {
            return 
                databaseValue.IsNullOrEmpty()
                    ? null
                    : databaseValue.FromJsv<Dictionary<string, string>>();
        }

        /// <summary>
        /// Convert a Dictionary&lt;string, string&gt; type into a string to save into a database field.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static string ConvertToDatabase(Dictionary<string, string> value)
        {
            return 
                value == null
                    ? null
                    : value.ToJsv();
        }
    }
}
