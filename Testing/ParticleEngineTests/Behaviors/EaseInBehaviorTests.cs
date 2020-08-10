// <copyright file="EaseInBehaviorTests.cs" company="PlaceholderCompany">
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
    /// Tests the <see cref="EaseInBehavior"/> class.
    /// </summary>
    public class EaseInBehaviorTests
    {
        #region Private Fields
        private readonly Mock<IRandomizerService> mockRandomizerService;
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="EaseInBehaviorTests"/> class.
        /// </summary>
        public EaseInBehaviorTests() => this.mockRandomizerService = new Mock<IRandomizerService>();

        #region Method Tets
        [Fact]
        public void Update_WhenInvoked_UpdatesValueProp()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new EaseInBehavior(settings, this.mockRandomizerService.Object);

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
            var behavior = new EaseInBehavior(settings, this.mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.Equal(16, behavior.ElapsedTime);
        }
        #endregion
    }
}
