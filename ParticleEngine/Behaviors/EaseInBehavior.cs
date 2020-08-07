using System;
using ParticleEngine.Services;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// Represents an ease in function type of behavior.
    /// </summary>
    public class EaseInBehavior : EasingBehavior
    {
        /// <summary>
        /// Creates a new instance of <see cref="EaseInBehavior"/>.
        /// </summary>
        /// <param name="settings">The behavior settings of the behavior.</param>
        /// <param name="randomizer">The randomizer used for ther setting value.</param>
        public EaseInBehavior(EasingBehaviorSettings settings, IRandomizerService randomizer) : base(settings, randomizer) { }

        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            Value = EasingFunctions.EaseInQuad(ElapsedTime, Start, Change, _lifeTime).ToString();
            base.Update(timeElapsed);
        }
    }
}
