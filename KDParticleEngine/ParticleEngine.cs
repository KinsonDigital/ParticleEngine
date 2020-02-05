﻿using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDParticleEngine
{
    /// <summary>
    /// Manages multiple <see cref="Particle"/>s with various settings that dictate
    /// how all of the <see cref="Particle"/>s behave and look on the screen.
    /// </summary>
    public class ParticleEngine<Texture> where Texture : class
    {
        #region Private Fields
        private readonly List<ParticlePool<Texture>> _particlePools = new List<ParticlePool<Texture>>();
        private readonly ITextureLoader<Texture> _textureLoader;
        private readonly IRandomizerService _randomizer;
        private bool _enabled = true;
        private bool _texturesLoaded;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticleEngine"/>.
        /// </summary>
        public ParticleEngine(ITextureLoader<Texture> textureLoader, IRandomizerService randomizer)
        {
            _textureLoader = textureLoader;
            _randomizer = randomizer;
        }
        #endregion


        #region Props
        /// <summary>
        /// Gets all of the particle pools.
        /// </summary>
        public ParticlePool<Texture>[] ParticlePools => _particlePools.ToArray();

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
        /// Returns a value indicating if the list of <see cref="ParticleEngine{ITexture}"/>
        /// <see cref="Texture"/>s is readonly.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Returns a value indicating if the list of <see cref="ParticleEngine{ITexture}"/>
        /// <see cref="Texture"/>s has a fixed size.
        /// </summary>
        public bool IsFixedSize => false;

        /// <summary>
        /// Returns a value indicating if the list of <see cref="Texture"/>s is syncrhonized
        /// for multi-threaded operations.
        /// </summary>
        public bool IsSynchronized => false;
        #endregion


        #region Public Methods
        /// <summary>
        /// Creates a particle pool using the given particle <paramref name="effect"/>.
        /// </summary>
        /// <param name="effect">The particle effect for the pool to use.</param>
        public void CreatePool(ParticleEffect effect) => _particlePools.Add(new ParticlePool<Texture>(_textureLoader, effect, _randomizer));


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
        /// <param name="timeElapsed">The amount of time that has passed in the <see cref="Engine"/> since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            if (!_texturesLoaded)
                throw new Exception("The textures must be loaded first.");

            if (!Enabled)
                return;

            _particlePools.ForEach(p => p.Update(timeElapsed));
        }
        #endregion
    }
}
