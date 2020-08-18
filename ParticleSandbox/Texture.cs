using KDParticleEngine;
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
        private bool isDisposed;
        #endregion


        #region Constructors
        public Texture(Texture2D texture2D) => MonoTexture = texture2D;
        #endregion


        #region Props
        public int Width => MonoTexture.Width;

        public int Height => MonoTexture.Height;

        public Texture2D MonoTexture { get; }
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
            if (this.isDisposed)
                return;

            if (disposing)
                MonoTexture.Dispose();


            this.isDisposed = true;
        }
        #endregion
    }
}
