// <copyright file="ITextureLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine
{
    /// <summary>
    /// Loads textures.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ITextureLoader<T>
        where T : class
    {
        /// <summary>
        /// Loads and returns a texture with the given <paramref name="imageFilePath"/>.
        /// </summary>
        /// <param name="imageFilePath">The file path of the image to load.</param>
        /// <returns>The texture.</returns>
        T LoadTexture(string imageFilePath);
    }
}
