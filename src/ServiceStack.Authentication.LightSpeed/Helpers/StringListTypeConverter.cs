// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringListTypeConverter.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeed.Helpers
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The Dictionary type converter.
    /// </summary>
    public static class StringListTypeConverter
    {
        /// <summary>
        /// The array wrapper.
        /// </summary>
        private static readonly char[] ArrayWrapper = { '[', ']' };

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
                    : databaseValue.Trim(ArrayWrapper).Split(',').ToList();
        }

        /// <summary>
        /// Convert a Dictionary&lt;string, string&gt; type into a string to save into a database field.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="object"/>.</returns>
        public static string ConvertToDatabase(IList<string> value)
        {
            return
                value == null || value[0].IsNullOrEmpty() || value == new List<string>()
                    ? @"[]"
                    : string.Format(@"[""{0}""]", value.Join(@""","""));
        }
    }
}
