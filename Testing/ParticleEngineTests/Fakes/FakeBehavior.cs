namespace KDParticleEngineTests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using KDParticleEngine.Behaviors;

    /// <summary>
    /// Used for testing purposes only.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FakeBehavior : Behavior
    {
        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="FakeBehavior"/>.
        /// </summary>
        /// <param name="setting">The settings for the behavior.</param>
        public FakeBehavior(EasingBehaviorSettings setting) :
            base(setting) { }
        #endregion
    }
}
