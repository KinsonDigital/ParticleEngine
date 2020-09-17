// <copyright file="ITexture.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;

    public interface ITexture
    {
        int Width { get; }

        int Height { get; }

        Texture2D InternalTexture { get; }

        void GetData(Color[] data);

        void SetData(Color[] data);
    }
}
