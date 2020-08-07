// <copyright file="EasingBehaviorSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine.Behaviors
{
    using System;

    /// <summary>
    /// Stores settings for creating an <see cref="EasingBehavior"/>.
    /// </summary>
    public class EasingBehaviorSettings : BehaviorSettings
    {
        /// <summary>
        /// The minimum starting value used in randomization.
        /// </summary>
        public float StartMin { get; set; }

        /// <summary>
        /// The maximum starting value used in randomization.
        /// </summary>
        public float StartMax { get; set; }

        /// <summary>
        /// The minimum amount of change used in randomization.
        /// </summary>
        public float ChangeMin { get; set; }

        /// <summary>
        /// The maximum amount of change used in randomization.
        /// </summary>
        public float ChangeMax { get; set; }

        /// <summary>
        /// The minimum total amount of time in milliseconds to complete the behavior.
        /// </summary>
        public float TotalTimeMin { get; set; }

        /// <summary>
        /// The maximum total amount of time in milliseconds to complete the behavior.
        /// </summary>
        public float TotalTimeMax { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is EasingBehaviorSettings setting))
                return false;

            return TypeOfBehavior == setting.TypeOfBehavior &&
                ApplyToAttribute == setting.ApplyToAttribute &&
                StartMin == setting.StartMin &&
                StartMax == setting.StartMax &&
                ChangeMin == setting.ChangeMin &&
                ChangeMax == setting.ChangeMax &&
                TotalTimeMin == setting.TotalTimeMin &&
                TotalTimeMax == setting.TotalTimeMax;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() =>
            HashCode.Combine(TypeOfBehavior, ApplyToAttribute,
                             StartMin, StartMax,
                             ChangeMin, ChangeMax,
                             TotalTimeMin, TotalTimeMax);
    }
}
