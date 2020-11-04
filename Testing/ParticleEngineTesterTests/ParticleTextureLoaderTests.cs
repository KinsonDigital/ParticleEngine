// <copyright file="ParticleTextureLoaderTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using Moq;
    using ParticleEngineTester;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="ParticleTextureLoader"/> class.
    /// </summary>
    public class ParticleTextureLoaderTests
    {
        private readonly Mock<ITexture> mockTexture;
        private readonly Mock<IContentLoader> mockContentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleTextureLoaderTests"/> class.
        /// </summary>
        public ParticleTextureLoaderTests()
        {
            this.mockTexture = new Mock<ITexture>();

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.LoadTexture("test-asset-name")).Returns(this.mockTexture.Object);
        }

        #region Method Tests
        [Fact]
        public void LoadTexture_WhenInvoked_LoadsParticleTexture()
        {
            // Arrange
            var loader = new ParticleTextureLoader(this.mockContentLoader.Object);

            // Act
            var actual = loader.LoadTexture("test-asset-name");

            // Assert
            Assert.NotNull(actual);
            this.mockContentLoader.Verify(m => m.LoadTexture("test-asset-name"), Times.Once());
        }
        #endregion
    }
}
