using Xunit;
using System.Drawing;
using KDParticleEngine;
using KDParticleEngine.Behaviors;

namespace KDParticleEngineTests
{
    /// <summary>
    /// Unit tests to test the <see cref="Particle{IFakeTexture}"/> class.
    /// </summary>
    public class ParticleTests
    {
        #region Constructor Tests
        //TODO: Need to add ctor tests
        #endregion


        #region Prop Tests
        [Fact]
        public void Position_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                Position = new PointF(11, 22)
            };

            //Act
            var actual = particle.Position;

            //Assert
            Assert.Equal(new PointF(11, 22), actual);
        }


        [Fact]
        public void Angle_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                Angle = 1234f
            };

            //Act
            var actual = particle.Angle;

            //Assert
            Assert.Equal(1234f, actual);
        }


        [Fact]
        public void TintColor_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                TintColor = ParticleColor.FromArgb(11, 22, 33, 44)
            };

            //Act
            var actual = particle.TintColor;

            //Assert
            Assert.Equal(ParticleColor.FromArgb(11, 22, 33, 44), actual);
        }


        [Fact]
        public void Size_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                Size = 1019f
            };

            //Act
            var actual = particle.Size;

            //Assert
            Assert.Equal(1019f, actual);
        }


        [Fact]
        public void LifeTime_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                LifeTime = 7784
            };

            //Act
            var actual = particle.LifeTime;

            //Assert
            Assert.Equal(7784, actual);
        }


        [Fact]
        public void IsAlive_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                IsAlive = true
            };

            //Assert
            Assert.True(particle.IsAlive);
        }


        [Fact]
        public void IsDead_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new IBehavior[0])
            {
                IsDead = false
            };

            //Assert
            Assert.False(particle.IsDead);
        }
        #endregion
    }
}
