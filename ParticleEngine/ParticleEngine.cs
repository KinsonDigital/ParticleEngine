// <copyright file="ParticleEngine.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine
{
    using System;
    using System.Collections.Generic;
    using ParticleEngine.Behaviors;
    using ParticleEngine.Services;

    /// <summary>
    /// Manages multiple <see cref="Particle"/>s with various settings that dictate
    /// how all of the <see cref="Particle"/>s behave and look on the screen.
    /// </summary>
    public class ParticleEngine : IDisposable
    {
        private readonly List<ParticlePool<IParticleTexture>> _particlePools = new List<ParticlePool<IParticleTexture>>();
        private readonly ITextureLoader<IParticleTexture> _textureLoader;
        private readonly IRandomizerService _randomizer;
        private bool _enabled = true;
        private bool _texturesLoaded;
        private bool _disposedValue = false;

        /// <summary>
        /// Creates a new instance of <see cref="ParticleEngine"/>.
        /// </summary>
        public ParticleEngine(ITextureLoader<IParticleTexture> textureLoader, IRandomizerService randomizer)
        {
            _textureLoader = textureLoader;
            _randomizer = randomizer;
        }

        /// <summary>
        /// Gets all of the particle pools.
        /// </summary>
        public ParticlePool<IParticleTexture>[] ParticlePools => _particlePools.ToArray();

        /// <summary>
        /// Gets or sets a value indicating if the engine is enabled or disabled.
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set
            {
                _enabled = value;

                //If the engine is disabled, kill all the particles
                if (!_enabled)
                    KillAllParticles();
            }
        }

        /// <summary>
        /// Creates a particle pool using the given particle <paramref name="effect"/>.
        /// </summary>
        /// <param name="effect">The particle effect for the pool to use.</param>
        /// <param name="behaviorFactory">The factory used for creating behaviors.</param>
        public void CreatePool(ParticleEffect effect, IBehaviorFactory behaviorFactory) => _particlePools.Add(new ParticlePool<IParticleTexture>(behaviorFactory, _textureLoader, effect, _randomizer));

        /// <summary>
        /// Clears all of the current existing pools.
        /// </summary>
        /// <remarks>This will properly dispose of the texture for each pool.</remarks>
        public void ClearPools()
        {
            foreach (var pool in _particlePools)
                pool.Dispose();

            _particlePools.Clear();
        }

        /// <summary>
        /// Loads all of the textures for each <see cref="ParticlePool"/>
        /// in the engine.
        /// </summary>
        public void LoadTextures()
        {
            foreach (var pool in _particlePools)
            {
                pool.LoadTexture();
            }

            _texturesLoaded = true;
        }

        /// <summary>
        /// Kills all of the particles.
        /// </summary>
        public void KillAllParticles() => _particlePools.ForEach(p => p.KillAllParticles());

        /// <summary>
        /// Updates all of the <see cref="Particle"/>s.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has passed since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            if (!_texturesLoaded)
                throw new Exception("The textures must be loaded first.");

            if (!Enabled)
                return;

            _particlePools.ForEach(p => p.Update(timeElapsed));
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose() => Dispose(true);

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// <paramref name="disposing">If true, will dispose of managed resources.</paramref>
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    foreach (var pool in ParticlePools)
                        pool.Dispose();
                }

                _disposedValue = true;
            }
        }
    }
}
