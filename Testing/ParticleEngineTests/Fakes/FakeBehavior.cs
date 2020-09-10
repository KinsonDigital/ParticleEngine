// <copyright file="FakeBehavior.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

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
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeBehavior"/> class.
        /// Creates a new instance of <see cref="FakeBehavior"/>.
        /// </summary>
        /// <param name="setting">The settings for the behavior.</param>
        public FakeBehavior(EasingRandomBehaviorSettings setting)
            : base(setting)
        {
        }
    }
}
