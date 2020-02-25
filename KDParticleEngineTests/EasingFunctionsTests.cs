using KDParticleEngine;
using Xunit;

namespace KDParticleEngineTests
{
    public class EasingFunctionsTests
    {
        #region Method Tests
        [Theory]
        [InlineData(0.3636, 2.0, 3.0, 3.0, 2.33326667)]
        [InlineData(0.4, 2.0, 3.0, 1.0, 4.7299999999999995)]
        [InlineData(0.8, 2.0, 3.0, 1.0, 4.82)]
        [InlineData(1.0, 2.0, 3.0, 1.0, 5.090511363636364)]
        public void EaseOutBounce_WhenInvoked_ReturnsCorrectValue(double t, double b, double c, double d, double expected)
        {
            //Act
            var actual = EasingFunctions.EaseOutBounce(t, b, c, d);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void EaseInQuad_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var expected = 30.444444444444443;

            //Act
            var actual = EasingFunctions.EaseInQuad(16, 2.0, 4.0, 6.0);

            //Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
