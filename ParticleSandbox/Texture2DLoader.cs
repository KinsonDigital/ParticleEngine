using KDParticleEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

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
