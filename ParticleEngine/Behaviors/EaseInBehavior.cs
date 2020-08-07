// <copyright file="EaseInBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine.Behaviors
{
    using System;
    using System.Globalization;
    using KDParticleEngine.Services;

    /// <summary>
    /// Represents an ease in function type of behavior.
    /// </summary>
    public class EaseInBehavior : EasingBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EaseInBehavior"/> class.
        /// </summary>
        /// <param name="settings">The behavior settings of the behavior.</param>
        /// <param name="randomizer">The randomizer used for the setting value.</param>
        public EaseInBehavior(EasingBehaviorSettings settings, IRandomizerService randomizer)
            : base(settings, randomizer)
        {
        }

        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            Value = EasingFunctions.EaseInQuad(ElapsedTime, Start, Change, LifeTime).ToString(CultureInfo.InvariantCulture);
            base.Update(timeElapsed);
        }
    }
}
