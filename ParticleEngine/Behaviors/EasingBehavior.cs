// <copyright file="EasingBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngine.Behaviors
{
    using System;
    using ParticleEngine.Services;

    /// <summary>
    /// A behavior that can be applied to a particle that uses an easing function
    /// to dictate the value of a particle attribute.
    /// </summary>
    public abstract class EasingBehavior : Behavior
    {
        private readonly EasingBehaviorSettings setting;
        private readonly IRandomizerService randomizer;
        private protected double lifeTime;

        /// <summary>
        /// Creates a new instance of <see cref="EasingBehavior"/>.
        /// </summary>
        /// <param name="randomizer">The randomizer used for choosing values between the various setting ranges.</param>
        public EasingBehavior(EasingBehaviorSettings settings, IRandomizerService randomizer) : base(settings)
        {
            this.setting = settings;
            this.randomizer = randomizer;
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
            Enabled = ElapsedTime < this.lifeTime;
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
            Start = this.randomizer.GetValue(this.setting.StartMin, this.setting.StartMax);
            Change = this.randomizer.GetValue(this.setting.ChangeMin, this.setting.ChangeMax);
            this.lifeTime = this.randomizer.GetValue(this.setting.TotalTimeMin, this.setting.TotalTimeMax);
        }
    }
}
