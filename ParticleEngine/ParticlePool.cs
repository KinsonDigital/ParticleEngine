// <copyright file="ParticlePool.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace KDParticleEngine
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;

    /// <summary>
    /// Contains a number of reusable particles with a given particle effect applied to them.
    /// </summary>
    /// <typeparam name="TTexture">The texture for the particles in the pool.</typeparam>
    public class ParticlePool<TTexture> : IDisposable
        where TTexture : class, IParticleTexture
    {
        private readonly IRandomizerService randomService;
        private readonly ITextureLoader<TTexture> textureLoader;
        private List<Particle> particles = new List<Particle>();
        private bool disposedValue = false;
        private int spawnRate;
        private double spawnRateElapsed = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticlePool{Texture}"/> class.
        /// </summary>
        /// <param name="behaviorFactory">The factory used for creating new behaviors for each particle.</param>
        /// <param name="textureLoader">Loads the textures for the <see cref="ParticlePool{Texture}"/>.</param>
        /// <param name="effect">The particle effect to be applied to all of the particles in the pool.</param>
        /// <param name="randomizer">Used for generating random values when a particle is spawned.</param>
        public ParticlePool(IBehaviorFactory behaviorFactory, ITextureLoader<TTexture> textureLoader, ParticleEffect effect, IRandomizerService randomizer)
        {
            if (behaviorFactory is null)
                throw new ArgumentNullException(nameof(behaviorFactory), "The parameter must not be null.");

            this.textureLoader = textureLoader;
            Effect = effect;
            this.randomService = randomizer;

            CreateAllParticles(behaviorFactory);
        }

        // TODO: Implement code to make use of invoking this event.

        /// <summary>
        /// Occurs every time the total amount of living particles has changed.
        /// </summary>
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;

        /// <summary>
        /// Gets current total number of living <see cref="Particle"/>s.
        /// </summary>
        public int TotalLivingParticles => this.particles.Count(p => p.IsAlive);

        /// <summary>
        /// Gets the current total number of dead <see cref="Particle"/>s.
        /// </summary>
        public int TotalDeadParticles => this.particles.Count(p => p.IsDead);

        /// <summary>
        /// Gets the list of particle in the pool.
        /// </summary>
        public ReadOnlyCollection<Particle> Particles => new ReadOnlyCollection<Particle>(this.particles.ToArray());

        /// <summary>
        /// Gets the particle effect of the pool.
        /// </summary>
        public ParticleEffect Effect { get; private set; }

        /// <summary>
        /// Gets the texture of the particles in the pool.
        /// </summary>
        public TTexture? PoolTexture { get; private set; }

        /// <summary>
        /// Updates the particle pool.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has passed since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            this.spawnRateElapsed += timeElapsed.TotalMilliseconds;

            // If the amount of time to spawn a new particle has passed
            if (this.spawnRateElapsed >= this.spawnRate)
            {
                this.spawnRate = GetRandomSpawnRate();

                SpawnNewParticle();

                this.spawnRateElapsed = 0;
            }

            for (var i = 0; i < this.particles.Count; i++)
            {
                if (this.particles[i].IsDead)
                    continue;

                this.particles[i].Update(timeElapsed);
            }
        }

        /// <summary>
        /// Kills all of the particles.
        /// </summary>
        public void KillAllParticles() => this.particles.ForEach(p => p.IsDead = true);

        /// <summary>
        /// Loads the texture for the pool to use for rendering the particles.
        /// </summary>
        public void LoadTexture() => PoolTexture = this.textureLoader.LoadTexture(Effect.ParticleTextureName);

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is ParticlePool<TTexture> pool))
                return false;

            return TotalLivingParticles == pool.TotalLivingParticles &&
                TotalDeadParticles == pool.TotalDeadParticles &&
                this.particles.Count == pool.Particles.Count &&
                Effect == pool.Effect;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(TotalLivingParticles.GetHashCode(), TotalDeadParticles.GetHashCode(), Effect.GetHashCode(), PoolTexture?.GetHashCode());

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
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
            if (this.disposedValue)
                return;

            // Dispose of managed resources
            if (disposing)
            {
                if (!(PoolTexture is null))
                    PoolTexture.Dispose();

                this.disposedValue = false;
                this.spawnRate = 0;
                this.spawnRateElapsed = 0;
                this.particles = new List<Particle>();
            }

            this.disposedValue = true;
        }

        /// <summary>
        /// Resets all of the particles.
        /// </summary>
        private void SpawnNewParticle()
        {
            for (var i = 0; i < this.particles.Count; i++)
            {
                if (this.particles[i].IsDead)
                {
                    this.particles[i].Position = Effect.SpawnLocation;
                    this.particles[i].Reset();
                    break;
                }
            }
        }

        /// <summary>
        /// Returns a random time in milliseconds that the <see cref="Particle"/> will be spawned next.
        /// </summary>
        /// <returns>A randomized spawn rate.</returns>
        private int GetRandomSpawnRate()
        {
            if (Effect.SpawnRateMin <= Effect.SpawnRateMax)
                return this.randomService.GetValue(Effect.SpawnRateMin, Effect.SpawnRateMax);

            return this.randomService.GetValue(Effect.SpawnRateMax, Effect.SpawnRateMin);
        }

        /// <summary>
        /// Generates all of the particles.
        /// </summary>
        private void CreateAllParticles(IBehaviorFactory behaviorFactory)
        {
            this.particles.Clear();

            for (var i = 0; i < Effect.TotalParticlesAliveAtOnce; i++)
            {
                this.particles.Add(new Particle(behaviorFactory.CreateBehaviors(Effect.BehaviorSettings.ToArray(), this.randomService)));
            }
        }
    }
}
