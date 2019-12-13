﻿using KDParticleEngine;
using Xunit;
using System;
using System.Drawing;

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
        #endregion
    }
}
