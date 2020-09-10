using System;
using System.Collections.Generic;
using System.Text;

namespace KDParticleEngine.Behaviors
{
    public class ColorTransitionBehaviorSettings : BehaviorSettings, IEasingCapable
    {
        /*NOTE:
         * The "change amount props" are build on the premise that if the start color component
         * value is less than the stop color component value, then returning a negative positive
         * value will make sure that the change amount being fed into the ease in function
         * will increase the start value until it reaches the stop value.
         */
        public ColorTransitionBehaviorSettings() => ApplyToAttribute = ParticleAttribute.Color;

        /// <inheritdoc/>
        public EasingFunction EasingFunctionType { get; set; } = EasingFunction.EaseIn;

        public ParticleColor StartColor { get; set; }

        public ParticleColor StopColor { get; set; }

        public double LifeTime { get; set; }

        internal int RedChangeAmount => StopColor.R - StartColor.R;

        internal int GreenChangeAmount => StopColor.G - StartColor.G;

        internal int BlueChangeAmount => StopColor.B - StartColor.B;
    }
}
