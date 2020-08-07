// <copyright file="IBehaviorFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

using ParticleEngine.Services;

namespace ParticleEngine.Behaviors
{
    /// <summary>
    /// Creates behaviors using behavior settings.
    /// </summary>
    public interface IBehaviorFactory
    {
        /// <summary>
        /// Creates all of the behaviors using the given <paramref name="settings"/> and <paramref name="randomizerService"/>.
        /// </summary>
        /// <param name="settings">The list of settings used to create each behavior.</param>
        /// <param name="randomizerService">Used to generate random values from the given <paramref name="settings"/> param.</param>
        IBehavior[] CreateBehaviors(BehaviorSettings[] settings, IRandomizerService randomService);
    }
}
