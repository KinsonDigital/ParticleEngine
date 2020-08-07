// <copyright file="ExtensionMethods.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

namespace ParticleEngine
{
    /// <summary>
    /// Provides extensions to various things to help make better code.
    /// </summary>
    public static class ExtensionMethods
    {
        private static readonly char[] _validHexSymbols = new[] { '#', 'A', 'B', 'C', 'D', 'E', 'F', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

        /// <summary>
        /// Returns a random value between the given <paramref name="minValue"/> and <paramref name="maxValue"/>.
        /// </summary>
        /// <param name="random">The random instance to use.</param>
        /// <param name="minValue">The minimum value that the result will be.</param>
        /// <param name="maxValue">The maximum value that the result will be.</param>
        /// <returns></returns>
        public static float Next(this Random random, float minValue, float maxValue)
                {
                    var minValueAsInt = (int)(minValue * 1000);
                    var maxValueAsInt = (int)(maxValue * 1000);

                    if (minValueAsInt > maxValueAsInt)
                        return maxValue;

                    var randomResult = random.Next(minValueAsInt, maxValueAsInt);

                    return randomResult / 1000f;
                }

        /// <summary>
        /// Returns a true/false value that represents the flip of a coin.
        /// </summary>
        /// <param name="random">The random instance to use.</param>
        /// <returns></returns>
        [ExcludeFromCodeCoverage]
        public static bool FlipCoin(this Random random) => random.NextDouble() <= 0.5f;

        /// <summary>
        /// Adds the given <paramref name="pointB"/>'s X and Y components to this point and returns the result.
        /// </summary>
        /// <param name="pointA">The current point to add the given point to.</param>
        /// <param name="pointB">The point to add to this point.</param>
        /// <returns></returns>
        public static PointF Add(this PointF pointA, PointF pointB)
        {
            pointA.X += pointB.X;
            pointA.Y += pointB.Y;

            return pointA;
        }

        /// <summary>
        /// Multiplies the components of this <see cref="PointF"/>
        /// by the given <paramref name="scalar"/>.
        /// </summary>
        /// <param name="point">The left operand of the multiplication operation.</param>
        /// <param name="scalar">The right operand of the multiplication operation.</param>
        /// <returns></returns>
        public static PointF Mult(this PointF point, double scalar)
        {
            point.X *= (float)scalar;
            point.Y *= (float)scalar;

            return point;
        }

        /// <summary>
        /// Counts the given <paramref name="items"/> based on given <paramref name="predicate"/> returning true.
        /// </summary>
        /// <typeparam name="T">The type of object in list to count.</typeparam>
        /// <param name="items">The list of items to count based on the predicate.</param>
        /// <param name="predicate">The predicate that when returns true, counts the item.</param>
        /// <returns></returns>
        public static int Count<T>(this List<T> items, Predicate<T> predicate)
        {
            var result = 0;

            for (int i = 0; i < items.Count; i++)
            {
                if (predicate(items[i]))
                    result++;
            }

            return result;
        }

        /// <summary>
        /// Counts the given <paramref name="items"/> based on given <paramref name="predicate"/> returning true.
        /// </summary>
        /// <typeparam name="T">The type of object in list to count.</typeparam>
        /// <param name="items">The list of items to count based on the predicate.</param>
        /// <param name="predicate">The predicate that when returns true, counts the item.</param>
        /// <returns></returns>
        public static int Count<T>(this T[] items, Predicate<T> predicate)
        {
            var result = 0;

            for (int i = 0; i < items.Length; i++)
            {
                if (predicate(items[i]))
                    result++;
            }

            return result;
        }
    }
}
