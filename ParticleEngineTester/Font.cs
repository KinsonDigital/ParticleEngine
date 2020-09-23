// <copyright file="Font.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Represents the font related to text to be rendered to the screen.
    /// </summary>
    public class Font : IFont
    {
        private string text = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="Font"/> class.
        /// </summary>
        /// <param name="font">The internal monogame font.</param>
        public Font(SpriteFont font)
        {
            if (font is null)
            {
                throw new ArgumentNullException(nameof(font), "The parameter must not be null.");
            }

            InternalFont = font;
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public SpriteFont InternalFont { get; }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public string Text
        {
            get => this.text;
            set => this.text = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int Width => (int)InternalFont.MeasureString(Text).X;

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public int Height => (int)InternalFont.MeasureString(Text).Y;
    }
}
