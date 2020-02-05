using KDParticleEngine.Services;

namespace KDParticleEngine.Behaviors
{
    public interface IBehaviorFactory
    {
        IBehavior[] CreateBehaviors(BehaviorSetting[] settings, IRandomizerService randomService);
    }
}
