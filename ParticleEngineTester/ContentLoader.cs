// <copyright file="ContentLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework.Content;
    using Microsoft.Xna.Framework.Graphics;

    [ExcludeFromCodeCoverage]
    public class ContentLoader : IContentLoader
    {
        private ContentManager contentManager;

        public ContentLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public ITexture Load(string assetName)
        {
            var internalTexture = this.contentManager.Load<Texture2D>(assetName);

            return new Texture(internalTexture);
        }
    }
}
