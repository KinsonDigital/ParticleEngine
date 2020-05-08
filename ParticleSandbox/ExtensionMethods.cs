using Microsoft.Xna.Framework;
using System.Drawing;
using XNARect = Microsoft.Xna.Framework.Rectangle;
using XNAColor = Microsoft.Xna.Framework.Color;
using KDParticleEngine;

namespace ParticleSandbox
{
    public static class ExtensionMethods
    {
        public static Vector2 ToVector2(this PointF point) => new Vector2(point.X, point.Y);


        public static XNAColor ToXNAColor(this ParticleColor clr) => new XNAColor(clr.R, clr.G, clr.B, clr.A);

        public static XNARect GetSrcRect(this IParticleTexture texture)
        {
            return new XNARect(0, 0, texture.Width, texture.Height);
        }


        public static Vector2 GetOriginAsCenter(this IParticleTexture texture) => new Vector2(texture.Width / 2, texture.Height / 2);
    }
}
