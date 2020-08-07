using System;
using Moq;
using Xunit;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;

namespace KDParticleEngineTests.Behaviors
{
    /// <summary>
    /// Holds tests for the <see cref="BehaviorFactory"/> class.
    /// </summary>
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
