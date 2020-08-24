// <copyright file="BehaviorFactoryTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace KDParticleEngineTests.Behaviors
{
    using System;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using Moq;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="BehaviorFactory"/> class.
    /// </summary>
    public class BehaviorFactoryTests
    {
        #region Method Tests
        [Theory]
        [InlineData(BehaviorType.EaseOutBounce, typeof(EaseOutBounceBehavior))]
        [InlineData(BehaviorType.EaseIn, typeof(EaseInBehavior))]
        public void CreateBehaviors_WhenInvoked_CreatesCorrectBehavior(BehaviorType behaviorType, Type expectedBehaviorType)
        {
            // Arrange
            var mockRandomizerService = new Mock<IRandomizerService>();
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    TypeOfBehavior = behaviorType,
                },
            };
            var factory = new BehaviorFactory();

            // Act
            var actual = factory.CreateBehaviors(settings, mockRandomizerService.Object);

            // Assert
            Assert.Single(actual);
            Assert.Equal(actual[0].GetType(), expectedBehaviorType);
        }
        #endregion
    }
}
