// <copyright file="IRenderer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface IRenderer
    {
        void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null);

        void Draw(ITexture texture, Vector2 position, Color color);

        void DrawText(IFont spriteFont, string text, Vector2 position, Color color);

        void DrawLine(float x1, float y1, float x2, float y2, Color color);

        void End();
    }
}
