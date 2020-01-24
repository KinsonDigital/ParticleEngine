﻿using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace KDParticleEngine
{
    public class ParticlePool
    {
        //DEBUGGING
        private Stopwatch _timer = new Stopwatch();
        private List<double> _timings = new List<double>();
        ///////////


        #region Public Events
        /// <summary>
        /// Occurs every time the total living particles has changed.
        /// </summary>
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;
        #endregion


        #region Private Fields
        private readonly List<Particle> _particles = new List<Particle>();
        private readonly IRandomizerService _randomService;
        private int _spawnRate;
        private int _spawnRateElapsed = 0;
        #endregion


        #region Constructors
        //TODO: Finish adding code docs
        public ParticlePool(ParticleEffect effect, IRandomizerService randomizer)
        {
            Effect = effect;
            _randomService = randomizer;

            CreateAllParticles();
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

        public Particle[] Particles => _particles.ToArray();

        public ParticleEffect Effect { get; private set; }
        #endregion


        #region Public Methods
        public void Update(TimeSpan timeElapsed)
        {
            //int totalTimings = 1000;

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

                //_timer.Start();

                _particles[i].Update(timeElapsed);

                //TODO: Removed. Effect.Update() code moved to Particle.Update()
                //Effect.Update(_particles[i], timeElapsed);

                //_timer.Stop();
                //_timings.Add(_timer.Elapsed.TotalMilliseconds);
                //_timer.Reset();


                //if (_timings.Count >= totalTimings)
                //{
                //    var maxValue = _timings.Max();

                //    _timings = _timings.Where(t => t < maxValue).ToList();
                //}

                //if (_timings.Count >= totalTimings - 1)
                //{
                //    var perfResult = _timings.Average();
                //}
            }
        }


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

                    var stop = true;
                }
            }
        }


        /// <summary>
        /// Kills all of the particles.
        /// </summary>
        public void KillAllParticles() => _particles.ForEach(p => p.IsDead = true);


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
        private void CreateAllParticles()
        {
            _particles.Clear();

            for (int i = 0; i < Effect.TotalParticlesAliveAtOnce; i++)
            {
                _particles.Add(CreateParticle());
            }
        }


        /// <summary>
        /// Generates a single <see cref="Particle"/> with random settings based on the <see cref="ParticleEngine"/>s
        /// range settings.
        /// </summary>
        /// <returns></returns>
        private Particle CreateParticle()
        {
            var newId = GetNewParticleId();


            return new Particle(Effect.BehaviorSettings, _randomService)
            {
                ID = newId
            };
        }


        /// <summary>
        /// Gets a new particle ID.
        /// </summary>
        /// <returns></returns>
        private int GetNewParticleId() => _particles.Count <= 0 ? 0 : _particles.Max(p => p.ID) + 1;
        #endregion
    }
}
