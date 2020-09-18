// <copyright file="ITexture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// A simple 2D texture that can be rendered.
    /// </summary>
    public interface ITexture
    {
        /// <summary>
        /// Gets the width of the <see cref="ITexture"/>.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the <see cref="ITexture"/>.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the internal monogame texture.
        /// </summary>
        Texture2D InternalTexture { get; }

        /// <summary>
        /// Retrieves the contents of the texture.
        /// </summary>
        /// <param name="data">Destination array for the texture data.</param>
        /// <remarks>
        ///     Throws ArgumentException if data is null,
        ///     data.length is too short or if arraySlice is greater than 0 and the GraphicsDevice
        ///     doesn't support texture arrays.
        /// </remarks>
        void GetData(Color[] data);

        /// <summary>
        /// Changes the texture's pixels.
        /// </summary>
        /// <param name="data">New data for the texture.</param>
        void SetData(Color[] data);
    }
}
