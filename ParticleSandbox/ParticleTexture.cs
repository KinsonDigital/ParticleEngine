using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSandbox
{
    public class ParticleTexture
    {
        private Texture2D _texture;

        public ParticleTexture(Texture2D texture)
        {
            _texture = texture;
        }
    }
}
