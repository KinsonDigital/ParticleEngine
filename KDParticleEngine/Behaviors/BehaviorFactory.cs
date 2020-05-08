﻿using KDParticleEngine.Services;
using System;
using System.Collections.Generic;

namespace KDParticleEngine.Behaviors
{
    /// <summary>
    /// Creates behaviors using behavior settings.
    /// </summary>
    public class BehaviorFactory : IBehaviorFactory
    {
        #region Public Methods
        /// <summary>
        /// Creates all of the behaviors using the given <paramref name="randomizerService"/>.
        /// </summary>
        /// <param name="settings">The list of settings used to create each behavior.</param>
        /// <param name="randomizerService">The random used to randomly generate values.</param>
        public IBehavior[] CreateBehaviors(BehaviorSettings[] settings, IRandomizerService randomizerService)
        {
            var behaviors = new List<IBehavior>();

            //Each particle with the given ID will get every single behavior
            //dictated by the behavior setups
            foreach (var setting in settings)
            {
                switch (setting.TypeOfBehavior)
                {
                    case BehaviorType.EaseOutBounce:
                        if (!(setting is EasingBehaviorSettings easeOutBounceSettings))
                            throw new Exception($"Behavior Factory Error: Behavior settings must be of type '{nameof(EasingBehaviorSettings)}' for an '{nameof(EaseOutBounceBehavior)}'.");

                        behaviors.Add(new EaseOutBounceBehavior(easeOutBounceSettings, randomizerService));
                        break;
                    case BehaviorType.EaseIn:
                        if (!(setting is EasingBehaviorSettings easeInSettings))
                            throw new Exception($"Behavior Factory Error: Behavior settings must be of type '{nameof(EasingBehaviorSettings)}' for an '{nameof(EaseInBehavior)}'.");

                        behaviors.Add(new EaseInBehavior(easeInSettings, randomizerService));
                        break;
                    case BehaviorType.RandomChoice:
                        if (!(setting is RandomChoiceBehaviorSettings randomChoiceSettings))
                            throw new Exception($"Behavior Factory Error: Behavior settings must be of type '{nameof(RandomChoiceBehaviorSettings)}' for an '{nameof(RandomColorBehavior)}'.");

                        behaviors.Add(new RandomColorBehavior(randomChoiceSettings, randomizerService));
                        break;
                }
            }


            return behaviors.ToArray();
        }
        #endregion
    }
}
