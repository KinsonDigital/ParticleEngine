using KDParticleEngine.Services;
using System;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Represents an ease out bounce type of easing function behavior.
    /// </summary>
    public class EaseOutBounceBehavior : EasingBehavior
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EaseOutBounceBehavior"/>.
        /// </summary>
        /// <param name="setting">The behavior settings of the behavior.</param>
        /// <param name="randomizer">The randomizer used for ther setting value.</param>
        public EaseOutBounceBehavior(BehaviorSetting setting, IRandomizerService randomizer) : base (setting, randomizer) { }
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed since the last frame.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            Value = EasingFunctions.EaseOutBounce(ElapsedTime, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }
        #endregion
    }
}
