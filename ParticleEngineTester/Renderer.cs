// <copyright file="Renderer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [ExcludeFromCodeCoverage]
    public class Renderer : IRenderer
    {
        private SpriteBatch? spriteBatch;

        internal Renderer(SpriteBatch spriteBatch)
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

        public void DrawText(IFont font, string text, Vector2 position, Color color)
        {
            if (this.spriteBatch is null)
            {
                throw new NullReferenceException($"The internal sprite batch cannot be null.");
            }

            text = string.IsNullOrEmpty(text) ? string.Empty : text;

            this.spriteBatch.DrawString(font.InternalFont, text, position, color);
        }

        public void DrawLine(float x1, float y1, float x2, float y2, Color color)
        {
            this.spriteBatch.DrawLine(x1, y1, x2, y2, color);
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
