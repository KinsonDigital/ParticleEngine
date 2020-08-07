// <copyright file="EasingBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using System;
using ParticleEngine.Services;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// A behavior that can be applied to a particle that uses an easing function
    /// to dictate the value of a particle attribute.
    /// </summary>
    public abstract class EasingBehavior : Behavior
    {
        private readonly EasingBehaviorSettings _setting;
        private readonly IRandomizerService _randomizer;
        private protected double _lifeTime;

        /// <summary>
        /// Creates a new instance of <see cref="EasingBehavior"/>.
        /// </summary>
        /// <param name="randomizer">The randomizer used for choosing values between the various setting ranges.</param>
        public EasingBehavior(EasingBehaviorSettings settings, IRandomizerService randomizer) : base(settings)
        {
            _setting = settings;
            _randomizer = randomizer;
            ApplyRandomization();
        }

        /// <summary>
        /// Gets or sets the starting value of the easing behavior.
        /// </summary>
        public double Start { get; set; }

        /// <summary>
        /// Gets or sets the amount of change to apply to the behavior value over time.
        /// </summary>
        public double Change { get; set; }

        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed for this update of the behavior.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
            Enabled = ElapsedTime < _lifeTime;
        }

        /// <summary>
        /// Resets the behavior.
        /// </summary>
        public override void Reset()
        {
            ApplyRandomization();
            base.Reset();
        }

        /// <summary>
        /// Generates random values based on the <see cref="EasingBehaviorSettings"/>
        /// and applies them.
        /// </summary>
        private void ApplyRandomization()
        {
            Start = _randomizer.GetValue(_setting.StartMin, _setting.StartMax);
            Change = _randomizer.GetValue(_setting.ChangeMin, _setting.ChangeMax);
            _lifeTime = _randomizer.GetValue(_setting.TotalTimeMin, _setting.TotalTimeMax);
        }
    }
}
