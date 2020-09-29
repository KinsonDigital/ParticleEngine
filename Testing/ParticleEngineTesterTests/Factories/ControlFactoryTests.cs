// <copyright file="ControlFactoryTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Factories
{
    using System;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTester.Factories;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    public class ControlFactoryTests
    {
        private readonly Mock<IRenderer> mockRenderer;
        private readonly Mock<IContentLoader> mockContentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFactoryTests"/> class.
        /// </summary>
        public ControlFactoryTests()
        {
            this.mockRenderer = new Mock<IRenderer>();
            this.mockContentLoader = new Mock<IContentLoader>();
        }

        #region Method Tests
        [Fact]
        public void CreateLabel_WithNullRenderer_ThrowsException()
        {
            // Arrange
            var factory = new ControlFactory(null, this.mockContentLoader.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateLabel(It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void CreateLabel_WithNullContentLoader_ThrowsException()
        {
            // Arrange
            var factory = new ControlFactory(this.mockRenderer.Object, null);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateLabel(It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void CreateLabel_WhenInvoked_ReturnsLabel()
        {
            // Arrange
            var factory = new ControlFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateLabel("test-label", "test-text");

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("test-label", actual.Name);
            Assert.Equal("test-text", actual.Text);
        }

        [Fact]
        public void CreateButton_WithNullRenderer_ThrowsException()
        {
            // Arrange
            var factory = new ControlFactory(null, this.mockContentLoader.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateButton(It.IsAny<string>(), It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void CreateButton_WithNullContentLoader_ThrowsException()
        {
            // Arrange
            var factory = new ControlFactory(this.mockRenderer.Object, null);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateButton(It.IsAny<string>(), It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void CreateButton_WhenInvoked_ReturnsButton()
        {
            // Arrange
            var mockTexture = new Mock<ITexture>();
            mockTexture.SetupGet(p => p.Width).Returns(100);
            mockTexture.SetupGet(p => p.Height).Returns(50);

            this.mockContentLoader.Setup(m => m.LoadTexture("test-content"))
                .Returns(mockTexture.Object);

            var factory = new ControlFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateButton("test-button", "test-content");

            // Assert
            Assert.NotNull(actual);
            Assert.Equal("test-button", actual.Name);
        }

        [Fact]
        public void CreateMouse_WhenInvoked_CreatesInstanceOfMouse()
        {
            // Arrange
            var factory = new ControlFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateMouse();

            // Assert
            Assert.NotNull(actual);
        }
        #endregion
    }
}
