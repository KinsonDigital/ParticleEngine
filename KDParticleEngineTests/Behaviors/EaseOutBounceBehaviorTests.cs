using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using Moq;
using System;
using Xunit;

namespace KDParticleEngineTests.Behaviors
{
    public class EaseOutBounceBehaviorTests
    {
        #region Method Tets
        [Fact]
        public void Update_WhenInvoked_UpdatesValueProp()
        {
            //Arrange
            var behavior = new EaseOutBounceBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.NotEqual(0, behavior.Value);
        }


        [Fact]
        public void Update_WhenInvoked_UpdatesElapsedTime()
        {
            //Arrange
            var behavior = new EaseOutBounceBehavior(It.IsAny<BehaviorSetting>(), It.IsAny<IRandomizerService>());

            //Act
            behavior.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.Equal(0.016, behavior.ElapsedTime);
        }
        #endregion
    }
}
