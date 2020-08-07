// <copyright file="IParticleTexture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine
{
    using System;

    /// <summary>
    /// Represents a single texture for a particle to be rendered.
    /// </summary>
    public interface IParticleTexture : IDisposable
    {
        /// <summary>
        /// Gets or sets the width of the texture.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets or sets the height of the texture.
        /// </summary>
        public int Height { get; }
    }
}
