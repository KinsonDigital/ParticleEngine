// <copyright file="SceneManagerTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using System;
    using Microsoft.Xna.Framework;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.UI;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="SceneManager"/> class.
    /// </summary>
    public class SceneManagerTests
    {
        private readonly Mock<ITexture> mockTexture;
        private readonly Mock<IContentLoader> mockContentLoader;
        private readonly Mock<IControlFactory> mockCtrlFactory;
        private readonly Mock<IButton> mockNextButton;
        private readonly Mock<IButton> mockPrevButton;

        private readonly int windowWidth = 800;
        private readonly int windowHeight = 400;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManagerTests"/> class.
        /// </summary>
        public SceneManagerTests()
        {
            this.mockTexture = new Mock<ITexture>();

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.LoadTexture("Graphics/prev-button")).Returns(this.mockTexture.Object);
            this.mockContentLoader.Setup(m => m.LoadTexture("Graphics/next-button")).Returns(this.mockTexture.Object);

            this.mockNextButton = new Mock<IButton>();
            this.mockPrevButton = new Mock<IButton>();

            this.mockCtrlFactory = new Mock<IControlFactory>();
            this.mockCtrlFactory.Setup(m => m.CreateButton("next-btn", It.IsAny<string>())).Returns(this.mockNextButton.Object);
            this.mockCtrlFactory.Setup(m => m.CreateButton("prev-btn", It.IsAny<string>())).Returns(this.mockPrevButton.Object);
        }

        #region Constructor Tests
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
        public void AddScene_WithDuplicateSceneNames_ThrowsException()
        {
            // Arrange
            var manager = CreateSceneManager();
            var mockScene = new Mock<IScene>();
            mockScene.SetupGet(p => p.Name).Returns("duplicate-scene");

            manager.AddScene(mockScene.Object);

            // Act & Assert
            AssertHelpers.ThrowsWithMessage<Exception>(() =>
            {
                manager.AddScene(mockScene.Object);
            }, $"A scene with the name 'duplicate-scene' already has been added to the '{nameof(SceneManager)}'.  Duplicate scene names not aloud.'");
        }

        [Theory]
        [InlineData("test-scene-B", 1)]
        [InlineData("invalid-scene-name", 0)]
        public void ActivateScene_WithExistingScene_ActivatesScene(string activeSceneName, int expectedIndex)
        {
            // Arrange
            var manager = CreateSceneManager();
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns("test-scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns("test-scene-B");

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            manager.ActivateScene(activeSceneName);
            var actual = manager.CurrentSceneIndex;

            // Assert
            Assert.Equal(expectedIndex, actual);
        }

        [Fact]
        public void ActivateScene_WhenInvoked_RaisesSceneChangedEvent()
        {
            // Arrange
            var sceneA = new Mock<IScene>();
            sceneA.SetupGet(p => p.Name).Returns(nameof(sceneA));

            var sceneB = new Mock<IScene>();
            sceneB.SetupGet(p => p.Name).Returns(nameof(sceneB));

            var manager = CreateSceneManager();
            manager.AddScene(sceneA.Object);
            manager.AddScene(sceneB.Object);

            // Act & Assert
            Assert.Raises<SceneChangedEventArgs>((handler) =>
            {
                manager.SceneChanged += handler;
            }, (handler) =>
            {
                manager.SceneChanged -= handler;
            }, () =>
            {
                manager.ActivateScene("sceneB");
            });
        }

        [Fact]
        public void LoadContent_WhenInvoked_LoadsContentForAllScenes()
        {
            // Arrange
            var manager = CreateSceneManager();
            var mockSceneA = new Mock<IScene>();
            mockSceneA.Setup(p => p.Name).Returns("scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.Setup(p => p.Name).Returns("scene-B");

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            manager.LoadContent();

            // Assert
            mockSceneA.Verify(m => m.LoadContent(), Times.Once());
            mockSceneB.Verify(m => m.LoadContent(), Times.Once());
        }

        [Fact]
        public void Update_WhenNextButtonIsClicked_MovesToNextScene()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns("sceneA");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns("sceneB");

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            this.mockNextButton.Raise((button) => button.Click += null, new ClickedEventArgs("next-btn"));

            Assert.Equal(1, manager.CurrentSceneIndex);
        }

        [Fact]
        public void Update_WhenPreviousButtonIsClicked_MovesToPreviousScene()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns("sceneA");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns("sceneB");

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            this.mockPrevButton.Raise((button) => button.Click += null, new ClickedEventArgs("next-btn"));

            Assert.Equal(0, manager.CurrentSceneIndex);
        }

        [Fact]
        public void Update_WhenDisabled_DoesNotUpdateAnyScenes()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.Setup(p => p.Name).Returns("scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.Setup(p => p.Name).Returns("scene-B");

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
            mockSceneA.Setup(p => p.Name).Returns("scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.Setup(p => p.Name).Returns("scene-B");
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
        public void Update_WhenInvoked_NoNullReferenceExceptionThrown()
        {
            // Arrange
            var manager = CreateSceneManager();

            // Act & Assert
            AssertHelpers.DoesNotThrow<IndexOutOfRangeException>(() =>
            {
                manager.Update(new GameTime());
            });
        }

        [Fact]
        public void Update_WhenCurrentSceneIsFirstScene_PreviousButtonIsDisabled()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            var mockPrevButton = new Mock<IButton>();
            mockPrevButton.SetupProperty(p => p.Enabled);

            this.mockCtrlFactory.Setup(m => m.CreateButton(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(mockPrevButton.Object);

            var manager = CreateSceneManager();
            manager.AddScene(mockSceneA.Object);

            // Act
            manager.Update(new GameTime());

            // Assert
            Assert.False(mockPrevButton.Object.Enabled);
        }

        [Fact]
        public void Update_WhenCurrentSceneIsNotFirstScene_PreviousButtonIsEnabled()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns(nameof(mockSceneA));

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns(nameof(mockSceneB));

            var mockPrevButton = new Mock<IButton>();
            mockPrevButton.SetupProperty(p => p.Enabled);

            this.mockCtrlFactory.Setup(m => m.CreateButton("prev-btn", It.IsAny<string>()))
                .Returns(mockPrevButton.Object);

            var manager = CreateSceneManager();
            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.NextScene();

            // Act
            manager.Update(new GameTime());

            // Assert
            Assert.True(mockPrevButton.Object.Enabled);
        }

        [Fact]
        public void Update_WhenCurrentSceneIsLastScene_NextButtonIsDisabled()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns(nameof(mockSceneA));

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns(nameof(mockSceneB));

            var mockNextButton = new Mock<IButton>();
            mockNextButton.SetupProperty(p => p.Enabled);

            this.mockCtrlFactory.Setup(m => m.CreateButton("next-btn", It.IsAny<string>()))
                .Returns(mockNextButton.Object);

            var manager = CreateSceneManager();
            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            manager.NextScene();

            // Act
            manager.Update(new GameTime());

            // Assert
            Assert.False(mockNextButton.Object.Enabled);
        }

        [Fact]
        public void Update_WhenCurrentSceneIsNotLastScene_NextButtonIsEnabled()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns(nameof(mockSceneA));

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns(nameof(mockSceneB));

            var mockNextButton = new Mock<IButton>();
            mockNextButton.SetupProperty(p => p.Enabled);

            this.mockCtrlFactory.Setup(m => m.CreateButton("next-btn", It.IsAny<string>()))
                .Returns(mockNextButton.Object);

            var manager = CreateSceneManager();
            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);
            manager.NextScene();
            manager.PreviousScene();

            // Act
            manager.Update(new GameTime());

            // Assert
            Assert.True(mockNextButton.Object.Enabled);
        }

        [Fact]
        public void Draw_WhenDisabled_DoesNotDrawAnyScenes()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.Setup(p => p.Name).Returns("scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.Setup(p => p.Name).Returns("scene-B");
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
            mockSceneA.Setup(p => p.Name).Returns("scene-A");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.Setup(p => p.Name).Returns("scene-B");
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
        [InlineData(0, 1, -1)]
        [InlineData(1, 1, 0)]
        [InlineData(2, 5, 1)]
        [InlineData(5, 2, 2)]
        public void NextScene_WhenMovingPastLastScene_ReturnsLastSceneIndex(int scenesToAdd, int moveSceneCount, int expectedSceneIndex)
        {
            // Arrange
            var manager = CreateSceneManager();

            for (var i = 0; i < scenesToAdd; i++)
            {
                var newScene = new Mock<IScene>();
                newScene.SetupGet(p => p.Name).Returns($"scene-{i}");
                manager.AddScene(newScene.Object);
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

        [Fact]
        public void NextScene_WithNoSceneChangedSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var sceneA = new Mock<IScene>();
            sceneA.SetupGet(p => p.Name).Returns(nameof(sceneA));

            var sceneB = new Mock<IScene>();
            sceneB.SetupGet(p => p.Name).Returns(nameof(sceneB));

            var manager = CreateSceneManager();
            manager.AddScene(sceneA.Object);
            manager.AddScene(sceneB.Object);

            // Act & Assert
            AssertHelpers.DoesNotRaise<SceneChangedEventArgs>((handler) =>
            {
            }, (handler) =>
            {
            }, () =>
            {
                manager.NextScene();
            });
        }

        [Fact]
        public void NextScene_WhenInvoked_RaisesSceneChangedEvent()
        {
            // Arrange
            var sceneA = new Mock<IScene>();
            sceneA.SetupGet(p => p.Name).Returns(nameof(sceneA));

            var sceneB = new Mock<IScene>();
            sceneB.SetupGet(p => p.Name).Returns(nameof(sceneB));

            var manager = CreateSceneManager();
            manager.AddScene(sceneA.Object);
            manager.AddScene(sceneB.Object);

            // Act & Assert
            Assert.Raises<SceneChangedEventArgs>((handler) =>
            {
                manager.SceneChanged += handler;
            }, (handler) =>
            {
                manager.SceneChanged -= handler;
            }, () =>
            {
                manager.NextScene();
            });
        }

        [Theory]
        [InlineData(2, 1, 1, 0)]
        [InlineData(2, 0, 5, 0)]
        [InlineData(3, 5, 1, 1)]
        [InlineData(0, 0, 1, -1)]
        public void PreviousScene_WithNoScenes_ReturnsCorrectSceneIndex(int scenesToAdd, int nextSceneCount, int prevSceneCount, int expectedSceneIndex)
        {
            // Arrange
            var manager = CreateSceneManager();

            for (var i = 0; i < scenesToAdd; i++)
            {
                var newScene = new Mock<IScene>();
                newScene.SetupGet(p => p.Name).Returns($"scene-{i}");
                manager.AddScene(newScene.Object);
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

        [Fact]
        public void PreviousScene_WithNoSceneChangedSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var sceneA = new Mock<IScene>();
            sceneA.SetupGet(p => p.Name).Returns(nameof(sceneA));

            var sceneB = new Mock<IScene>();
            sceneB.SetupGet(p => p.Name).Returns(nameof(sceneB));

            var manager = CreateSceneManager();
            manager.AddScene(sceneA.Object);
            manager.AddScene(sceneB.Object);

            // Act & Assert
            AssertHelpers.DoesNotRaise<SceneChangedEventArgs>((handler) =>
            {
            }, (handler) =>
            {
            }, () =>
            {
                manager.PreviousScene();
            });
        }

        [Fact]
        public void PreviousScene_WhenInvoked_RaisesSceneChangedEvent()
        {
            // Arrange
            var sceneA = new Mock<IScene>();
            sceneA.SetupGet(p => p.Name).Returns(nameof(sceneA));

            var sceneB = new Mock<IScene>();
            sceneB.SetupGet(p => p.Name).Returns(nameof(sceneB));

            var manager = CreateSceneManager();
            manager.AddScene(sceneA.Object);
            manager.AddScene(sceneB.Object);

            // Act & Assert
            Assert.Raises<SceneChangedEventArgs>((handler) =>
            {
                manager.SceneChanged += handler;
            }, (handler) =>
            {
                manager.SceneChanged -= handler;
            }, () =>
            {
                manager.PreviousScene();
            });
        }

        [Fact]
        public void Dispose_WhenInvoked_DisposesOfAllScenes()
        {
            // Arrange
            var mockSceneA = new Mock<IScene>();
            mockSceneA.SetupGet(p => p.Name).Returns("sceneA");

            var mockSceneB = new Mock<IScene>();
            mockSceneB.SetupGet(p => p.Name).Returns("sceneB");

            var manager = CreateSceneManager();

            manager.AddScene(mockSceneA.Object);
            manager.AddScene(mockSceneB.Object);

            // Act
            manager.Dispose();
            manager.Dispose();

            // Assert
            mockSceneA.Verify(m => m.Dispose(), Times.Once());
            mockSceneB.Verify(m => m.Dispose(), Times.Once());
        }
        #endregion

        /// <summary>
        /// Creates a new scene manager for testing.
        /// </summary>
        /// <returns>New scene manager.</returns>
        private SceneManager CreateSceneManager()
        {
            return new SceneManager(this.mockCtrlFactory.Object);
        }
    }
}
