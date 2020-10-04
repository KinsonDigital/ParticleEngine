// <copyright file="SceneFactoryTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Fakes
{
    using System;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.Scenes;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    public class SceneFactoryTests
    {
        private readonly Mock<IRenderer> mockRenderer;
        private readonly Mock<IContentLoader> mockContentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneFactoryTests"/> class.
        /// </summary>
        public SceneFactoryTests()
        {
            this.mockRenderer = new Mock<IRenderer>();
            this.mockContentLoader = new Mock<IContentLoader>();
        }

        #region Method Tests
        [Fact]
        public void CreateScene_WithNullRenderer_ThrowsException()
        {
            // Arrange
            var factory = new SceneFactory(null, this.mockContentLoader.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateScene(It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void CreateScene_WithNullContentLoader_ThrowsException()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, null);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                factory.CreateScene(It.IsAny<string>());
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void CreateScene_WithInvalidSceneKey_ThrowsException()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentException>(() =>
            {
                factory.CreateScene("invalid-key");
            }, "sceneKey (Parameter 'The given scene key 'invalid-key' is invalid.')");
        }

        [Fact]
        public void CreateScene_WithAngularVelocitySceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("angular-velocity-scene");

            // Assert
            Assert.IsType<AngularVelocityScene>(actual);
        }

        [Fact]
        public void CreateScene_WithXVelocitySceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("x-velocity-scene");

            // Assert
            Assert.IsType<XVelocityScene>(actual);
        }

        [Fact]
        public void CreateScene_WithYVelocitySceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("y-velocity-scene");

            // Assert
            Assert.IsType<YVelocityScene>(actual);
        }

        [Fact]
        public void CreateScene_WithSizeVelocitySceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("size-scene");

            // Assert
            Assert.IsType<SizeScene>(actual);
        }

        [Fact]
        public void CreateScene_WithRedColorSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("red-color-scene");

            // Assert
            Assert.IsType<RedColorScene>(actual);
        }

        [Fact]
        public void CreateScene_WithGreenColorSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("green-color-scene");

            // Assert
            Assert.IsType<GreenColorScene>(actual);
        }

        [Fact]
        public void CreateScene_WithBlueColorSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("blue-color-scene");

            // Assert
            Assert.IsType<BlueColorScene>(actual);
        }

        [Fact]
        public void CreateScene_WithAlphaColorSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("alpha-color-scene");

            // Assert
            Assert.IsType<AlphaColorScene>(actual);
        }

        [Fact]
        public void CreateScene_WithColorTransitionSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("color-transition-scene");

            // Assert
            Assert.IsType<ColorTransitionScene>(actual);
        }

        [Fact]
        public void CreateScene_WithBurstingEffectSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("bursting-effect-scene");

            // Assert
            Assert.IsType<BurstingEffectScene>(actual);
        }

        [Fact]
        public void CreateScene_WithSpawnLocationSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("spawn-location-scene");

            // Assert
            Assert.IsType<SpawnLocationScene>(actual);
        }

        [Fact]
        public void CreateScene_WithRandomChoiceSceneKey_CreatesCorrectScene()
        {
            // Arrange
            var factory = new SceneFactory(this.mockRenderer.Object, this.mockContentLoader.Object);

            // Act
            var actual = factory.CreateScene("random-choice-scene");

            // Assert
            Assert.IsType<RandomChoiceScene>(actual);
        }
        #endregion
    }
}
