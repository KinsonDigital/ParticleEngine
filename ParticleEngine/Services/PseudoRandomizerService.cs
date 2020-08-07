// <copyright file="PseudoRandomizerService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Provides methods for randomizing numbers.
    /// </summary>
    public class PseudoRandomizerService : IRandomizerService
    {
        private readonly Random _random;

        /// <summary>
        /// Creates a new instance of <see cref="PseudoRandomizerService"/>.
        /// </summary>
        public PseudoRandomizerService() => this._random = new Random();

        /// <summary>
        /// Returns a true/false value that represents the flip of a coin.
        /// </summary>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public bool FlipCoin() => this._random.NextDouble() <= 0.5;

        /// <summary>
        /// Gets a random number between the given <paramref name="minValue"/> and <paramref name="maxValue"/>s.
        /// A random value will be chosen between the min and max values no matter which value is less than
        /// or greater than the other.
        /// </summary>
        /// <param name="minValue">The inclusive minimum value of the range to randomly choose from.</param>
        /// <param name="maxValue">The inclusive maximum value of the range to randomly choose from.</param>
        /// <returns></returns>
        public float GetValue(float minValue, float maxValue)
        {
            var minValueAsInt = (int)((minValue + 0.001f) * 1000);
            var maxValueAsInt = (int)((maxValue + 0.001f) * 1000);

            if (minValueAsInt > maxValueAsInt)
            {
                return (float)Math.Round(this._random.Next(maxValueAsInt, minValueAsInt) / 1000f, 3);
            }
            else
            {
                return (float)Math.Round(this._random.Next(minValueAsInt, maxValueAsInt) / 1000f, 3);
            }
        }

        /// <summary>
        /// Gets a random number between the given <paramref name="minValue"/> and <paramref name="maxValue"/>s.
        /// A random value will be chosen between the min and max values no matter which value is less than
        /// or greater than the other.
        /// </summary>
        /// <param name="minValue">The inclusive minimum value of the range to randomly choose from.</param>
        /// <param name="maxValue">The inclusive maximum value of the range to randomly choose from.</param>
        /// <returns></returns>
        public double GetValue(double minValue, double maxValue) =>
            // Add 0.001 so that way the max value is inclusive.
            GetValue((float)minValue, (float)maxValue);

        /// <summary>
        /// Gets a random number between the given <paramref name="minValue"/> and <paramref name="maxValue"/>s.
        /// A random value will be chosen between the min and max values no matter which value is less than
        /// or greater than the other.
        /// </summary>
        /// <param name="minValue">The inclusive minimum value of the range to randomly choose from.</param>
        /// <param name="maxValue">The inclusive maximum value of the range to randomly choose from.</param>
        /// <returns></returns>
        public int GetValue(int minValue, int maxValue)
        {
            // Add 1 so that way the max value is inclusive.
            return minValue > maxValue ?
                this._random.Next(maxValue, minValue + 1) :
                this._random.Next(minValue, maxValue + 1);
        }
    }
}
