// <copyright file="ColorTransitionBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngine.Behaviors
{
    using System;

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

#pragma warning disable SA1629 // Documentation text should end with a period
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
#pragma warning restore SA1629 // Documentation text should end with a period

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

            byte alpha = 0;
            byte red = 0;
            byte green = 0;
            byte blue = 0;

            // Transition the red color component
            switch (this.settings.EasingFunctionType)
            {
                case EasingFunction.EaseIn:
                    (alpha, red, green, blue) = EaseInQuad();
                    break;
                case EasingFunction.EaseOutBounce:
                    (alpha, red, green, blue) = EaseOutBounce();
                    break;
            }

            Value = $"clr:{alpha},{red},{green},{blue}";

            Enabled = ElapsedTime < LifeTime;
        }

        /// <summary>
        /// Returns the alpha, red, green and blue color components after the ease in quad easing function has been applied.
        /// </summary>
        /// <returns>The component values after the easing function changes.</returns>
        private (byte alphaResult, byte redResult, byte greenResult, byte blueResult) EaseInQuad()
        {
            var alpha = EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.A,
                this.settings.AlphaChangeAmount,
                this.settings.LifeTime);

            alpha = alpha > 255 ? 255 : alpha;
            alpha = alpha < 0 ? 0 : alpha;

            var red = EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.R,
                this.settings.RedChangeAmount,
                this.settings.LifeTime);

            red = red > 255 ? 255 : red;
            red = red < 0 ? 0 : red;

            var green = EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.G,
                this.settings.GreenChangeAmount,
                this.settings.LifeTime);

            green = green > 255 ? 255 : green;
            green = green < 0 ? 0 : green;

            var blue = EasingFunctions.EaseInQuad(
                ElapsedTime,
                this.settings.StartColor.B,
                this.settings.BlueChangeAmount,
                this.settings.LifeTime);

            blue = blue > 255 ? 255 : blue;
            blue = blue < 0 ? 0 : blue;

            return ((byte)alpha, (byte)red, (byte)green, (byte)blue);
        }

        /// <summary>
        /// Returns the alpha, red, green and blue color components after the ease out bounce easing function has been applied.
        /// </summary>
        /// <returns>The component values after the easing function changes.</returns>
        private (byte alphaResult, byte redResult, byte greenResult, byte blueResult) EaseOutBounce()
        {
            var alpha = EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.A,
                this.settings.AlphaChangeAmount,
                this.settings.LifeTime);

            alpha = alpha > 255 ? 255 : alpha;
            alpha = alpha < 0 ? 0 : alpha;

            var red = EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.R,
                this.settings.RedChangeAmount,
                this.settings.LifeTime);

            red = red > 255 ? 255 : red;
            red = red < 0 ? 0 : red;

            var green = EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.G,
                this.settings.GreenChangeAmount,
                this.settings.LifeTime);

            green = green > 255 ? 255 : green;
            green = green < 0 ? 0 : green;

            var blue = EasingFunctions.EaseOutBounce(
                ElapsedTime,
                this.settings.StartColor.B,
                this.settings.BlueChangeAmount,
                this.settings.LifeTime);

            blue = blue > 255 ? 255 : blue;
            blue = blue < 0 ? 0 : blue;

            return ((byte)alpha, (byte)red, (byte)green, (byte)blue);
        }
    }
}
