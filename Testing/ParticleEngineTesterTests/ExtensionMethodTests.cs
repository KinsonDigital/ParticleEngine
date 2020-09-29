// <copyright file="ExtensionMethodTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using System.Drawing;
    using Microsoft.Xna.Framework;
    using ParticleEngineTester;
    using Xunit;

    public class ExtensionMethodTests
    {
        #region Method Tests
        [Fact]
        public void ToPointF_WhenInvoked_ReturnsCorrectValue()
        {
            // Arrange
            var vector = new Vector2(11, 22);

            // Act
            var actual = vector.ToPointF();

            // Assert
            Assert.Equal(new PointF(11, 22), actual);
        }

        [Fact]
        public void ToUppperFirstChar_WhenInvoked_ReturnsCorrectResult()
        {
            // Arrange
            var value = "test-value";

            // Act
            var actual = value.ToUpperFirstChar();

            // Assert
            Assert.Equal("Test-value", actual);
        }

        [Theory]
        [InlineData("test-title", "Test Title")]
        [InlineData("test title", "test title")]
        public void ToTitle_WhenInvoked_ReturnsCorrectResult(string title, string expected)
        {
            // Act
            var actual = title.ToTitle();

            // Assert
            Assert.Equal(expected, actual);
        }
        #endregion
    }
}
