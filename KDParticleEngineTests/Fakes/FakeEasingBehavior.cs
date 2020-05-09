using System.Diagnostics.CodeAnalysis;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;

namespace KDParticleEngineTests.Fakes
{
    /// <summary>
    /// Used for testing purposes only.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FakeEasingBehavior : EasingBehavior
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="FakeEasingBehavior"/>.
        /// </summary>
        /// <param name="setting">The settings for the behavior.</param>
        public FakeEasingBehavior(EasingBehaviorSettings setting, IRandomizerService randomizerService) :
            base(setting, randomizerService) { }
        #endregion
    }
}
