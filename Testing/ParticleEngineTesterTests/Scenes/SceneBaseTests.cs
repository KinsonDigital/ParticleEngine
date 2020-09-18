// <copyright file="SceneBaseTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Scenes
{
    using System;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTesterTests.Fakes;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="SceneBase"/> class.
    /// </summary>
    public class SceneBaseTests
    {
        #region Constructor Tests
        [Fact]
        public void Ctor_WithNullRenderer_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var scene = new FakeSceneBase(null, new Mock<IContentLoader>().Object, null);
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void Ctor_WithNullContentLoader_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var scene = new FakeSceneBase(new Mock<IRenderer>().Object, null, null);
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void Ctor_WithNullName_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var scene = new FakeSceneBase(new Mock<IRenderer>().Object, new Mock<IContentLoader>().Object, null);
            }, "The parameter must not be null. (Parameter 'name')");
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultNameProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            scene.Name = "other-test-name";
            var actual = scene.Name;

            // Assert
            Assert.Equal("other-test-name", actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultEnabledProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            var actual = scene.Enabled;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultUpdateOrderProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            var actual = scene.UpdateOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultDrawOrderProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            var actual = scene.DrawOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultVisibleProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            var actual = scene.Visible;

            // Assert
            Assert.True(actual);
        }
        #endregion

        private FakeSceneBase CreateScene(string name = "test-name")
        {
            return new FakeSceneBase(new Mock<IRenderer>().Object, new Mock<IContentLoader>().Object, name);
        }
    }
}
