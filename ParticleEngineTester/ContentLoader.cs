// <copyright file="ContentLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Loads various kinds of content.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ContentLoader : IContentLoader
    {
        private readonly ContentManager contentManager;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContentLoader"/> class.
        /// </summary>
        /// <param name="contentManager">The mnogame content loader.</param>
        public ContentLoader(ContentManager contentManager) => this.contentManager = contentManager;

        /// <inheritdoc/>
        public ITexture LoadTexture(string assetName)
        {
            var internalTexture = this.contentManager.Load<Texture2D>(assetName);

            return new Texture(internalTexture);
        }

        /// <inheritdoc/>
        public IFont LoadFont(string assetName)
        {
            var internalFont = this.contentManager.Load<SpriteFont>(assetName);

            return new Font(internalFont);
        }

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
                this.contentManager.Dispose();
            }

            this.isDisposed = true;
        }
    }
}
