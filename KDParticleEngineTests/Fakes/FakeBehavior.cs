using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using System.Diagnostics.CodeAnalysis;

namespace KDParticleEngineTests.Fakes
{
    [ExcludeFromCodeCoverage]
    public class FakeBehavior : EasingBehavior
    {
        public FakeBehavior(BehaviorSetting setting, IRandomizerService randomizer) : base(setting, randomizer)
        {
        }
    }
}
