using Microsoft.Xna.Framework;
using System.Drawing;
using XNARect = Microsoft.Xna.Framework.Rectangle;
using XNAColor = Microsoft.Xna.Framework.Color;
using ParticleEngine;

namespace ParticleSandbox
{
    /// <summary>
    /// Provides helper methods for the code base.
    /// </summary>
    public static class ExtensionMethods
    {
        #region Public Methods
        /// <summary>
        /// Converts a <see cref="PointF"/> to a <see cref="Vector2"/>.
        /// </summary>
        /// <param name="point">The <see cref="PointF"/> structure to convert.</param>
        /// <returns></returns>
        public static Vector2 ToVector2(this PointF point) => new Vector2(point.X, point.Y);

        
        /// <summary>
        /// Converts a <see cref="ParticleColor"/> to a <see cref="XNAColor"/>.
        /// </summary>
        /// <param name="clr">The color to convert.</param>
        /// <returns></returns>
        public static XNAColor ToXNAColor(this ParticleColor clr) => new XNAColor(clr.R, clr.G, clr.B, clr.A);


        /// <summary>
        /// Returns a rectangle of a <see cref="IParticleTexture"/>.
        /// </summary>
        /// <param name="texture">The texture to construct a rectagle from.</param>
        /// <returns></returns>
        public static XNARect GetSrcRect(this IParticleTexture texture) => new XNARect(0, 0, texture.Width, texture.Height);


        /// <summary>
        /// Returns the origin location of the given <see cref="IParticleTexture"/>.
        /// </summary>
        /// <param name="texture">The texture to calculate an origin from.</param>
        /// <returns></returns>
        public static Vector2 GetOriginAsCenter(this IParticleTexture texture) => new Vector2(texture.Width / 2, texture.Height / 2);
        #endregion
    }
}
