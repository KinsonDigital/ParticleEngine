using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KDParticleEngine
{
    /// <summary>
    /// Contains a number of resusable particles with a given particle effect applied to them.
    /// </summary>
    public class ParticlePool<Texture> where Texture : class
    {
        #region Public Events
        /// <summary>
        /// Occurs every time the total living particles has changed.
        /// </summary>
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;
        #endregion


        #region Private Fields
        private readonly List<Particle> _particles = new List<Particle>();
        private readonly IRandomizerService _randomService;
        private readonly ITextureLoader<Texture> _textureLoader;
        private int _spawnRate;
        private int _spawnRateElapsed = 0;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticlePool"/>.
        /// </summary>
        /// <param name="effect"></param>
        /// <param name="randomizer"></param>
        public ParticlePool(IBehaviorFactory behaviorFactory, ITextureLoader<Texture> textureLoader, ParticleEffect effect, IRandomizerService randomizer)
        {
            _textureLoader = textureLoader;
            Effect = effect;
            _randomService = randomizer;

            CreateAllParticles(behaviorFactory);
        }
        #endregion


        #region Props
        /// <summary>
        /// Gets current total number of living <see cref="Particle"/>s.
        /// </summary>
        public int TotalLivingParticles => _particles.Count(p => p.IsAlive);

        /// <summary>
        /// Gets the current total number of dead <see cref="Particle"/>s.
        /// </summary>
        public int TotalDeadParticles => _particles.Count(p => p.IsDead);

        /// <summary>
        /// Gets the list of particle in the pool.
        /// </summary>
        public Particle[] Particles => _particles.ToArray();

        /// <summary>
        /// Gets the particle effect of the pool.
        /// </summary>
        public ParticleEffect Effect { get; private set; }

        /// <summary>
        /// Gets or sets the texture of the particles in the pool.
        /// </summary>
        public Texture? PoolTexture { get; private set; }
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the particle pool.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has passed in the <see cref="Engine"/> since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            _spawnRateElapsed += (int)timeElapsed.TotalMilliseconds;

            //If the amount of time to spawn a new particle has passed
            if (_spawnRateElapsed >= _spawnRate)
            {
                _spawnRate = GetRandomSpawnRate();

                SpawnNewParticle();

                _spawnRateElapsed = 0;
            }

            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].IsDead)
                    continue;

                _particles[i].Update(timeElapsed);
            }
        }


        /// <summary>
        /// Kills all of the particles.
        /// </summary>
        public void KillAllParticles() => _particles.ForEach(p => p.IsDead = true);


        /// <summary>
        /// Loads the texture for the pool to use for rendering the particles.
        /// </summary>
        public void LoadTexture() => PoolTexture = _textureLoader.LoadTexture(Effect.ParticleTextureName);


        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is ParticlePool<Texture> pool))
                return false;


            return TotalLivingParticles == pool.TotalLivingParticles &&
                TotalDeadParticles == pool.TotalDeadParticles &&
                _particles.Count == pool.Particles.Length &&
                Effect == pool.Effect;
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Spawns a new <see cref="Particle"/>.  This simple finds the first dead <see cref="Particle"/> and
        /// sets it back to alive and sets all of its parameters to random values.
        /// </summary>
        private void SpawnNewParticle()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].IsDead)
                {
                    _particles[i].Position = Effect.SpawnLocation;

                    _particles[i].Reset();
                }
            }
        }


        /// <summary>
        /// Returns a random time in milliseconds that the <see cref="Particle"/> will be spawned next.
        /// </summary>
        /// <returns></returns>
        private int GetRandomSpawnRate()
        {
            if (Effect.SpawnRateMin <= Effect.SpawnRateMax)
                return _randomService.GetValue(Effect.SpawnRateMin, Effect.SpawnRateMax);


            return _randomService.GetValue(Effect.SpawnRateMax, Effect.SpawnRateMin);
        }


        /// <summary>
        /// Generates all of the particles.
        /// </summary>
        private void CreateAllParticles(IBehaviorFactory behaviorFactory)
        {
            _particles.Clear();

            for (int i = 0; i < Effect.TotalParticlesAliveAtOnce; i++)
            {
                _particles.Add(new Particle(behaviorFactory.CreateBehaviors(Effect.BehaviorSettings, _randomService)));
            }
        }
        #endregion
    }
}
