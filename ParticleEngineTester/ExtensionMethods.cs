// <copyright file="ExtensionMethods.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System.Drawing;
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
    }
}
