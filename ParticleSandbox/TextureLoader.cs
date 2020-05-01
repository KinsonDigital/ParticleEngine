using KDParticleEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSandbox
{
    public class TextureLoader : ITextureLoader<IParticleTexture>
    {
        private readonly ContentManager _content;


        public TextureLoader(ContentManager content) => _content = content;
        

        public IParticleTexture LoadTexture(string textureName)
        {
            var texture2D = _content.Load<Texture2D>(textureName);


            return new Texture(texture2D);
        }
    }
}
