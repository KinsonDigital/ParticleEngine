using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;

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
        #endregion


        #region Public Methods
        /// <summary>
        /// Creates a particle pool using the given particle <paramref name="effect"/>.
        /// </summary>
        /// <param name="effect">The particle effect for the pool to use.</param>
        /// <param name="behaviorFactory">The factory used for creating behaviors.</param>
        public void CreatePool(ParticleEffect effect, IBehaviorFactory behaviorFactory) => _particlePools.Add(new ParticlePool<Texture>(behaviorFactory, _textureLoader, effect, _randomizer));


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
        #endregion
    }
}
