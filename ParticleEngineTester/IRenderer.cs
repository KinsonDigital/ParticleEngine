// <copyright file="IRenderer.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    /// <summary>
    /// Renders various things to the screen.
    /// </summary>
    public interface IRenderer : IDisposable
    {
        /// <summary>
        /// Begins a new sprite and text batch with the specified render state.
        /// </summary>
        /// <param name="sortMode">
        ///     The drawing order for sprite and text drawing
        ///     Microsoft.Xna.Framework.Graphics.SpriteSortMode.Deferred
        ///     by default.</param>
        /// <param name="blendState">
        ///     State of the blending. Uses Microsoft.Xna.Framework.Graphics.BlendState.AlphaBlend if null.
        /// </param>
        /// <param name="samplerState">
        ///  State of the sampler. Uses Microsoft.Xna.Framework.Graphics.SamplerState.LinearClamp if null.
        /// </param>
        /// <param name="depthStencilState">
        /// State of the depth-stencil buffer. Uses Microsoft.Xna.Framework.Graphics.DepthStencilState.None if null.
        /// </param>
        /// <param name="rasterizerState">
        /// State of the rasterization. Uses Microsoft.Xna.Framework.Graphics.RasterizerState.CullCounterClockwise if null.
        /// </param>
        /// <param name="effect">
        ///     A custom Microsoft.Xna.Framework.Graphics.Effect to override the default sprite
        ///     effect. Uses default sprite effect if null.
        /// </param>
        /// <param name="transformMatrix">
        ///     An optional matrix used to transform the sprite geometry. Uses Microsoft.Xna.Framework.Matrix.Identity if null.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        ///     Thrown if <see cref="IRenderer.Begin()"/> is called next time without previous <see cref="IRenderer.End()"/>.
        /// </exception>
        void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, Effect? effect = null, Matrix? transformMatrix = null);

        /// <summary>
        /// Draws the texture on the screen.
        /// </summary>
        /// <param name="texture">The texture to draw.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">The color mask.</param>
        void Draw(ITexture texture, Vector2 position, Color color);

        /// <summary>
        /// Draws the texture on the screen.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered.  if nul, draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this texture.</param>
        /// <param name="origin">Center of the rotation, 0,0 by default.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        void Draw(ITexture texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth);

        /// <summary>
        /// Draws the texture on the screen.
        /// </summary>
        /// <param name="texture">A texture.</param>
        /// <param name="destinationRectangle">The drawing bounds on screen.</param>
        /// <param name="sourceRectangle">An optional region on the texture which will be rendered.  if nul, draws full texture.</param>
        /// <param name="color">A color mask.</param>
        /// <param name="rotation">A rotation of this texture.</param>
        /// <param name="origin">Center of the rotation, 0,0 by default.</param>
        /// <param name="layerDepth">A depth of the layer of this sprite.</param>
        void Draw(ParticleTexture texture, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float layerDepth);

        /// <summary>
        /// Draws text on the screen.
        /// </summary>
        /// <param name="spriteFont">The font to draw.</param>
        /// <param name="text">The text which will be drawn.</param>
        /// <param name="position">The drawing location on screen.</param>
        /// <param name="color">The color mask.</param>
        void DrawText(IFont spriteFont, string text, Vector2 position, Color color);

        /// <summary>
        /// Draws a line onto the screen.
        /// </summary>
        /// <param name="x1">The X coordinate of the first point of the line.</param>
        /// <param name="y1">The Y coordinate of the first point of the line.</param>
        /// <param name="x2">The X coordinate of the second point of the line.</param>
        /// <param name="y2">The Y coordinate of the second point of the line.</param>
        /// <param name="color">The color of the line.</param>
        void DrawLine(float x1, float y1, float x2, float y2, Color color);

        /// <summary>
        /// Flushes all batched text and sprites to the screen.
        /// </summary>
        /// <remarks>
        ///     This command should be called after <see cref="IRenderer.Begin()"/> and drawing commands.
        /// </remarks>
        void End();
    }
}
