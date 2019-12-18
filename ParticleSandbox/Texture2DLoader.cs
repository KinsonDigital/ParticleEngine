using KDParticleEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSandbox
{
    public class Texture2DLoader : ITextureLoader<Texture2D>
    {
        private ContentManager _content;


        public Texture2DLoader(ContentManager content) => _content = content;
        

        public Texture2D LoadTexture(string textureName)
        {
            return _content.Load<Texture2D>(textureName);
        }
    }
}
