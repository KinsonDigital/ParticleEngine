using KDParticleEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleSandbox
{
    /// <summary>
    /// Loads textures.
    /// </summary>
    /// <typeparam name="T">The type of particle texture to load.</typeparam>
    public class TextureLoader : ITextureLoader<Texture2D>
    {
        #region Private Fields
        private readonly ContentManager _content;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="TextureLoader"/>.
        /// </summary>
        /// <param name="content">The content manager used to load images.</param>
        public TextureLoader(ContentManager content) => this._content = content;
        #endregion


        #region Public Methods
        /// <summary>
        /// Loads and returns a texture with the given <paramref name="imageFilePath"/>.
        /// </summary>
        /// <param name="imageFilePath">The file path of the image to load.</param>
        /// <returns></returns>
        public Texture2D LoadTexture(string imageFilePath) => this._content.Load<Texture2D>(imageFilePath);
        #endregion
    }
}
