using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

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
        private readonly IRandomizerService _randomizer;
        private int _spawnRate;
        private int _spawnRateElapsed = 0;
        #endregion


        #region Constructors
        //TODO: Finish adding code docs
        public ParticlePool(ParticleEffect setup, IRandomizerService randomizer)
        {
            Setup = setup;
            _randomizer = randomizer;

            GenerateAllParticles();
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

        public ParticleEffect Setup { get; private set; }
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

                //Update the current particle
                _particles[i].Update(timeElapsed);

                //Apply all of the behaviors
                Setup.Behaviors.ForEach(b =>
                {
                    b.Update(_particles[i], timeElapsed);
                });

                //If the current particle's time to live has expired, kill the particle
                if (_particles[i].LifeTime <= 0)
                {
                    _particles[i].IsAlive = false;

                    //Invoke the engine updated event
                    LivingParticlesCountChanged?.Invoke(this, new EventArgs());
                }
            }
        }


        /// <summary>
        /// Spawns a new <see cref="Particle"/>.  This simple finds the first dead <see cref="Particle"/> and
        /// sets it back to alive and sets all of its parameters to random values.
        /// </summary>
        private void SpawnNewParticle()
        {
            //Find the first dead particle and bring it back to life
            for (int i = 0; i < _particles.Count; i++)
            {
                if (_particles[i].IsDead)
                {
                    _particles[i].Position = Setup.SpawnLocation;
                    _particles[i].Velocity = Setup.UseRandomVelocity ? GetRandomVelocity() : Setup.ParticleVelocity;
                    _particles[i].Angle = GetRandomAngle();
                    _particles[i].AngularVelocity = GetRandomAngularVelocity();
                    _particles[i].TintColor = GetRandomColor();
                    _particles[i].Size = GetRandomSize();
                    _particles[i].LifeTime = GetRandomLifeTime();
                    _particles[i].IsAlive = true;
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
            if (Setup.SpawnRateMin <= Setup.SpawnRateMax)
                return _randomizer.GetValue(Setup.SpawnRateMin, Setup.SpawnRateMax);


            return _randomizer.GetValue(Setup.SpawnRateMax, Setup.SpawnRateMin);
        }


        /// <summary>
        /// Generates all of the particles.
        /// </summary>
        private void GenerateAllParticles()
        {
            _particles.Clear();

            for (int i = 0; i < Setup.TotalParticlesAliveAtOnce; i++)
            {
                _particles.Add(GenerateParticle());
            }
        }


        /// <summary>
        /// Generates a single <see cref="Particle"/> with random settings based on the <see cref="ParticleEngine"/>s
        /// range settings.
        /// </summary>
        /// <returns></returns>
        private Particle GenerateParticle()
        {
            var position = Setup.SpawnLocation;

            var velocity = Setup.UseRandomVelocity ? GetRandomVelocity() : Setup.ParticleVelocity;

            var angle = GetRandomAngle();

            var angularVelocity = GetRandomAngularVelocity();

            var color = GetRandomColor();

            var size = GetRandomSize();

            var lifeTime = GetRandomLifeTime();


            return new Particle(position, velocity, angle, angularVelocity, color, size, lifeTime);
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Velocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private PointF GetRandomVelocity()
        {
            var velXRandomResult = _randomizer.GetValue(Setup.VelocityXMin, Setup.VelocityXMax);
            var velYRandomResult = _randomizer.GetValue(Setup.VelocityYMin, Setup.VelocityYMax);


            return new PointF(velXRandomResult,
                              velYRandomResult);
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Angle"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngle() => _randomizer.GetValue(Setup.AngleMin, Setup.AngleMax);


        /// <summary>
        /// Returns a random <see cref="Particle.AngularVelocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngularVelocity() => _randomizer.GetValue(Setup.AngularVelocityMin, Setup.AngularVelocityMax) * (_randomizer.FlipCoin() ? 1 : -1);


        /// <summary>
        /// Returns a random <see cref="Particle.TintColor"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            if (Setup.UseColorsFromList)
            {
                return Setup.TintColors is null || Setup.TintColors.Length == 0 ? Color.FromArgb(255, 255, 255, 255) : Setup.TintColors[_randomizer.GetValue(0, Setup.TintColors.Length - 1)];
            }
            else
            {
                var red = Setup.RedMin <= Setup.RedMax ?
                    (byte)_randomizer.GetValue(Setup.RedMin, Setup.RedMax) :
                    (byte)_randomizer.GetValue(Setup.RedMax, Setup.RedMin);
                var green = Setup.GreenMin <= Setup.GreenMax ?
                    (byte)_randomizer.GetValue(Setup.GreenMin, Setup.GreenMax) :
                    (byte)_randomizer.GetValue(Setup.GreenMax, Setup.GreenMin);
                var blue = Setup.BlueMin <= Setup.BlueMax ?
                    (byte)_randomizer.GetValue(Setup.BlueMin, Setup.BlueMax) :
                    (byte)_randomizer.GetValue(Setup.BlueMax, Setup.BlueMin);

                return Color.FromArgb(255, red, green, blue);
            }
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Size"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomSize() => _randomizer.GetValue(Setup.SizeMin, Setup.SizeMax);


        /// <summary>
        /// Returns a random <see cref="Particle.LifeTime"/> for a spawned <see cref="Particle"/>.
        /// If the max is less than the min, the <see cref="Particle.LifeTime"/> will still be chosen
        /// randomly between the two values.
        /// </summary>
        /// <returns></returns>
        private int GetRandomLifeTime()
        {
            if (Setup.LifeTimeMin <= Setup.LifeTimeMax)
                return _randomizer.GetValue(Setup.LifeTimeMin, Setup.LifeTimeMax);


            return _randomizer.GetValue(Setup.LifeTimeMax, Setup.LifeTimeMin);
        }
        #endregion
    }
}
