using ParticleEngine;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ParticleSandbox
{
    /// <summary>
    /// Represents a texture that can be rendered.
    /// </summary>
    public class Texture : IParticleTexture
    {
        #region Private Fields
        private readonly Texture2D _texture2D;
        private bool _disposedValue;
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region Protected Methods
        protected virtual void Dispose(bool disposing)
        {
            if (_disposedValue)
                return;

            if (disposing)
                _texture2D.Dispose();


            _disposedValue = true;
        }
        #endregion
    }
}
