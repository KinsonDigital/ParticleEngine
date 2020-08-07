using ParticleEngine.Behaviors;
using ParticleEngine.Services;
using System;
using System.Collections.Generic;

namespace ParticleEngine
{
    /// <summary>
    /// Contains a number of resusable particles with a given particle effect applied to them.
    /// </summary>
    public class ParticlePool<Texture> : IDisposable where Texture : class, IParticleTexture
    {
        /// <summary>
        /// Occurs every time the total amount of living particles has changed.
        /// </summary>
        //TODO: Implement code to make use of invoking this event.
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;

        private List<Particle> _particles = new List<Particle>();
        private readonly IRandomizerService _randomService;
        private readonly ITextureLoader<Texture> _textureLoader;
        private bool _disposedValue = false;
        private int _spawnRate;
        private double _spawnRateElapsed = 0;


        /// <summary>
        /// Creates a new instance of <see cref="ParticlePool"/>.
        /// </summary>
        /// <param name="behaviorFactory">The factory used for creating new behaviors for each particle.</param>
        /// <param name="textureLoader">Loads the textures for the <see cref="ParticlePool{Texture}"/></param>
        /// <param name="effect">The particle effect to be applied to all of the particles in the pool.</param>
        /// <param name="randomizer">Used for generating random values when a particle is spawned.</param>
        public ParticlePool(IBehaviorFactory behaviorFactory, ITextureLoader<Texture> textureLoader, ParticleEffect effect, IRandomizerService randomizer)
        {
            _textureLoader = textureLoader;
            Effect = effect;
            _randomService = randomizer;

            CreateAllParticles(behaviorFactory);
        }


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


        /// <summary>
        /// Updates the particle pool.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has passed since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            _spawnRateElapsed += timeElapsed.TotalMilliseconds;

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


        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(TotalLivingParticles.GetHashCode(), TotalDeadParticles.GetHashCode(), Effect.GetHashCode(), PoolTexture?.GetHashCode());


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
            if (_disposedValue)
                return;

            //Dispose of managed resources
            if (disposing)
            {
                if (!(PoolTexture is null))
                    PoolTexture.Dispose();

                _disposedValue = false;
                _spawnRate = 0;
                _spawnRateElapsed = 0;
                _particles = new List<Particle>();
            }

            _disposedValue = true;
        }


        /// <summary>
        /// Resets all of the particles.
        /// </summary>
        private void SpawnNewParticle()
        {
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].IsDead)
                {
                    _particles[i].Position = Effect.SpawnLocation;
                    _particles[i].Reset();
                    break;
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
    }
}
