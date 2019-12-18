using KDParticleEngine.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;

namespace KDParticleEngine
{
    /// <summary>
    /// Manages multiple <see cref="Particle"/>s with various settings that dictate
    /// how all of the <see cref="Particle"/>s behave and look on the screen.
    /// </summary>
    public class ParticleEngine<Texture> where Texture : class
    {
        //#region Public Events
        ///// <summary>
        ///// Occurs every time the total living particles has changed.
        ///// </summary>
        //public event EventHandler<EventArgs>? LivingParticlesCountChanged;
        //#endregion


        #region Private Fields
        private List<ParticlePool<Texture>> _particlePools = new List<ParticlePool<Texture>>();
        private readonly IRandomizerService _randomizer;
        private int _totalParticlesAliveAtOnce = 10;

        private float _angleMin;
        private float _angleMax = 360;
        private bool _enabled = true;

        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticleEngine"/>.
        /// </summary>
        public ParticleEngine(IRandomizerService randomizer)
        {
            _randomizer = randomizer;
        }
        #endregion


        #region Props
        /// <summary>
        /// Gets the list of particles in the engine.
        /// </summary>
        public Particle<Texture>[] Particles
        {
            get
            {
                var result = new List<Particle<Texture>>();

                _particlePools.ForEach(pool => result.AddRange(pool.Particles));


                return result.ToArray();
            }
        }


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
        /// Gets current total number of living <see cref="Particle"/>s.
        /// </summary>
        public int TotalLivingParticles => _particlePools.Sum(p => p.TotalLivingParticles);

        /// <summary>
        /// Gets the current total number of dead <see cref="Particle"/>s.
        /// </summary>
        public int TotalDeadParticles => _particlePools.Sum(p => p.TotalDeadParticles);

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
        public void AddSetup(ParticleSetup<Texture> setup)
        {
            _particlePools.Add(new ParticlePool<Texture>(setup, _randomizer));
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
            if (!Enabled)
                return;

            _particlePools.ForEach(p => p.Update(timeElapsed));
        }
        #endregion
    }
}
