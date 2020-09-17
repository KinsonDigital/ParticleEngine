using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using ParticleEngineTester;
using ParticleEngineTesterTests.Fakes;
using ParticleEngineTesterTests.Helpers;
using Xunit;

namespace ParticleEngineTesterTests.Scenes
{
    public class SceneBaseTests
    {
        #region Constructor Tests
        [Fact]
        public void Ctor_WithNullRenderer_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var scene = new FakeSceneBase(null, new Mock<IContentLoader>().Object);
            }, "The parameter must not be null. (Parameter 'renderer')");
        }

        [Fact]
        public void Ctor_WithNullContentLoader_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var scene = new FakeSceneBase(new Mock<IRenderer>().Object, null);
            }, "The parameter must not be null. (Parameter 'contentLoader')");
        }

        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsDefaultNameProp()
        {
            // Arrange
            var scene = CreateScene();

            // Act
            var actual = scene.Name;

            // Assert
            Assert.Equal(string.Empty, actual);
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

        private FakeSceneBase CreateScene()
        {
            return new FakeSceneBase(new Mock<IRenderer>().Object, new Mock<IContentLoader>().Object);
        }
    }
}
