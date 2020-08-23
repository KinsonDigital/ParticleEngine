// <copyright file="EasingBehaviorTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests.Behaviors
{
    using System;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using KDParticleEngineTests.Fakes;
    using Moq;
    using Xunit;

    /// <summary>
    /// Tests testing the <see cref="EasingBehavior"/> abstract class.
    /// </summary>
    public class EasingBehaviorTests : IDisposable
    {
        private Mock<IRandomizerService> mockRandomizerService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EasingBehaviorTests"/> class.
        /// </summary>
        public EasingBehaviorTests() => this.mockRandomizerService = new Mock<IRandomizerService>();

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsSetting()
        {
            // Arrange
            var setting = new EasingBehaviorSettings()
            {
                ApplyToAttribute = ParticleAttribute.Angle,
            };
            var behavior = new FakeEasingBehavior(setting, this.mockRandomizerService.Object);

            // Act
            var actual = behavior.ApplyToAttribute;

            // Assert
            Assert.Equal(ParticleAttribute.Angle, actual);
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void Start_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, this.mockRandomizerService.Object);

            // Act
            behavior.Start = 123;
            var actual = behavior.Start;

            // Assert
            Assert.Equal(123, actual);
        }

        [Fact]
        public void Change_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, this.mockRandomizerService.Object);

            // Act
            behavior.Change = 123;
            var actual = behavior.Change;

            // Assert
            Assert.Equal(123, actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void Update_WhenInvoked_UpdatesElapsedTime()
        {
            // Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, this.mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.Equal(16, behavior.ElapsedTime);
        }

        [Fact]
        public void Update_WithLifeTimeNotElapsed_IsEnabledAfterUpdate()
        {
            // Arrange
            this.mockRandomizerService.Setup(m => m.GetValue(0f, 1000f)).Returns(500f);
            var settings = new EasingBehaviorSettings()
            {
                TotalTimeMin = 0,
                TotalTimeMax = 1000,
            };
            var behavior = new FakeEasingBehavior(settings, this.mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.True(behavior.Enabled);
        }

        [Fact]
        public void Reset_WhenInvoked_ResetsStartProp()
        {
            // Arrange
            var setting = new EasingBehaviorSettings();
            this.mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeEasingBehavior(setting, this.mockRandomizerService.Object);

            // Act
            behavior.Reset();

            // Assert
            Assert.Equal(123, behavior.Start);
        }

        [Fact]
        public void Reset_WhenInvoked_ResetsChangeProp()
        {
            // Arrange
            var setting = new EasingBehaviorSettings();
            this.mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeEasingBehavior(setting, this.mockRandomizerService.Object);

            // Act
            behavior.Reset();

            // Assert
            Assert.Equal(123, behavior.Change);
        }

        [Fact]
        public void Reset_WhenInvoked_ResetsElapsedTimeProp()
        {
            // Arrange
            var setting = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(setting, this.mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));
            behavior.Reset();

            // Assert
            Assert.Equal(0, behavior.ElapsedTime);
        }

        [Fact]
        public void Reset_WhenInvoked_ResetsEnabledProp()
        {
            // Arrange
            var setting = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(setting, this.mockRandomizerService.Object);

            // Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 45));
            behavior.Reset();

            // Assert
            Assert.True(behavior.Enabled);
        }
        #endregion

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.mockRandomizerService = null;
            GC.SuppressFinalize(this);
        }
    }
}
