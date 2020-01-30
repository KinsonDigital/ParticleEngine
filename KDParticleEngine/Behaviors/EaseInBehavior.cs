using KDParticleEngine.Services;
using System;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Represents an ease in type of easing function behavior.
    /// </summary>
    public class EaseInBehavior : EasingBehavior
    {
        #region Constructors
        public EaseInBehavior(BehaviorSetting setting, IRandomizerService randomizer) : base(setting, randomizer) { }
        #endregion


        #region Public Methods
        /// <summary>
        /// Updates the behavior.
        /// </summary>
        /// <param name="timeElapsed">The amount of time that has elapsed for this update of the behavior.</param>
        public override void Update(TimeSpan timeElapsed)
        {
            Value = EasingFunctions.EaseInQuad(ElapsedTime, Start, Change, TotalTime);

            base.Update(timeElapsed);
        }
        #endregion
    }
}
