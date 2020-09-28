// <copyright file="ParticleTextureLoader.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using KDParticleEngine;

    /// <summary>
    /// Loads a particle texture for rendering to the screen.
    /// </summary>
    public class ParticleTextureLoader : ITextureLoader<IParticleTexture>
    {
        private readonly IContentLoader contentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleTextureLoader"/> class.
        /// </summary>
        /// <param name="contentLoader">The content loader used to load the texture content.</param>
        public ParticleTextureLoader(IContentLoader contentLoader) => this.contentLoader = contentLoader;

        /// <inheritdoc/>
        public IParticleTexture LoadTexture(string assetName)
        {
            var loadedTexture = this.contentLoader.LoadTexture(assetName);

            return new ParticleTexture(loadedTexture);
        }
    }
}
