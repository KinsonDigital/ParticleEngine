// <copyright file="IDimensions.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Represents different dimensions and locations of an object.
    /// </summary>
    public interface IDimensions
    {
        /// <summary>
        /// Gets or sets the location of the object.
        /// </summary>
        Vector2 Location { get; set; }

        /// <summary>
        /// Gets the width of the object.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the object.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets or sets the position of the left side of the object.
        /// </summary>
        int Left { get; set; }

        /// <summary>
        /// Gets or sets the position of the right side of the object.
        /// </summary>
        int Right { get; set; }

        /// <summary>
        /// Gets or sets the position of the top of the object.
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// Gets or sets the position of the bottom the object.
        /// </summary>
        int Bottom { get; set; }
    }
}
