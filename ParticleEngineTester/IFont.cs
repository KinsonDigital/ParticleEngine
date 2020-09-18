// <copyright file="IFont.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents the font related to text to be rendered to the screen.
    /// </summary>
    public interface IFont
    {
        /// <summary>
        /// Gets the internal monogame font.
        /// </summary>
        SpriteFont InternalFont { get; }

        /// <summary>
        /// Gets or sets the text of the font.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets the width of the text.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the font.
        /// </summary>
        int Height { get; }
    }
}
