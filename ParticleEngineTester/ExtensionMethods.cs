// <copyright file="ExtensionMethods.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// TODO: Create unit tests for this class

namespace ParticleEngineTester
{
    using System.Drawing;
    using System.Text;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Provides extensions to various types.
    /// </summary>
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns the given <see cref="Vector2"/> as a <see cref="PointF"/> type.
        /// </summary>
        /// <param name="vector">The vector to convert.</param>
        /// <returns>The <see cref="PointF"/> equivalent of a <see cref="Vector2"/>.</returns>
        public static PointF ToPointF(this Vector2 vector) => new PointF(vector.X, vector.Y);

        /// <summary>
        /// Converts the first character of the string to an upper case.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <returns>The string with the first character in upper case.</returns>
        public static string ToUpperFirstChar(this string value) => $"{value[0].ToString().ToUpper()}{value[1..]}";

        /// <summary>
        /// Converts the hyphen separated string value to a title.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value converted to a title.</returns>
        public static string ToTitle(this string value)
        {
            if (value.Contains('-'))
            {
                var sections = value.Split('-');

                var resultStrBuilder = new StringBuilder();

                foreach (var section in sections)
                {
                    resultStrBuilder.Append($"{section.ToUpperFirstChar()} ");
                }

                return resultStrBuilder.ToString().TrimEnd();
            }

            return value;
        }
    }
}
