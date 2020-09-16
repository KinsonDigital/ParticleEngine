// <copyright file="SceneManagerTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using System;
    using Microsoft.Xna.Framework;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    public class SceneManagerTests
    {
        private readonly Mock<ITexture> mockTexture;
        private readonly Mock<IContentLoader> mockContentLoader;
        private readonly int windowWidth = 800;
        private readonly int windowHeight = 400;

        public SceneManagerTests()
        {
            this.mockTexture = new Mock<ITexture>();

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.Load("prev-button")).Returns(this.mockTexture.Object);
            this.mockContentLoader.Setup(m => m.Load("next-button")).Returns(this.mockTexture.Object);
        }

        #region Constructor Tests
        [Fact]
        public void Ctor_WithNullSpriteBatch_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var manager = new SceneManager(null, null, this.windowWidth, this.windowHeight);
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void Ctor_WithNullContentLoader_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var manager = new SceneManager(new Mock<ISpriteRenderer>().Object, null, this.windowWidth, this.windowHeight);
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultEnabledResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            var actual = manager.Enabled;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultUpdateOrderResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            var actual = manager.UpdateOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultDrawOrderResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            var actual = manager.DrawOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultVisibleResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            var actual = manager.Visible;

            // Assert
            Assert.True(actual);
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void UpdateOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            manager.UpdateOrder = 1234;
            var actual = manager.UpdateOrder;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void DrawOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            manager.DrawOrder = 1234;
            var actual = manager.DrawOrder;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void Visible_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act
            manager.Visible = false;
            var actual = manager.Visible;

            // Assert
            Assert.False(actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void AddScene_WhenInvoked_AddsSceneToManager()
        {
            // Arrange
            var manager = CreateSceneManager();
            var mockScene = new Mock<IScene>();

            // Act
            manager.AddScene(mockScene.Object);
            var actual = manager.Scenes;

            // Assert
            Assert.Single(actual);
        }

        [Fact]
        public void LoadContent_WhenInvoked_LoadsContentForAllScenes()
        {
            // Arrange
            var manager = CreateSceneManager();
            var mockSceneA = new Mock<IScene>();
            var mockSceneB = new Mock<IScene>();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            manager.LoadContent();

            // Assert
            mockSceneA.Verify(m => m.LoadContent(), Times.Once());
            mockSceneB.Verify(m => m.LoadContent(), Times.Once());
        }

        [Fact]
        public void Update_WhenDisabled_DoesNotUpdateAnyScenes()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            var mockSceneB = new Mock<IScene>();

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.Enabled = false;

            // Act
            manager.Update(new GameTime());

            // Assert
            mockSceneA.Verify(m => m.Update(It.IsAny<GameTime>()), Times.Never());
            mockSceneB.Verify(m => m.Update(It.IsAny<GameTime>()), Times.Never());
        }

        [Fact]
        public void Update_WhenInvoked_UpdatesCurrentSceneOnly()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            var mockSceneB = new Mock<IScene>();

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.NextScene();

            // Act
            manager.Update(new GameTime());

            // Assert
            mockSceneA.Verify(m => m.Update(It.IsAny<GameTime>()), Times.Never());
            mockSceneB.Verify(m => m.Update(It.IsAny<GameTime>()), Times.Once());
        }

        [Fact]
        public void Draw_WhenDisabled_DoesNotDrawAnyScenes()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            var mockSceneB = new Mock<IScene>();

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.Visible = false;

            // Act
            manager.Draw(new GameTime());

            // Assert
            mockSceneA.Verify(m => m.Draw(It.IsAny<GameTime>()), Times.Never());
            mockSceneB.Verify(m => m.Draw(It.IsAny<GameTime>()), Times.Never());
        }

        [Fact]
        public void Draw_WhenInvoked_DrawsCurrentSceneOnly()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            var mockSceneB = new Mock<IScene>();

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.NextScene();

            // Act
            manager.Draw(new GameTime());

            // Assert
            mockSceneA.Verify(m => m.Draw(It.IsAny<GameTime>()), Times.Never());
            mockSceneB.Verify(m => m.Draw(It.IsAny<GameTime>()), Times.Once());
        }

        [Theory]
        [InlineData(0, 1, 0)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 5, 1)]
        [InlineData(5, 2, 2)]
        public void NextScene_WhenMovingPastLastScene_ReturnsLastSceneIndex(int scenesToAdd, int moveSceneCount, int expectedSceneIndex)
        {
            // Arrange
            var manager = CreateSceneManager();

            for (var i = 0; i < scenesToAdd; i++)
            {
                manager.AddScene(new Mock<IScene>().Object);
            }

            // Act
            for (var i = 0; i < moveSceneCount; i++)
            {
                manager.NextScene();
            }

            var actual = manager.CurrentSceneIndex;

            // Assert
            Assert.Equal(expectedSceneIndex, actual);
        }

        [Theory]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 0, 5, 0)]
        [InlineData(3, 5, 1, 1)]
        [InlineData(0, 0, 1, 0)]
        public void PreviousScene_WithNoScenes_ReturnsCorrectSceneIndex(int scenesToAdd, int nextSceneCount, int prevSceneCount, int expectedSceneIndex)
        {
            // Arrange
            var manager = CreateSceneManager();

            for (var i = 0; i < scenesToAdd; i++)
            {
                manager.AddScene(new Mock<IScene>().Object);
            }

            for (var i = 0; i < nextSceneCount; i++)
            {
                manager.NextScene();
            }

            // Act
            for (var i = 0; i < prevSceneCount; i++)
            {
                manager.PreviousScene();
            }

            var actual = manager.CurrentSceneIndex;

            // Assert
            Assert.Equal(expectedSceneIndex, actual);
        }
        #endregion

        private SceneManager CreateSceneManager()
        {
            return new SceneManager(new Mock<ISpriteRenderer>().Object, this.mockContentLoader.Object, this.windowWidth, this.windowHeight);
        }
    }
}
