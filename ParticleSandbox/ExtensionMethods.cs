using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSandbox
{
    public static class ExtensionMethods
    {
        public static ParticleTexture LoadTexture(this ContentManager content, string assetName)
        {
            return new ParticleTexture(content.Load<Texture2D>(assetName));
        }
    }
}
