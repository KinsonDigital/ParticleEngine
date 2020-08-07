using System;
using ParticleEngine.Services;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// Represents an ease out bounce type of easing function behavior.
    /// </summary>
    public class EaseOutBounceBehavior : EasingBehavior
    {
        /// <summary>
        /// Creates a new instance of <see cref="EaseOutBounceBehavior"/>.
        /// </summary>
        /// <param name="settings">The behavior settings of the behavior.</param>
        /// <param name="randomizer">The randomizer used for choosing values between the various setting ranges.</param>
        public EaseOutBounceBehavior(EasingBehaviorSettings settings, IRandomizerService randomizer) : base (settings, randomizer) { }


        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            Value = EasingFunctions.EaseOutBounce(ElapsedTime, Start, Change, _lifeTime).ToString();
            base.Update(timeElapsed);
        }
    }
}
