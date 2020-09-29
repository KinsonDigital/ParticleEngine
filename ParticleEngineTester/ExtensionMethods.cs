// <copyright file="ExtensionMethods.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable SA1512 // Single-line comments should not be followed by blank line
#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
// TODO: Create unit tests for this class
#pragma warning restore SA1512 // Single-line comments should not be followed by blank line
#pragma warning restore SA1515 // Single-line comment should be preceded by blank line

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
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
            else
            {
                return value;
            }
        }

        /// <summary>
        /// Sets the size of the window to the given <paramref name="width"/> and <paramref name="height"/>.
        /// </summary>
        /// <param name="deviceManager">The graphics device manager to use to set the size.</param>
        /// <param name="width">The width of the window.</param>
        /// <param name="height">The height of the window.</param>
        [ExcludeFromCodeCoverage]
        public static void SetWindowSize(this GraphicsDeviceManager deviceManager, int width, int height)
        {
            if (deviceManager is null)
            {
                throw new ArgumentNullException(nameof(deviceManager), "The parameter must not be null.");
            }

            deviceManager.PreferredBackBufferWidth = width;
            deviceManager.PreferredBackBufferHeight = height;
            deviceManager.ApplyChanges();
        }
    }
}
