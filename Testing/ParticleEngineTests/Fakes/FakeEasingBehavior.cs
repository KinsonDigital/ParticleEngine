// <copyright file="FakeEasingBehavior.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests.Fakes
{
    using System.Diagnostics.CodeAnalysis;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;

    /// <summary>
    /// Used for testing purposes only.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class FakeEasingBehavior : EasingBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeEasingBehavior"/> class.
        /// </summary>
        /// <param name="setting">The settings for the behavior.</param>
        /// <param name="randomizerService">Generates random numbers.</param>
        public FakeEasingBehavior(EasingBehaviorSettings setting, IRandomizerService randomizerService)
            : base(setting, randomizerService)
        {
        }
    }
}
