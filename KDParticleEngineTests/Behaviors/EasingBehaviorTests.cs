using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using KDParticleEngineTests.Fakes;
using Moq;
using System;
using Xunit;

namespace KDParticleEngineTests.Behaviors
{
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
            var setting = new BehaviorSetting()
            {
                ApplyToAttribute = ParticleAttribute.Angle
            };
            var behavior = new FakeBehavior(setting, It.IsAny<IRandomizerService>());

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
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

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
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.Change = 123;
            var actual = behavior.Change;

            //Assert
            Assert.Equal(123, actual);
        }


        [Fact]
        public void TotalTime_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.TotalTime = 123;
            var actual = behavior.TotalTime;

            //Assert
            Assert.Equal(123, actual);
        }


        [Fact]
        public void Value_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.Value = 123;
            var actual = behavior.Value;

            //Assert
            Assert.Equal(123, actual);
        }
        #endregion


        #region Method Tests
        [Fact]
        public void Update_WhenInvoked_UpdatesElapsedTime()
        {
            //Arrange
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.Equal(0.016, behavior.ElapsedTime);
        }


        [Fact]
        public void Update_WithTotalTimeNotElapsed_IsEnabledAfterUpdate()
        {
            //Arrange
            var behavior = new FakeBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>())
            {
                TotalTime = 0.32
            };

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.True(behavior.Enabled);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsValueProp()
        {
            //Arrange
            var setting = new BehaviorSetting();
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object)
            {
                Value = 123
            };

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(0, behavior.Value);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsStartProp()
        {
            //Arrange
            var setting = new BehaviorSetting();
            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(123, behavior.Start);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsChangeProp()
        {
            //Arrange
            var setting = new BehaviorSetting();
            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(123, behavior.Change);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsTotalTimeProp()
        {
            //Arrange
            var setting = new BehaviorSetting();
            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<float>(), It.IsAny<float>())).Returns(123);
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object);

            //Act
            behavior.Reset();

            //Assert
            Assert.Equal(123, behavior.TotalTime);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsElapsedTimeProp()
        {
            //Arrange
            var setting = new BehaviorSetting();
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object);

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
            var setting = new BehaviorSetting();
            var behavior = new FakeBehavior(setting, _mockRandomizerService.Object);

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
