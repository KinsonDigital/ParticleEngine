// <copyright file="ITextureLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine
{
    /// <summary>
    /// Loads textures.
    /// </summary>
    /// <typeparam name="T">The type of texture to load.</typeparam>
    public interface ITextureLoader<T>
        where T : class
    {
        /// <summary>
        /// Loads and returns a texture with the given <paramref name="assetName"/>.
        /// </summary>
        /// <param name="assetName">The name of the asset to load.</param>
        /// <remarks>
        ///     The <paramref name="assetName"/> can be a simple name, file path or just a file name without the path.
        ///     The name is abstract and is left to the implementation on how to handle it.
        /// </remarks>
        /// <returns>The texture.</returns>
        T LoadTexture(string assetName);
    }
}
