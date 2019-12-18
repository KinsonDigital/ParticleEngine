using KDParticleEngine.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KDParticleEngine
{
    internal class ParticlePool<Texture> where Texture : class
    {
        #region Public Events
        /// <summary>
        /// Occurs every time the total living particles has changed.
        /// </summary>
        public event EventHandler<EventArgs>? LivingParticlesCountChanged;
        #endregion

        private List<Particle<Texture>> _particles;
        private ParticleSetup<Texture> _setup;
        private IRandomizerService _randomizer;
        private int _spawnRate;
        private int _spawnRateElapsed = 0;

        public ParticlePool(ParticleSetup<Texture> setup, IRandomizerService randomizer)
        {
            _setup = setup;
            _randomizer = randomizer;
        }


        /// <summary>
        /// Gets current total number of living <see cref="Particle"/>s.
        /// </summary>
        public int TotalLivingParticles => _particles.Count(p => p.IsAlive);

        /// <summary>
        /// Gets the current total number of dead <see cref="Particle"/>s.
        /// </summary>
        public int TotalDeadParticles => _particles.Count(p => p.IsDead);

        public Particle<Texture>[] Particles => _particles.ToArray();


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
                    _particles[i].Position = _setup.SpawnLocation;
                    _particles[i].Velocity = _setup.UseRandomVelocity ? GetRandomVelocity() : _setup.ParticleVelocity;
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
            if (_setup.SpawnRateMin <= _setup.SpawnRateMax)
                return _randomizer.GetValue(_setup.SpawnRateMin, _setup.SpawnRateMax);


            return _randomizer.GetValue(_setup.SpawnRateMax, _setup.SpawnRateMin);
        }


        /// <summary>
        /// Generates all of the particles.
        /// </summary>
        private void GenerateAllParticles()
        {
            _particles.Clear();

            for (int i = 0; i < _setup.TotalParticlesAliveAtOnce; i++)
            {
                _particles.Add(GenerateParticle());
            }
        }


        /// <summary>
        /// Generates a single <see cref="Particle"/> with random settings based on the <see cref="ParticleEngine"/>s
        /// range settings.
        /// </summary>
        /// <returns></returns>
        private Particle<Texture> GenerateParticle()
        {
            var position = _setup.SpawnLocation;

            var velocity = _setup.UseRandomVelocity ? GetRandomVelocity() : _setup.ParticleVelocity;

            var angle = GetRandomAngle();

            var angularVelocity = GetRandomAngularVelocity();

            var color = GetRandomColor();

            var size = GetRandomSize();

            var lifeTime = GetRandomLifeTime();


            return new Particle<Texture>(_setup.ParticleTexture, position, velocity, angle, angularVelocity, color, size, lifeTime);
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Velocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private PointF GetRandomVelocity()
        {
            var velXRandomResult = _randomizer.GetValue(_setup.VelocityXMin, _setup.VelocityXMax);
            var velYRandomResult = _randomizer.GetValue(_setup.VelocityYMin, _setup.VelocityYMax);


            return new PointF(velXRandomResult,
                              velYRandomResult);
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Angle"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngle() => _randomizer.GetValue(_setup.AngleMin, _setup.AngleMax);


        /// <summary>
        /// Returns a random <see cref="Particle.AngularVelocity"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomAngularVelocity() => _randomizer.GetValue(_setup.AngularVelocityMin, _setup.AngularVelocityMax) * (_randomizer.FlipCoin() ? 1 : -1);


        /// <summary>
        /// Returns a random <see cref="Particle.TintColor"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor()
        {
            if (_setup.UseColorsFromList)
            {
                return _setup.TintColors is null || _setup.TintColors.Length == 0 ? Color.FromArgb(255, 255, 255, 255) : _setup.TintColors[_randomizer.GetValue(0, _setup.TintColors.Length - 1)];
            }
            else
            {
                var red = _setup.RedMin <= _setup.RedMax ?
                    (byte)_randomizer.GetValue(_setup.RedMin, _setup.RedMax) :
                    (byte)_randomizer.GetValue(_setup.RedMax, _setup.RedMin);
                var green = _setup.GreenMin <= _setup.GreenMax ?
                    (byte)_randomizer.GetValue(_setup.GreenMin, _setup.GreenMax) :
                    (byte)_randomizer.GetValue(_setup.GreenMax, _setup.GreenMin);
                var blue = _setup.BlueMin <= _setup.BlueMax ?
                    (byte)_randomizer.GetValue(_setup.BlueMin, _setup.BlueMax) :
                    (byte)_randomizer.GetValue(_setup.BlueMax, _setup.BlueMin);

                return Color.FromArgb(255, red, green, blue);
            }
        }


        /// <summary>
        /// Returns a random <see cref="Particle.Size"/> for a spawned <see cref="Particle"/>.
        /// </summary>
        /// <returns></returns>
        private float GetRandomSize() => _randomizer.GetValue(_setup.SizeMin, _setup.SizeMax);


        /// <summary>
        /// Returns a random <see cref="Particle.LifeTime"/> for a spawned <see cref="Particle"/>.
        /// If the max is less than the min, the <see cref="Particle.LifeTime"/> will still be chosen
        /// randomly between the two values.
        /// </summary>
        /// <returns></returns>
        private int GetRandomLifeTime()
        {
            if (_setup.LifeTimeMin <= _setup.LifeTimeMax)
                return _randomizer.GetValue(_setup.LifeTimeMin, _setup.LifeTimeMax);


            return _randomizer.GetValue(_setup.LifeTimeMax, _setup.LifeTimeMin);
        }
    }
}
