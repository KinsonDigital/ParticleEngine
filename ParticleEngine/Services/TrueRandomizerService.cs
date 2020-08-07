﻿// <copyright file="TrueRandomizerService.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine.Services
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Cryptography;

    /// <summary>
    /// Provides methods for randomizing numbers.
    /// </summary>
    public class TrueRandomizerService : IRandomizerService
    {
        private readonly RNGCryptoServiceProvider _provider = new RNGCryptoServiceProvider();
        private readonly byte[] _uint32Buffer = new byte[4];

        /// <summary>
        /// Creates a new instance of <see cref="PseudoRandomizerService"/>.
        /// </summary>
        public TrueRandomizerService() { }

        /// <summary>
        /// Returns a true/false value that represents the flip of a coin.
        /// </summary>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public bool FlipCoin() => GetValue(0f, 1f) <= 0.5f;

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
            var minValueAsInt = (int)(minValue * 1000);
            var maxValueAsInt = (int)(maxValue * 1000);

            if (minValueAsInt > maxValueAsInt)
            {
                return (float)Math.Round(GetValue(maxValueAsInt, minValueAsInt) / 1000f, 3);
            }
            else
            {
                return (float)Math.Round(GetValue(minValueAsInt, maxValueAsInt) / 1000f, 3);
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
            // If the min value is greater than the max,
            // swap the values.
            if (minValue > maxValue)
            {
                var valueTemp = minValue;
                minValue = maxValue;
                maxValue = valueTemp;
            }

            if (minValue == maxValue) return minValue;

            maxValue += 1;

            var diff = (long)(maxValue - minValue);

            while (true)
            {
                this._provider.GetBytes(this._uint32Buffer);

                var rand = Math.Abs((int)BitConverter.ToUInt32(this._uint32Buffer, 0));
                var max = 1 + (long)int.MaxValue;
                var remainder = max % diff;

                if (rand < max - remainder)
                    return (int)(minValue + (rand % diff));
            }
        }
    }
}
