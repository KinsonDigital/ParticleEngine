// <copyright file="EasingBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine.Behaviors
{
    using System;
    using System.Globalization;
    using KDParticleEngine.Services;

    /// <summary>
    /// A behavior that can be applied to a particle that uses an easing function
    /// to dictate the value of a particle attribute.
    /// </summary>
    public class EasingRandomBehavior : Behavior
    {
        private readonly EasingRandomBehaviorSettings settings;
        private readonly IRandomizerService randomizer;

        /// <summary>
        /// Initializes a new instance of the <see cref="EasingRandomBehavior"/> class.
        /// </summary>
        /// <param name="settings">The behavior settings to add to the behavior.</param>
        /// <param name="randomizer">The randomizer used for choosing values between the various setting ranges.</param>
        public EasingRandomBehavior(EasingRandomBehaviorSettings settings, IRandomizerService randomizer)
            : base(settings)
        {
            this.settings = settings;
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
        /// Gets the life time of the behavior in milliseconds.
        /// </summary>
        protected double LifeTime { get; private set; }

        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed for this update of the behavior.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            switch (this.settings.EasingFunctionType)
            {
                case EasingFunction.EaseOutBounce:
                    Value = EasingFunctions.EaseOutBounce(ElapsedTime, Start, Change, LifeTime).ToString(CultureInfo.InvariantCulture);
                    break;
                case EasingFunction.EaseIn:
                    Value = EasingFunctions.EaseInQuad(ElapsedTime, Start, Change, LifeTime).ToString(CultureInfo.InvariantCulture);
                    break;
            }

            base.Update(timeElapsed);
            Enabled = ElapsedTime < LifeTime;
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
        /// Generates random values based on the <see cref="EasingRandomBehaviorSettings"/>
        /// and applies them.
        /// </summary>
        private void ApplyRandomization()
        {
            Start = this.randomizer.GetValue(this.settings.StartMin, this.settings.StartMax);
            Change = this.randomizer.GetValue(this.settings.ChangeMin, this.settings.ChangeMax);
            LifeTime = this.randomizer.GetValue(this.settings.TotalTimeMin, this.settings.TotalTimeMax);
        }
    }
}
