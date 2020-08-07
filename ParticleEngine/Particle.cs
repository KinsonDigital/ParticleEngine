// <copyright file="Particle.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace KDParticleEngine
{
    using System;
    using System.Drawing;
    using KDParticleEngine.Behaviors;

    /// <summary>
    /// Represents a single particle with various properties that dictate how the <see cref="Particle"/>
    /// behaves and looks on the screen.
    /// </summary>
    public class Particle
    {
        private readonly IBehavior[] behaviors;

        /// <summary>
        /// Initializes a new instance of the <see cref="Particle"/> class.
        /// </summary>
        /// <param name="behaviors">The list of behaviors to add to the <see cref="Particle"/>.</param>
        public Particle(IBehavior[] behaviors) => this.behaviors = behaviors;

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
        /// Gets or sets a value indicating whether the <see cref="Particle"/> is alive or dead.
        /// </summary>
        public bool IsAlive { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Particle"/> is dead or alive.
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
            for (var i = 0; i < this.behaviors.Length; i++)
            {
                if (this.behaviors[i].Enabled)
                {
                    var value = 0f;

                    var parseSuccess = this.behaviors[i].ApplyToAttribute != ParticleAttribute.Color
                        ? float.TryParse(string.IsNullOrEmpty(this.behaviors[i].Value) ? "0" : this.behaviors[i].Value, out value)
                        : true;

                    if (!parseSuccess)
                        throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior value '{this.behaviors[i].Value}' failed.\n\tValue must be a number.");

                    this.behaviors[i].Update(timeElapsed);
                    IsAlive = true;

                    switch (this.behaviors[i].ApplyToAttribute)
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
                            var clrParseSuccess = TryParse(this.behaviors[i].Value, out ParticleColor result);

                            if (clrParseSuccess)
                            {
                                TintColor = result;
                            }
                            else
                            {
                                // TODO: Improve this exception and test for it
                                throw new Exception($"Parsing of the color '{this.behaviors[i].Value}' failed.");
                            }

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

        /// <summary>
        /// Resets the particle.
        /// </summary>
        public void Reset()
        {
            if (!(this.behaviors is null))
            {
                for (var i = 0; i < this.behaviors.Length; i++)
                    this.behaviors[i].Reset();
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
            HashCode.Combine(this.behaviors, Position, Angle, TintColor, Size, IsAlive, IsDead);

        /// <summary>
        /// Parses the given <paramref name="value"/> to a color component byte value.
        /// </summary>
        /// <param name="clrComponent">The name of the component being parsed.</param>
        /// <param name="value">The value to be parsed.</param>
        /// <returns>A parsed byte result.</returns>
        private static byte ParseColorComponent(string clrComponent, string value)
        {
            var parseSuccess = int.TryParse(value, out var result);

            if (parseSuccess)
            {
                if (result < 0 || result > 255)
                    throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior {clrComponent} color component value '{value}' failed.");

                return (byte)result;
            }

            throw new Exception($"{nameof(Particle)}.{nameof(Particle.Update)} Exception:\n\tParsing the behavior {clrComponent} color component value '{value}' failed.");
        }

        /// <summary>
        /// Parses the <paramref name="colorValue"/> string into a <see cref="ParticleColor"/> type.
        /// </summary>
        /// <param name="colorValue">The color string to parse.</param>
        /// <returns>True if the parse was successful.</returns>
        private static bool TryParse(string colorValue, out ParticleColor color)
        {
            color = new ParticleColor(0, 0, 0, 0);

            /*Parse the string data into color components to create a color from
             * Example Data: clr:10,20,30,40
            */

            if (!colorValue.Contains(':', StringComparison.OrdinalIgnoreCase))
                return false;

            // Split into sections to separate 'clr' section and the '10,20,30,40' pieces of the string
            // Section 1 => clr
            // Section 2 => 10,20,30,40
            var valueSections = colorValue.Split(':');

            if (valueSections.Length >= 1 && string.IsNullOrEmpty(valueSections[0]))
                return false;

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
            color = new ParticleColor(alpha, red, green, blue);

            return true;
        }
    }
}
