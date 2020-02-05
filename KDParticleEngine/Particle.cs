using KDParticleEngine.Behaviors;
using System;
using System.Drawing;

namespace KDParticleEngine
{
    /// <summary>
    /// Represents a single particle with various properties that dictate how the <see cref="Particle"/>
    /// behaves and looks on the screen.
    /// </summary>
    public class Particle
    {
        #region Private Fields
        private readonly IBehavior[] _behaviors;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="Particle"/>.
        /// </summary>
        public Particle(IBehavior[] behaviors) => _behaviors = behaviors;
        #endregion


        #region Props
        /// <summary>
        /// Gets or sets the position of the <see cref="Particle"/>.
        /// </summary>
        public PointF Position { get; set; }

        /// <summary>
        /// Gets or sets the angle that the <see cref="Particle"/> is at when first spawned.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the color that the <see cref="Texture"/> will be tinted.
        /// </summary>
        public ParticleColor TintColor { get; set; } = ParticleColor.White;

        /// <summary>
        /// Gets or sets the sized of the <see cref="Particle"/>.
        /// </summary>
        public float Size { get; set; }

        /// <summary>
        /// Gets or sets the amount of time in milliseconds that the <see cref="Particle"/> will stay alive.
        /// </summary>
        public int LifeTime { get; set; }

        /// <summary>
        /// Gets or sets if the <see cref="Particle"/> is alive or dead.
        /// </summary>
        public bool IsAlive { get; set; } = false;

        /// <summary>
        /// Gets or sets if the <see cref="Particle"/> is dead or alive.
        /// </summary>
        public bool IsDead
        {
            get => !IsAlive;
            set => IsAlive = !value;
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the particle.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public void Update(TimeSpan timeElapsed)
        {
            IsDead = true;

            //Apply the behavior values to the particle attributes
            for (int i = 0; i < _behaviors.Length; i++)
            {
                if (_behaviors[i].Enabled)
                {
                    _behaviors[i].Update(timeElapsed);
                    IsAlive = true;

                    static byte ClampClrValue(float value)
                    {
                        return (byte)(value < 0 ? 0 : value);
                    }

                    switch (_behaviors[i].ApplyToAttribute)
                    {
                        case ParticleAttribute.X:
                            Position = new PointF((float)_behaviors[i].Value, Position.Y);
                            break;
                        case ParticleAttribute.Y:
                            Position = new PointF(Position.X, (float)_behaviors[i].Value);
                            break;
                        case ParticleAttribute.Angle:
                            Angle = (float)_behaviors[i].Value;
                            break;
                        case ParticleAttribute.Size:
                            Size = (float)_behaviors[i].Value;
                            break;
                        case ParticleAttribute.RedColorComponent:
                            TintColor.R = ClampClrValue((float)_behaviors[i].Value);
                            break;
                        case ParticleAttribute.GreenColorComponent:
                            TintColor.G = ClampClrValue((float)_behaviors[i].Value);
                            break;
                        case ParticleAttribute.BlueColorComponent:
                            TintColor.B = ClampClrValue((float)_behaviors[i].Value);
                            break;
                        case ParticleAttribute.AlphaColorComponent:
                            TintColor.A = ClampClrValue((float)_behaviors[i].Value);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// Resets the particle.
        /// </summary>
        public void Reset()
        {
            if (!(_behaviors is null))
            {
                for (int i = 0; i < _behaviors.Length; i++)
                    _behaviors[i].Reset();
            }

            Angle = 0;
            TintColor = ParticleColor.White;
            LifeTime = 0;
            IsAlive = true;
        }


        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is Particle particle))
                return false;

            
            return Position == particle.Position &&
                Angle == particle.Angle &&
                TintColor == particle.TintColor &&
                Size == particle.Size &&
                LifeTime == particle.LifeTime &&
                IsAlive == particle.IsAlive &&
                IsDead == particle.IsDead;
        }


        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(_behaviors, Position, Angle, TintColor, Size, LifeTime, IsAlive, IsDead);
        #endregion
    }
}
