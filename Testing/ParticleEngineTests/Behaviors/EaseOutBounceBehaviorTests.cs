// <copyright file="EaseOutBounceBehaviorTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests.Behaviors
{
    using System;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using Moq;
    using Xunit;

    /// <summary>
    /// Holds tests for the <see cref="EaseOutBounceBehavior"/> class.
    /// </summary>
    public class EaseOutBounceBehaviorTests
    {
        #region Private Fields
        private readonly Mock<IRandomizerService> _mockRandomizerService;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EaseOutBounceBehaviorTests"/>.
        /// </summary>
        public EaseOutBounceBehaviorTests() => _mockRandomizerService = new Mock<IRandomizerService>();
        #endregion

        #region Method Tets
        [Fact]
        public void Update_WhenInvoked_UpdatesValueProp()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new EaseOutBounceBehavior(settings, _mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.NotEqual("0", behavior.Value);
        }

        [Fact]
        public void Update_WhenInvoked_UpdatesElapsedTime()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new EaseOutBounceBehavior(settings, _mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.Equal(16, behavior.ElapsedTime);
        }
        #endregion
    }
}
