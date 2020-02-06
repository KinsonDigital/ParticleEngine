using KDParticleEngine.Services;
using System.Collections.Generic;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Creates behaviors using behavior settings.
    /// </summary>
    public class BehaviorFactory : IBehaviorFactory
    {
        /// <summary>
        /// Creates all of the behaviors using the given <paramref name="randomizerService"/>.
        /// </summary>
        /// <param name="randomizerService">The random used to randomly generate values.</param>
        public IBehavior[] CreateBehaviors(BehaviorSetting[] settings, IRandomizerService randomizerService)
        {
            var behaviors = new List<IBehavior>();

            //Each particle with the given ID will get every single behavior
            //dictated by the behavior setups
            foreach (var setting in settings)
            {
                switch (setting.TypeOfBehavior)
                {
                    case BehaviorType.EaseOutBounce:
                        behaviors.Add(new EaseOutBounceBehavior(setting, randomizerService));
                        break;
                    case BehaviorType.EaseIn:
                        behaviors.Add(new EaseInBehavior(setting, randomizerService));
                        break;
                }
            }


            return behaviors.ToArray();
        }
    }
}
