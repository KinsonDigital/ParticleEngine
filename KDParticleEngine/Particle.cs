using System;
using System.Drawing;
using KDParticleEngine.Behaviors;

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
        /// Gets or sets the angle of the <see cref="Particle"/>.
        /// </summary>
        public float Angle { get; set; }

        /// <summary>
        /// Gets or sets the color that the <see cref="Texture"/> will be tinted to.
        /// </summary>
        public ParticleColor TintColor { get; set; } = ParticleColor.White;

        /// <summary>
        /// Gets or sets the size of the <see cref="Particle"/>.
        /// </summary>
        public float Size { get; set; }

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

            static byte ClampClrValue(float value)
            {
                return (byte)(value < 0 ? 0 : value);
            }

            //Apply the behavior values to the particle attributes
            for (int i = 0; i < _behaviors.Length; i++)
            {
                if (_behaviors[i].Enabled)
                {
                    _behaviors[i].Update(timeElapsed);
                    IsAlive = true;


                    switch (_behaviors[i].ApplyToAttribute)
                    {
                        case ParticleAttribute.X:
                            Position = new PointF(float.Parse(_behaviors[i].Value), Position.Y);
                            break;
                        case ParticleAttribute.Y:
                            Position = new PointF(Position.X, float.Parse(_behaviors[i].Value));
                            break;
                        case ParticleAttribute.Angle:
                            Angle = float.Parse(_behaviors[i].Value);
                            break;
                        case ParticleAttribute.Size:
                            Size = float.Parse(_behaviors[i].Value);
                            break;
                        case ParticleAttribute.Color:
                            //Parse the string data into color components to create a color from

                            //Example Data: clr:10,20,30,40

                            //Split into sections to separate 'clr' section and the '10,20,30,40' pieces of the string
                            //Section 1 => clr
                            //Section 2 => 10,20,30,40
                            var valueSections = _behaviors[i].Value.Split(':');

                            //Split the color components to separate each number
                            //Section 1 => 10
                            //Section 2 => 20
                            //Section 3 => 30
                            //Section 4 => 40
                            var clrComponents = valueSections[1].Split(',');

                            //Create the color
                            var tintColor = new ParticleColor(byte.Parse(clrComponents[0]),
                                                              byte.Parse(clrComponents[1]),
                                                              byte.Parse(clrComponents[2]),
                                                              byte.Parse(clrComponents[3]));

                            TintColor = tintColor;
                            break;
                        case ParticleAttribute.RedColorComponent:
                            TintColor.R = ClampClrValue(float.Parse(_behaviors[i].Value));
                            break;
                        case ParticleAttribute.GreenColorComponent:
                            TintColor.G = ClampClrValue(float.Parse(_behaviors[i].Value));
                            break;
                        case ParticleAttribute.BlueColorComponent:
                            TintColor.B = ClampClrValue(float.Parse(_behaviors[i].Value));
                            break;
                        case ParticleAttribute.AlphaColorComponent:
                            TintColor.A = ClampClrValue(float.Parse(_behaviors[i].Value));
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
                IsAlive == particle.IsAlive &&
                IsDead == particle.IsDead;
        }


        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(_behaviors, Position, Angle, TintColor, Size, IsAlive, IsDead);
        #endregion
    }
}
