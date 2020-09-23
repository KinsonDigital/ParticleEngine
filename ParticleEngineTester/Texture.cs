// <copyright file="Texture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// A simple 2D texture that can be rendered.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Texture : ITexture
    {
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Texture"/> class.
        /// </summary>
        /// <param name="internalTexture">The internal monogame texture.</param>
        public Texture(Texture2D internalTexture) => InternalTexture = internalTexture;

        /// <inheritdoc/>
        public Texture2D InternalTexture { get; private set; }

        /// <inheritdoc/>
        public int Width => InternalTexture.Width;

        /// <inheritdoc/>
        public int Height => InternalTexture.Height;

        /// <inheritdoc/>
        public void GetData(Color[] data) => InternalTexture.GetData(data);

        /// <inheritdoc/>
        public void SetData(Color[] data) => InternalTexture.SetData(data);

        /// <inheritdoc/>
        [ExcludeFromCodeCoverage]
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        [ExcludeFromCodeCoverage]
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                InternalTexture.Dispose();
            }

            this.isDisposed = true;
        }
    }
}
