// <copyright file="Texture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    [ExcludeFromCodeCoverage]
    public class Texture : ITexture
    {

        internal Texture(Texture2D internalTexture)
        {
            this.InternalTexture = internalTexture;
        }

        public Texture2D InternalTexture { get; private set; }

        public int Width => InternalTexture.Width;

        public int Height => InternalTexture.Height;

        public void GetData(Color[] data)
        {
            InternalTexture.GetData(data);
        }

        public void SetData(Color[] data)
        {
            InternalTexture.SetData(data);
        }
    }
}
