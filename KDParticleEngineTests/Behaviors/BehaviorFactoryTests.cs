using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using Moq;
using System;
using Xunit;

namespace KDParticleEngineTests.Behaviors
{
    public class BehaviorFactoryTests
    {
        #region Method Tests
        [Theory]
        [InlineData(BehaviorType.EaseOutBounce, typeof(EaseOutBounceBehavior))]
        [InlineData(BehaviorType.EaseIn, typeof(EaseInBehavior))]
        public void CreateBehaviors_WhenInvoked_CreatesCorrectBehavior(BehaviorType behaviorType, Type expectedBehaviorType)
        {
            //Arrange
            var mockRandomizerService = new Mock<IRandomizerService>();
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    TypeOfBehavior = behaviorType
                }
            };
            var factory = new BehaviorFactory();

            //Act
            var actual = factory.CreateBehaviors(settings, mockRandomizerService.Object);

            //Assert
            Assert.Single(actual);
            Assert.Equal(actual[0].GetType(), expectedBehaviorType);
        }
        #endregion
    }
}
