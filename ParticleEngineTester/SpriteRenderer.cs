// <copyright file="SpriteRenderer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [ExcludeFromCodeCoverage]
    public class SpriteRenderer : ISpriteRenderer
    {
        private SpriteBatch? spriteBatch;

        internal SpriteRenderer(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
        }

        public void Draw(ITexture texture, Vector2 position, Color color)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.Draw(texture.InternalTexture, position, color);
        }

        public void End()
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            this.spriteBatch.End();
        }
    }
}
