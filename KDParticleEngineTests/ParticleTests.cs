﻿using Xunit;
using System.Drawing;
using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using Moq;
using System;
using KDParticleEngineTests.Fakes;

namespace KDParticleEngineTests
{
    /// <summary>
    /// Holds tests for the <see cref="Particle"/> class.
    /// </summary>
    public class ParticleTests
    {
        #region Private Fields
        private readonly TimeSpan _frameTime;
        private readonly Mock<IBehavior> _mockBehavior;
        #endregion


        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticleTests"/>.
        /// </summary>
        public ParticleTests()
        {
            _frameTime = new TimeSpan(0, 0, 0, 0, 16);
            _mockBehavior = new Mock<IBehavior>();
            _mockBehavior.SetupGet(p => p.Enabled).Returns(true);
        }
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


        #region Method Tests
        [Fact]
        public void Update_WithDisabledBehavior_BehaviorShouldNotUpdate()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.Enabled).Returns(false);
            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            _mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Never);
        }


        [Fact]
        public void Update_WithEnabledBehavior_BehaviorShouldUpdate()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            _mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Once());
        }


        [Fact]
        public void Update_WhenApplyingToXAttribute_UpdatesPositionX()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.X);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.Position.X.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToYAttribute_UpdatesPositionY()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.Y);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.Position.Y.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToAngleAttribute_UpdatesAngle()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.Angle);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.Angle.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToSizeAttribute_UpdatesSize()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.Size);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.Size.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToRedColorComponentAttribute_UpdatesRedColorComponent()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.RedColorComponent);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.TintColor.R.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToGreenColorComponentAttribute_UpdatesGreenColorComponent()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.GreenColorComponent);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.TintColor.G.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToBlueColorComponentAttribute_UpdatesBlueColorComponent()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.BlueColorComponent);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);
            
            //Assert
            Assert.Equal("123", particle.TintColor.B.ToString());
        }


        [Fact]
        public void Update_WhenApplyingToAlphaColorComponentAttribute_UpdatesAlphaColorComponent()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.AlphaColorComponent);

            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);

            //Assert
            Assert.Equal("123", particle.TintColor.A.ToString());
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsAllBehaviors()
        {
            //Arrange
            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Reset();

            //Assert
            _mockBehavior.Verify(m => m.Reset(), Times.Once());
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsAngle()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.Angle);
            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);
            particle.Reset();

            //Assert
            Assert.Equal(0, particle.Angle);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsTintColor()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Value).Returns("123");
            _mockBehavior.SetupGet(p => p.ApplyToAttribute).Returns(ParticleAttribute.RedColorComponent);
            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(_frameTime);
            particle.Reset();

            //Assert
            Assert.Equal(255, particle.TintColor.R);
        }


        [Fact]
        public void Reset_WhenInvoked_ResetsIsAlive()
        {
            //Arrange
            _mockBehavior.SetupGet(p => p.Enabled).Returns(false);
            var particle = new Particle(new[] { _mockBehavior.Object });

            //Act
            particle.Update(new TimeSpan(0, 0, 0, 10, 0));
            particle.Reset();

            //Assert
            Assert.True(particle.IsAlive);
        }


        [Fact]
        public void Equals_WithDifferentObjects_ReturnsFalse()
        {
            //Arrange
            var particle = new Particle(It.IsAny<IBehavior[]>());
            var obj = new object();

            //Act
            var actual = particle.Equals(obj);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithNonEqualObjects_ReturnsFalse()
        {
            //Arrange
            var particleA = new Particle(It.IsAny<IBehavior[]>())
            {
                Size = 123f
            };
            var particleB = new Particle(It.IsAny<IBehavior[]>());

            //Act
            var actual = particleA.Equals(particleB);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithEqualObjects_ReturnsTrue()
        {
            //Arrange
            var particleA = new Particle(It.IsAny<IBehavior[]>());
            var particleB = new Particle(It.IsAny<IBehavior[]>());

            //Act
            var actual = particleA.Equals(particleB);

            //Assert
            Assert.True(actual);
        }


        [Fact]
        public void GetHashCode_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var behaviors = new IBehavior[]
            {
                _mockBehavior.Object
            };
            var particleA = new Particle(behaviors);
            var particleB = new Particle(behaviors);

            //Act
            var actual = particleA.GetHashCode() == particleB.GetHashCode();

            //Assert
            Assert.True(actual);
        }
        #endregion
    }
}
