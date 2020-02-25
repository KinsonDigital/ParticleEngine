using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System.Diagnostics.CodeAnalysis;

namespace KDParticleEngineTests.Fakes
{
    /// <summary>
    /// Used for testing purposes only.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FakeBehavior : EasingBehavior
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="FakeBehavior"/>.
        /// </summary>
        /// <param name="setting">The settings for the behavior.</param>
        /// <param name="randomizer">Creates random values.</param>
        public FakeBehavior(BehaviorSetting setting, IRandomizerService randomizer) : base(setting, randomizer) { }
        #endregion
    }
}
