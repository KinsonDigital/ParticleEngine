﻿// <copyright file="BehaviorSettings.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine.Behaviors
{
    using System;

    /// <summary>
    /// Holds various settings for a particle behavior.
    /// </summary>
    public abstract class BehaviorSettings
    {
        /// <summary>
        /// Gets or sets the type of behavior to be created.
        /// </summary>
        public BehaviorType TypeOfBehavior { get; set; }

        /// <summary>
        /// Gets or sets the particle attribute to set the behavior value to.
        /// </summary>
        public ParticleAttribute ApplyToAttribute { get; set; }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (!(obj is BehaviorSettings setting))
            {
                return false;
            }

            return TypeOfBehavior == setting.TypeOfBehavior &&
                ApplyToAttribute == setting.ApplyToAttribute;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode() => HashCode.Combine(TypeOfBehavior, ApplyToAttribute);
    }
}
