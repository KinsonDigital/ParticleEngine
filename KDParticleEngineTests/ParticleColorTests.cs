﻿using KDParticleEngine;
using System;
using System.Drawing;
using System.Linq;
using System.Reflection;
using Xunit;

namespace KDParticleEngineTests
{
    public class ParticleColorTests
    {
        #region Prop Tests
        [Fact]
        public void ColorProps_WhenGettingValues_ReturnsCorrrectValue()
        {
            //Arrange
            var propsToTest = GetColorPropNames();


            foreach (var prop in propsToTest)
            {
                var expected = Color.FromName(prop);
                var actual = GetColorPropValue(prop);

                Assert.Equal(expected.A, actual.A);
                Assert.Equal(expected.R, actual.R);
                Assert.Equal(expected.G, actual.G);
                Assert.Equal(expected.B, actual.B);
            }
        }
        #endregion


        #region Method Tests
        [Fact]
        public void GetBrightness_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var color = new ParticleColor(255, 24, 98, 118);

            //Act
            var actual = color.GetBrightness();

            //Assert
            Assert.Equal(27.843138f, actual);
        }


        [Theory]
        [InlineData(118, 98, 118, 300f)]
        [InlineData(10, 30, 20, 150f)]
        [InlineData(10, 10, 10, 0f)]
        [InlineData(24, 98, 118, 192.76596f)]
        public void GetHue_WhenInvoked_ReturnsCorrectValue(byte red, byte green, byte blue, float result)
        {
            //Arrange
            var color = new ParticleColor(255, red, green, blue);

            //Act
            var actual = color.GetHue();

            //Assert
            Assert.Equal(result, actual);
        }


        [Fact]
        public void GetSaturation_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var color = new ParticleColor(255, 24, 98, 118);

            //Act
            var actual = color.GetSaturation();

            //Assert
            Assert.Equal(66.19718, Math.Round(actual, 5));
        }


        [Theory]
        [InlineData(255, 255, 255, 255, true)]
        [InlineData(11, 22, 33, 44, false)]
        public void EqualOperator_WithEqualColors_ReturnsTrue(byte alpha, byte red, byte green, byte blue, bool expectedResult)
        {
            //Arrange
            var colorA = new ParticleColor(255, 255, 255, 255);
            var colorB = new ParticleColor(alpha, red, green, blue);

            //Act
            var actual = colorA == colorB;

            //Assert
            Assert.Equal(expectedResult, actual);
        }


        [Theory]
        [InlineData(255, 255, 255, 255, false)]
        [InlineData(11, 22, 33, 44, true)]
        public void NotEqualOperator_WithNotEqualColors_ReturnsTrue(byte alpha, byte red, byte green, byte blue, bool expectedResult)
        {
            //Arrange
            var colorA = new ParticleColor(255, 255, 255, 255);
            var colorB = new ParticleColor(alpha, red, green, blue);

            //Act
            var actual = colorA != colorB;

            //Assert
            Assert.Equal(expectedResult, actual);
        }


        [Fact]
        public void Equals_WithTwoDifferentObjectTypes_ReturnsFalse()
        {
            //Arrange
            var color = new ParticleColor(11, 22, 33, 44);
            var otherObj = new object();

            //Act
            var actual = color.Equals(otherObj);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithTwoNonMatchingObject_ReturnsFalse()
        {
            //Arrange
            var clrA = new ParticleColor(11, 22, 33, 44);
            var clrB = new ParticleColor(11, 22, 33, 99);

            //Act
            var actual = clrA.Equals(clrB);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithTwoMatchingObject_ReturnsTrue()
        {
            //Arrange
            var clrA = new ParticleColor(11, 22, 33, 44);
            var clrB = new ParticleColor(11, 22, 33, 44);

            //Act
            var actual = clrA.Equals(clrB);

            //Assert
            Assert.True(actual);
        }


        [Fact]
        public void GetHashCode_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var colorA = new ParticleColor(11, 22, 33, 44);
            var colorB = new ParticleColor(11, 22, 33, 44);

            //Act
            var actual = colorA.GetHashCode() == colorB.GetHashCode();

            //Assert
            Assert.True(actual);
        }
        #endregion


        #region Private Methods
        /// <summary>
        /// Returns all of the names of the <see cref="ParticleColor"/> color property names.
        /// </summary>
        /// <returns></returns>
        private string[] GetColorPropNames() => typeof(ParticleColor).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(ParticleColor)).Select(p => p.Name).ToArray();


        private ParticleColor GetColorPropValue(string name)
        {
            var foundProp = typeof(ParticleColor).GetProperties(BindingFlags.Public | BindingFlags.Static)
                .Where(p => p.PropertyType == typeof(ParticleColor) && p.Name == name).FirstOrDefault();


            return foundProp.GetValue(null, null) as ParticleColor;
        }
        #endregion
    }
}
