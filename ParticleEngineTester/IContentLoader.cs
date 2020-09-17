// <copyright file="IContentLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    public interface IContentLoader
    {
        ITexture LoadTexture(string assetName);

        IFont LoadFont(string assetName);
    }
}
