// <copyright file="RandomChoiceBehaviorSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine.Behaviors
{
    using System;

    /// <summary>
    /// Various settings for behaviors that choose values randomly from a list of choices.
    /// </summary>
    public class RandomChoiceBehaviorSettings : BehaviorSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomChoiceBehaviorSettings"/> class.
        /// </summary>
        public RandomChoiceBehaviorSettings() => TypeOfBehavior = BehaviorType.RandomChoice;

        /// <summary>
        /// Gets or sets the data for the use by an <see cref="IBehavior"/> implementation.
        /// </summary>
        public string[]? Data { get; set; }

        /// <summary>
        /// Gets or sets the amount of time that the behavior should be enabled.
        /// </summary>
        public double LifeTime { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is RandomChoiceBehaviorSettings setting))
                return false;

            return TypeOfBehavior == setting.TypeOfBehavior &&
                ApplyToAttribute == setting.ApplyToAttribute;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => HashCode.Combine(Data);
    }
}
