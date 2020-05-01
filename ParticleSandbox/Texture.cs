using KDParticleEngine;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParticleSandbox
{
    public class Texture : IParticleTexture
    {
        #region Private Fields
        private readonly Texture2D _texture2D;
        #endregion


        #region Constructors
        public Texture(Texture2D texture2D)
        {
            _texture2D = texture2D;
        }
        #endregion


        #region Props
        public int Width => _texture2D.Width;

        public int Height => _texture2D.Height;

        public Texture2D MonoTexture => _texture2D;
        #endregion


        #region Public Methods
        public void Dispose()
        {
            //TODO: Add disposable pattern
        }
        #endregion
    }
}
