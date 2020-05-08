using KDParticleEngine;
using Xunit;
using System;
using System.Drawing;
using KDParticleEngine.Behaviors;
using Moq;
using System.Collections.Generic;

namespace KDParticleEngineTests
{
    /// <summary>
    /// Provides extensions to various things to help make better code.
    /// </summary>
    public class ExtensionMethodsTests
    {
        #region Method Tests
        [Fact]
        public void Next_WhenInvokedWithMinLessThanMax_ReturnsValueWithinMinAndMax()
        {
            //Arrange
            var random = new Random();
            var expected = true;

            //Act
            var randomNum = random.Next(50f, 100f);
            var actual = randomNum >= 50f && randomNum <= 100f;

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Next_WhenInvokedWithMinMoreThanMax_ReturnsMaxValue()
        {
            //Arrange
            var random = new Random();
            var expected = 98f;

            //Act
            var actual = random.Next(124f, 98f);

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void Add_WhenInvoking_ReturnsCorrectValue()
        {
            //Arrange
            var pointA = new PointF(10, 20);
            var pointB = new PointF(5, 3);

            //Act
            var result = pointA.Add(pointB);

            //Assert
            Assert.Equal(new PointF(15f, 23f), result);
        }


        [Fact]
        public void Mult_WhenInvoking_ReturnsCorrectValue()
        {
            //Arrange
            var point = new PointF(10, 20);

            //Act
            var result = point.Mult(2);

            //Assert
            Assert.Equal(new PointF(20f, 40f), result);
        }


        [Fact]
        public void Count_WhenInvokingListVersion_ReturnsCorrectValue()
        {
            //Arrange
            var mockBehavior = new Mock<IBehavior>();

            var particles = new List<Particle>();

            for (int i = 0; i < 20; i++)
            {
                particles.Add(new Particle(new IBehavior[] { mockBehavior.Object }) { IsAlive = i > 10 });
            }

            //Act
            var actual = particles.Count(p => p.IsAlive);

            //Assert
            Assert.Equal(9, actual);
        }


        [Fact]
        public void Count_WhenInvokingArrayVersion_ReturnsCorrectValue()
        {
            //Arrange
            var mockBehavior = new Mock<IBehavior>();

            Particle[] particles;

            var tempList = new List<Particle>();
            for (int i = 0; i < 20; i++)
            {
                tempList.Add(new Particle(new IBehavior[] { mockBehavior.Object }) { IsAlive = i > 10 });
            }
            particles = tempList.ToArray();

            //Act
            var actual = particles.Count(p => p.IsAlive);

            //Assert
            Assert.Equal(9, actual);
        }


        [Theory]
        [InlineData("FF0A141EFF0A141E", new byte[] { 255, 10, 20, 30 })]
        [InlineData("FF0A141E", new byte[] { 255, 10, 20, 30 })]
        [InlineData("#FF0A141E", new byte[] { 255, 10, 20, 30 })]
        [InlineData("FF0A141", new byte[] { 255, 10, 20, 255 })]
        [InlineData("FF0A14", new byte[] { 255, 10, 20, 255 })]
        [InlineData("FF0A1", new byte[] { 255, 10, 255, 255 })]
        [InlineData("FF0A", new byte[] { 255, 10, 255, 255 })]
        [InlineData("FF0", new byte[] { 255, 255, 255, 255 })]
        [InlineData("FF", new byte[] { 255, 255, 255, 255 })]
        [InlineData("F", new byte[] { 255, 255, 255, 255 })]
        [InlineData("", new byte[] { 255, 255, 255, 255 })]
        public void ToParticleColor_WhenInvoked_ReturnsCorrectParticleColor(string hexValue, byte[] expected)
        {
            //Act
            var actual = hexValue.ToParticleColor();

            //Assert
            Assert.Equal(expected[0], actual.A);
            Assert.Equal(expected[1], actual.R);
            Assert.Equal(expected[2], actual.G);
            Assert.Equal(expected[3], actual.B);
        }


        [Theory]
        [InlineData("", false)]
        [InlineData(null, false)]
        [InlineData("#FF112233", true)]
        [InlineData("#FF11ZZ33", false)]
        public void IsValidHexValue_WhenInvoked_ReturnsCorrectResult(string hexValue, bool expected)
        {
            //Act
            var actual = hexValue.IsValidHexValue();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(123.0, "0000007B")]
        [InlineData(626152704.0, "25525500")]
        [InlineData(1234.1234, "000004D2")]
        [InlineData(12345.12345, "00003039")]
        [InlineData(123456.123456, "0001E240")]
        [InlineData(1234567.1234567, "0012D687")]
        [InlineData(12345678.12345678, "00BC614E")]
        public void ToHexValue_WhenInvoked_ReturnsCorrectValue(double valueToConvert, string expected)
        {
            //Act
            var actual = valueToConvert.ToHexValue();

            //Assert
            Assert.Equal(expected, actual);
        }


        [Fact]
        public void ToBase10Number_WhenInvoked_ReturnsCorrectValue()
        {
            var result = $"{626152704:X8}";

            //Arrange
            var value = "25525500";

            //Act
            var actual = value.ToBase10Value();

            //Assert
            Assert.Equal(626152704.0, actual);
        }
        #endregion
    }
}
