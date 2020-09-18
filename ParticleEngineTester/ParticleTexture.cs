// <copyright file="ParticleTexture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using KDParticleEngine;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// A single particle texture to render to the screen.
    /// </summary>
    public class ParticleTexture : IParticleTexture
    {
        private readonly ITexture texture;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleTexture"/> class.
        /// </summary>
        /// <param name="texture">The particle texture that will be rendered.</param>
        public ParticleTexture(ITexture texture) => this.texture = texture;

        /// <summary>
        /// Gets a monogame texture to render to the screen.
        /// </summary>
        [ExcludeFromCodeCoverage]
        public Texture2D MonoTexture => this.texture.InternalTexture;

        /// <inheritdoc/>
        public int Width => this.texture.Width;

        /// <inheritdoc/>
        public int Height => this.texture.Height;

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                this.texture.Dispose();
            }

            this.isDisposed = true;
        }
    }
}
