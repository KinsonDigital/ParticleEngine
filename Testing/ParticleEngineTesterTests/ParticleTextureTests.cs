// <copyright file="ParticleTextureTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using Moq;
    using ParticleEngineTester;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ParticleTexture"/> class.
    /// </summary>
    public class ParticleTextureTests
    {
        #region Prop Tests
        [Fact]
        public void Width_WhenGettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var mockTexture = new Mock<ITexture>();
            mockTexture.SetupGet(p => p.Width).Returns(123);

            var texture = new ParticleTexture(mockTexture.Object);

            // Act
            var actual = texture.Width;

            // Assert
            Assert.Equal(123, actual);
        }

        [Fact]
        public void Height_WhenGettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var mockTexture = new Mock<ITexture>();
            mockTexture.SetupGet(p => p.Height).Returns(123);

            var texture = new ParticleTexture(mockTexture.Object);

            // Act
            var actual = texture.Height;

            // Assert
            Assert.Equal(123, actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void Dispose_WhenInvoked_DisposesOfInternalTextureOnlyOnce()
        {
            // Arrange
            var mockTexture = new Mock<ITexture>();
            var texture = new ParticleTexture(mockTexture.Object);

            // Act
            texture.Dispose();
            texture.Dispose();

            // Assert
            mockTexture.Verify(m => m.Dispose(), Times.Once());
        }
        #endregion
    }
}
