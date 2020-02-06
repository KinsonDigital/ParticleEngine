using KDParticleEngine.Services;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Creates behaviors using behavior settings.
    /// </summary>
    public interface IBehaviorFactory
    {
        #region Methods
        /// <summary>
        /// Creates all of the behaviors using the given <paramref name="randomizerService"/>.
        /// </summary>
        /// <param name="settings">The list of settings used to create each behavior.</param>
        /// <param name="randomizerService">The random used to randomly generate values.</param>
        IBehavior[] CreateBehaviors(BehaviorSetting[] settings, IRandomizerService randomService);
        #endregion
    }
}
