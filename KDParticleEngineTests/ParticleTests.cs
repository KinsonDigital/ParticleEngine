﻿using System;
using Moq;
using Xunit;
using System.Drawing;
using KDParticleEngine;

namespace KDParticleEngineTests
{
    /// <summary>
    /// Unit tests to test the <see cref="Particle{IFakeTexture}"/> class.
    /// </summary>
    public class ParticleTests : IDisposable
    {
        #region Private Fields
        private Mock<IFakeTexture> _mockTexture;
        #endregion


        #region Constructors
        public ParticleTests() => _mockTexture = new Mock<IFakeTexture>();
        #endregion


        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoking_ProperlySetsUpObject()
        {
            //Arrange/Act
            var particle = new Particle(new PointF(1234, 5678), new PointF(12,34), 11.11f, 22.22f, Color.FromArgb(11, 22, 33, 44), 33.33f, 44);

            //Assert
            Assert.Equal(new PointF(1234, 5678), particle.Position);
            Assert.Equal(11.11f, particle.Angle);
            Assert.Equal(Color.FromArgb(11, 22, 33, 44), particle.TintColor);
            Assert.Equal(33.33f, particle.Size);
            Assert.Equal(44, particle.LifeTime);
        }
        #endregion


        #region Prop Tests
        [Fact]
        public void Position_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
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
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
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
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
            {
                TintColor = Color.FromArgb(11, 22, 33, 44)
            };

            //Act
            var actual = particle.TintColor;

            //Assert
            Assert.Equal(Color.FromArgb(11, 22, 33, 44), actual);
        }


        [Fact]
        public void Size_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
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
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
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
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
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
            var particle = new Particle(new PointF(0, 0), new PointF(0, 0), 0f, 0f, Color.Empty, 0f, 0)
            {
                IsDead = false
            };

            //Assert
            Assert.False(particle.IsDead);
        }
        #endregion


        #region Public Methods
        public void Dispose() => _mockTexture = null;
        #endregion
    }
}
