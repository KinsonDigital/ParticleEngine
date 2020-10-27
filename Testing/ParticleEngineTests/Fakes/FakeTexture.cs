// <copyright file="FakeTexture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngineTests.Fakes
{
    using KDParticleEngine;

    /// <summary>
    /// Represents a fake <see cref="IParticleTexture"/> implementation for testing purposes.
    /// </summary>
    public class FakeTexture : IParticleTexture
    {
        /// <summary>
        /// Gets the width of the fake texture.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the fake texture.
        /// </summary>
        public int Height { get; }

        /// <inheritdoc/>
        public void Dispose()
        {
        }
    }
}
