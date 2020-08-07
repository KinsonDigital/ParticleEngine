// <copyright file="Particle.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine
{
    using System;
    using System.Drawing;
    using ParticleEngine.Behaviors;

    /// <summary>
    /// Represents a single particle with various properties that dictate how the <see cref="Particle"/>
    /// behaves and looks on the screen.
    /// </summary>
    public class Particle
    {
        private readonly IBehavior[] _behaviors;

        /// <summary>
        /// Creates a new instance of <see cref="Particle"/>.
        /// </summary>
        public Particle(IBehavior[] behaviors) => this._behaviors = behaviors;

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

            // Apply the behavior values to the particle attributes
            for (int i = 0; i < this._behaviors.Length; i++)
            {
                if (this._behaviors[i].Enabled)
                {
                    var value = 0f;

                    var parseSuccess = this._behaviors[i].ApplyToAttribute != ParticleAttribute.Color
                        ? float.TryParse(string.IsNullOrEmpty(this._behaviors[i].Value) ? "0" : this._behaviors[i].Value, out value)
                        : true;

                    if (!parseSuccess)
                        throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior value '{this._behaviors[i].Value}' failed.\n\tValue must be a number.");

                    this._behaviors[i].Update(timeElapsed);
                    IsAlive = true;

                    switch (this._behaviors[i].ApplyToAttribute)
                    {
                        case ParticleAttribute.X:
                            Position = new PointF(value, Position.Y);
                            break;
                        case ParticleAttribute.Y:
                            Position = new PointF(Position.X, value);
                            break;
                        case ParticleAttribute.Angle:
                            Angle = value;
                            break;
                        case ParticleAttribute.Size:
                            Size = value;
                            break;
                        case ParticleAttribute.Color:
                            // Create the color
                            TintColor = ParseToParticleColor(this._behaviors[i].Value);
                            break;
                        case ParticleAttribute.RedColorComponent:
                            TintColor.R = ClampClrValue(value);
                            break;
                        case ParticleAttribute.GreenColorComponent:
                            TintColor.G = ClampClrValue(value);
                            break;
                        case ParticleAttribute.BlueColorComponent:
                            TintColor.B = ClampClrValue(value);
                            break;
                        case ParticleAttribute.AlphaColorComponent:
                            TintColor.A = ClampClrValue(value);
                            break;
                    }
                }
            }
        }

        private byte ParseColorComponent(string clrComponent, string value)
        {
            var parseSuccess = int.TryParse(value, out int result);

            if (parseSuccess)
            {
                if (result < 0 || result > 255)
                    throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior {clrComponent} color component value '{value}' failed.");

                return (byte)result;
            }

            throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior {clrComponent} color component value '{value}' failed.");
        }

        private ParticleColor ParseToParticleColor(string colorValue)
        {
            /*Parse the string data into color components to create a color from
             * Example Data: clr:10,20,30,40
            */

            if (!colorValue.Contains(':'))
                throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tInvalid random color syntax.  Missing ':'.\n\tSyntax is as follows: clr:<alpha>,<red>,<green>,<blue>");

            // Split into sections to separate 'clr' section and the '10,20,30,40' pieces of the string
            // Section 1 => clr
            // Section 2 => 10,20,30,40
            var valueSections = colorValue.Split(':');

            if (valueSections.Length >= 1 && string.IsNullOrEmpty(valueSections[0]))
                throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tInvalid random color syntax.  Missing 'clr'.\n\tSyntax is as follows: clr:<alpha>,<red>,<green>,<blue>");

            // Split the color components to separate each number
            // Section 1 => 10
            // Section 2 => 20
            // Section 3 => 30
            // Section 4 => 40
            var clrComponents = valueSections[1].Split(',');

            var alpha = ParseColorComponent("alpha", clrComponents[0]);
            var red = ParseColorComponent("red", clrComponents[1]);
            var green = ParseColorComponent("green", clrComponents[2]);
            var blue = ParseColorComponent("blue", clrComponents[3]);

            // Create the color
            return new ParticleColor(alpha, red, green, blue);
        }

        /// <summary>
        /// Resets the particle.
        /// </summary>
        public void Reset()
        {
            if (!(this._behaviors is null))
            {
                for (int i = 0; i < this._behaviors.Length; i++)
                    this._behaviors[i].Reset();
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
            HashCode.Combine(this._behaviors, Position, Angle, TintColor, Size, IsAlive, IsDead);
    }
}
