﻿// <copyright file="ParticleEngine.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace KDParticleEngine
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;

    /// <summary>
    /// Manages multiple <see cref="Particle"/>s with various settings that dictate
    /// how all of the <see cref="Particle"/>s behave and look on the screen.
    /// </summary>
    public class ParticleEngine : IDisposable
    {
        private readonly List<ParticlePool<IParticleTexture>> particlePools = new List<ParticlePool<IParticleTexture>>();
        private readonly ITextureLoader<IParticleTexture> textureLoader;
        private readonly IRandomizerService randomizer;
        private bool enabled = true;
        private bool texturesLoaded;
        private bool disposedValue = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEngine"/> class.
        /// </summary>
        /// <param name="textureLoader">Loads particle textures.</param>
        /// <param name="randomizer">Randomizes numbers.</param>
        public ParticleEngine(ITextureLoader<IParticleTexture> textureLoader, IRandomizerService randomizer)
        {
            this.textureLoader = textureLoader;
            this.randomizer = randomizer;
        }

        /// <summary>
        /// Gets all of the particle pools.
        /// </summary>
        public ReadOnlyCollection<ParticlePool<IParticleTexture>> ParticlePools
            => new ReadOnlyCollection<ParticlePool<IParticleTexture>>(this.particlePools.ToArray());

        /// <summary>
        /// Gets or sets a value indicating whether the engine is enabled or disabled.
        /// </summary>
        public bool Enabled
        {
            get => this.enabled;
            set
            {
                this.enabled = value;

                // If the engine is disabled, kill all the particles
                if (!this.enabled)
                    KillAllParticles();
            }
        }

        /// <summary>
        /// Creates a particle pool using the given particle <paramref name="effect"/>.
        /// </summary>
        /// <param name="effect">The particle effect for the pool to use.</param>
        /// <param name="behaviorFactory">The factory used for creating behaviors.</param>
        public void CreatePool(ParticleEffect effect, IBehaviorFactory behaviorFactory) => this.particlePools.Add(new ParticlePool<IParticleTexture>(behaviorFactory, this.textureLoader, effect, this.randomizer));

        /// <summary>
        /// Clears all of the current existing pools.
        /// </summary>
        /// <remarks>This will properly dispose of the texture for each pool.</remarks>
        public void ClearPools()
        {
            foreach (var pool in this.particlePools)
                pool.Dispose();

            this.particlePools.Clear();
        }

        /// <summary>
        /// Loads all of the textures for each <see cref="ParticlePool"/>
        /// in the engine.
        /// </summary>
        public void LoadTextures()
        {
            foreach (var pool in this.particlePools)
            {
                pool.LoadTexture();
            }

            this.texturesLoaded = true;
        }

        /// <summary>
        /// Kills all of the particles.
        /// </summary>
        public void KillAllParticles() => this.particlePools.ForEach(p => p.KillAllParticles());

        /// <summary>
        /// Updates all of the <see cref="Particle"/>s.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has passed since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            if (!this.texturesLoaded)
                throw new Exception("The textures must be loaded first.");

            if (!Enabled)
                return;

            this.particlePools.ForEach(p => p.Update(timeElapsed));
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposedValue)
            {
                if (disposing)
                {
                    foreach (var pool in ParticlePools)
                        pool.Dispose();
                }

                this.disposedValue = true;
            }
        }
    }
}
