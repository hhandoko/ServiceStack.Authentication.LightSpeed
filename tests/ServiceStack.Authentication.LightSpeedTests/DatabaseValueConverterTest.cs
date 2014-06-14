// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseValueConverterTest.cs" company="ServiceStack.Authentication.LightSpeed">
//   Copyright (c) ServiceStack.Authentication.LightSpeed contributors 2014
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ServiceStack.Authentication.LightSpeedTests
{
    using System.Linq;

    using NUnit.Framework;

    using ServiceStack.Authentication.LightSpeed.Helpers;

    /// <summary>
    /// The database value converter test.
    /// </summary>
    [TestFixture]
    public class DatabaseValueConverterTest
    {
        /// <summary>
        /// Test string list converter method.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="result">The expected result.</param>
        [TestCase(@"", @"[]")]
        [TestCase(@"Hello,World", @"[""Hello"",""World""]")]
        public void WriteStringList(string input, string result)
        {
            // Arrange
            var list = input.Split(',').ToList();

            // Act
            var databaseValue = StringListTypeConverter.ConvertToDatabase(list);

            // Assert
            Assert.IsTrue(string.Equals(databaseValue, result));
        }
    }
}
