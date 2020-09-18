// <copyright file="IContentLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;

    /// <summary>
    /// Loads various kinds of content.
    /// </summary>
    public interface IContentLoader : IDisposable
    {
        /// <summary>
        /// Loads a texture.
        /// </summary>
        /// <param name="assetName">The name of the texture asset to load.</param>
        /// <returns>The texture asset.</returns>
        ITexture LoadTexture(string assetName);

        /// <summary>
        /// Loads a font.
        /// </summary>
        /// <param name="assetName">The name of the font asset to load.</param>
        /// <returns>The font asset.</returns>
        IFont LoadFont(string assetName);
    }
}
