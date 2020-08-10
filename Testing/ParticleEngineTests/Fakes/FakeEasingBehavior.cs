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
