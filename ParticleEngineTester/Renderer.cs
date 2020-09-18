// <copyright file="Renderer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Renders various things to the screen.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Renderer : IRenderer
    {
        private readonly SpriteBatch? spriteBatch;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Renderer"/> class.
        /// </summary>
        /// <param name="spriteBatch">The monogame sprite batch used for drawing.</param>
        public Renderer(SpriteBatch spriteBatch) => this.spriteBatch = spriteBatch;

        /// <inheritdoc/>
        public void Begin(
            SpriteSortMode sortMode = SpriteSortMode.Deferred,
            BlendState? blendState = null,
            SamplerState? samplerState = null,
            DepthStencilState? depthStencilState = null,
            RasterizerState? rasterizerState = null,
            Effect? effect = null,
            Matrix? transformMatrix = null)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        /// <inheritdoc/>
        public void Draw(ITexture texture, Vector2 position, Color color)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Draw(texture.InternalTexture, position, color);
        }

        /// <inheritdoc/>
        public void DrawText(IFont font, string text, Vector2 position, Color color)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            text = string.IsNullOrEmpty(text) ? string.Empty : text;

            this.spriteBatch.DrawString(font.InternalFont, text, position, color);
        }

        /// <inheritdoc/>
        public void Draw(ITexture texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Draw(
                texture.InternalTexture,
                destinationRectangle,
                sourceRectangle,
                color,
                rotation,
                origin,
                SpriteEffects.None,
                layerDepth);
        }

        /// <inheritdoc/>
        public void Draw(ParticleTexture texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Draw(
                texture.MonoTexture,
                destinationRectangle,
                sourceRectangle,
                color,
                rotation,
                origin,
                SpriteEffects.None,
                layerDepth);
        }

        /// <inheritdoc/>
        public void DrawLine(float x1, float y1, float x2, float y2, Color color) => this.spriteBatch?.DrawLine(x1, y1, x2, y2, color);

        /// <inheritdoc/>
        public void End()
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.End();
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
                this.spriteBatch?.Dispose();
            }

            this.isDisposed = true;
        }
    }
}
