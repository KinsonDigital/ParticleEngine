﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Adds behavior that transations from one color to another over a period of time.
    /// </summary>
    public class ColorTransitionBehavior : IBehavior
    {
        private readonly ColorTransitionBehaviorSettings settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTransitionBehavior"/> class.
        /// </summary>
        /// <param name="settings">The color transition related behavior settings.</param>
        public ColorTransitionBehavior(ColorTransitionBehaviorSettings settings) => this.settings = settings;

        /// <summary>
        /// Gets the current value of the behavior represented as a color.
        /// </summary>
        /// <remarks>
        ///     The color will be a string value that represents the current color transition
        ///     result that follows the following syntax.
        ///     Syntax: clr:<alpha>,<red>,<green>,<blue>
        ///     Example: clr:255,10,20,30
        /// </remarks>
        public string Value { get; private set; } = string.Empty;

        /// <inheritdoc/>
        public double ElapsedTime { get; private set; }

        /// <summary>
        /// Gets the particle attribute that the behavior will be applied to.
        /// </summary>
        /// <remarks>Readonly and shold always be the value of <see cref="ParticleAttribute.Color"/>.</remarks>
        public ParticleAttribute ApplyToAttribute => ParticleAttribute.Color;

        /// <inheritdoc/>
        public bool Enabled { get; private set; }

        /// <summary>
        /// Gets the life time of the behavior in milliseconds.
        /// </summary>
        /// <remarks>
        ///     Once the amount of time that has elapsed the life time of the
        ///     behavior, the behavior will be disabled.
        /// </remarks>
        protected double LifeTime => this.settings.LifeTime;

        /// <inheritdoc/>
        public void Reset()
        {
            Value = string.Empty;
            ElapsedTime = 0;
            Enabled = true;
        }

        /// <inheritdoc/>
        public void Update(TimeSpan timeElapsed)
        {
            ElapsedTime += timeElapsed.TotalMilliseconds;

            byte red = 0;
            byte green = 0;
            byte blue = 0;

            // Transition the red color component
            switch (this.settings.EasingFunctionType)
            {
                case EasingFunction.EaseIn:
                    (red, green, blue) = EaseInQuad();
                    break;
                case EasingFunction.EaseOutBounce:
                    (red, green, blue) = EaseOutBounce();
                    break;
            }

            Value = $"clr:255,{red},{green},{blue}";

            Enabled = ElapsedTime < LifeTime;
        }

        private (byte redResult, byte greenResult, byte blueResult) EaseInQuad()
        {
            var red = (byte)EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.R,
                this.settings.RedChangeAmount,
                this.settings.LifeTime);

            var green = (byte)EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.G,
                this.settings.GreenChangeAmount,
                this.settings.LifeTime);

            var blue = (byte)EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.B,
                this.settings.BlueChangeAmount,
                this.settings.LifeTime);

            return (red, green, blue);
        }

        private (byte redResult, byte greenResult, byte blueResult) EaseOutBounce()
        {
            var red = (byte)EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.R,
                this.settings.RedChangeAmount,
                this.settings.LifeTime);

            var green = (byte)EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.G,
                this.settings.GreenChangeAmount,
                this.settings.LifeTime);

            var blue = (byte)EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.B,
                this.settings.BlueChangeAmount,
                this.settings.LifeTime);

            return (red, green, blue);
        }
    }
}
