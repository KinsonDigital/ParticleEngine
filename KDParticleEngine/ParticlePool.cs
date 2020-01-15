using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace KDParticleEngine
{
    public class ParticlePool
    {
        #region Public Events
        /// <summary>
        /// Occurs every time the total living particles has changed.
        /// </summary>
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;
        #endregion


        #region Private Fields
        private readonly List<Particle> _particles = new List<Particle>();
        private readonly List<IBehavior> _behaviors = new List<IBehavior>();//TODO: Remove this
        private readonly IRandomizerService _randomizer;
        private int _spawnRate;
        private int _spawnRateElapsed = 0;
        #endregion


        #region Constructors
        //TODO: Finish adding code docs
        public ParticlePool(ParticleEffect setup, IRandomizerService randomizer)
        {
            Effect = setup;
            _randomizer = randomizer;

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

                Effect.Update(_particles[i], timeElapsed);
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
                    _particles[i].Position = Effect.SpawnLocation;//WORKS
                    _particles[i].IsAlive = true;//WORKS

                    _particles[i].Size = 1f;
                    _particles[i].TintColor = Color.White;


                    _particles.ForEach(p => Effect.ResetBehaviors(p.ID));
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
                return _randomizer.GetValue(Effect.SpawnRateMin, Effect.SpawnRateMax);


            return _randomizer.GetValue(Effect.SpawnRateMax, Effect.SpawnRateMin);
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
            var position = Effect.SpawnLocation;

            var velocity = Effect.UseRandomVelocity ? GetRandomVelocity() : Effect.ParticleVelocity;

            var angle = GetRandomAngle();

            var angularVelocity = GetRandomAngularVelocity();

            var color = GetRandomColor();

            var size = GetRandomSize();

            var lifeTime = GetRandomLifeTime();


            var newId = GetNewParticleId();

            Effect.CreateParticleBehaviors(newId);


            return new Particle()
            {
                ID = newId
            };
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Velocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private PointF GetRandomVelocity()
        {
            var velXRandomResult = _randomizer.GetValue(Effect.VelocityXMin, Effect.VelocityXMax);
            var velYRandomResult = _randomizer.GetValue(Effect.VelocityYMin, Effect.VelocityYMax);


            return new PointF(velXRandomResult,
                              velYRandomResult);
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Angle"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngle() => _randomizer.GetValue(Effect.AngleMin, Effect.AngleMax);


        /// <summary>
        /// Returns a random <see cref="Particle.AngularVelocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngularVelocity() => _randomizer.GetValue(Effect.AngularVelocityMin, Effect.AngularVelocityMax) * (_randomizer.FlipCoin() ? 1 : -1);


        /// <summary>
        /// Returns a random <see cref="Particle.TintColor"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            if (Effect.UseColorsFromList)
            {
                return Effect.TintColors is null || Effect.TintColors.Length == 0 ? Color.FromArgb(255, 255, 255, 255) : Effect.TintColors[_randomizer.GetValue(0, Effect.TintColors.Length - 1)];
            }
            else
            {
                var red = Effect.RedMin <= Effect.RedMax ?
                    (byte)_randomizer.GetValue(Effect.RedMin, Effect.RedMax) :
                    (byte)_randomizer.GetValue(Effect.RedMax, Effect.RedMin);
                var green = Effect.GreenMin <= Effect.GreenMax ?
                    (byte)_randomizer.GetValue(Effect.GreenMin, Effect.GreenMax) :
                    (byte)_randomizer.GetValue(Effect.GreenMax, Effect.GreenMin);
                var blue = Effect.BlueMin <= Effect.BlueMax ?
                    (byte)_randomizer.GetValue(Effect.BlueMin, Effect.BlueMax) :
                    (byte)_randomizer.GetValue(Effect.BlueMax, Effect.BlueMin);

                return Color.FromArgb(255, red, green, blue);
            }
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Size"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomSize() => _randomizer.GetValue(Effect.SizeMin, Effect.SizeMax);


        /// <summary>
        /// Returns a random <see cref="Particle.LifeTime"/> for a spawned <see cref="Particle"/>.
        /// If the max is less than the min, the <see cref="Particle.LifeTime"/> will still be chosen
        /// randomly between the two values.
        /// </summary>
        /// <returns></returns>
        private int GetRandomLifeTime()
        {
            if (Effect.LifeTimeMin <= Effect.LifeTimeMax)
                return _randomizer.GetValue(Effect.LifeTimeMin, Effect.LifeTimeMax);


            return _randomizer.GetValue(Effect.LifeTimeMax, Effect.LifeTimeMin);
        }


        /// <summary>
        /// Gets a new particle ID.
        /// </summary>
        /// <returns></returns>
        private int GetNewParticleId() => _particles.Count <= 0 ? 0 : _particles.Max(p => p.ID) + 1;


        private bool IsEasingBehavior(IBehavior behavior)
        {
            var behaviorType = behavior.GetType();

            return behaviorType.IsSubclassOf(typeof(EasingBehavior));
        }
        #endregion
    }
}
