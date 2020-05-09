using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using KDParticleEngineTests.Fakes;
using Moq;
using System;
using Xunit;

namespace KDParticleEngineTests.Behaviors
{
    /// <summary>
    /// Holds tests for testing the <see cref="EasingBehavior"/> abstract class.
    /// </summary>
    public class EasingBehaviorTests : IDisposable
    {
        #region Private Fields
        private Mock<IRandomizerService> _mockRandomizerService;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="EasingBehaviorTests"/>
        /// </summary>
        public EasingBehaviorTests() => _mockRandomizerService = new Mock<IRandomizerService>();
        #endregion


        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsSetting()
        {
            //Arrange
            var setting = new EasingBehaviorSettings()
            {
                ApplyToAttribute = ParticleAttribute.Angle
            };
            var behavior = new FakeEasingBehavior(setting, _mockRandomizerService.Object);

            //Act
            var actual = behavior.ApplyToAttribute;

            //Assert
            Assert.Equal(ParticleAttribute.Angle, actual);
        }
        #endregion


        #region Prop Tests
        [Fact]
        public void Start_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, _mockRandomizerService.Object);

            //Act
            behavior.Start = 123;
            var actual = behavior.Start;

            //Assert
            Assert.Equal(123, actual);
        }


        [Fact]
        public void Change_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, _mockRandomizerService.Object);

            //Act
            behavior.Change = 123;
            var actual = behavior.Change;

            //Assert
            Assert.Equal(123, actual);
        }
        #endregion


        #region Method Tests
        [Fact]
        public void Update_WhenInvoked_UpdatesElapsedTime()
        {
            //Arrange
            var settings = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(settings, _mockRandomizerService.Object);

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.Equal(16, behavior.ElapsedTime);
        }

        //TODO: Fix this unit test
        [Fact]
        public void Update_WithLifeTimeNotElapsed_IsEnabledAfterUpdate()
        {
            //Arrange
            _mockRandomizerService.Setup(m => m.GetValue(0f, 1000f)).Returns(500f);
            var settings = new EasingBehaviorSettings()
            {
                TotalTimeMin = 0,
                TotalTimeMax = 1000
            };
            var behavior = new FakeEasingBehavior(settings, _mockRandomizerService.Object);

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.True(behavior.Enabled);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsStartProp()
        {
            //Arrange
            var setting = new EasingBehaviorSettings();
            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeEasingBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(123, behavior.Start);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsChangeProp()
        {
            //Arrange
            var setting = new EasingBehaviorSettings();
            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeEasingBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(123, behavior.Change);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsElapsedTimeProp()
        {
            //Arrange
            var setting = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));
            behavior.Reset();

            //Assert
            Assert.Equal(0, behavior.ElapsedTime);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsEnabledProp()
        {
            //Arrange
            var setting = new EasingBehaviorSettings();
            var behavior = new FakeEasingBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 45));
            behavior.Reset();

            //Assert
            Assert.True(behavior.Enabled);
        }
        #endregion


        #region Public Methods
        public void Dispose() => _mockRandomizerService = null;
        #endregion
    }
}
