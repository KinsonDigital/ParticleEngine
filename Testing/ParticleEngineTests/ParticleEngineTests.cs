// <copyright file="ParticleEngineTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests
{
    using System;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using KDParticleEngineTests.XUnitHelpers;
    using Moq;
    using Xunit;

    /// <summary>
    /// Holds tests for the <see cref="ParticleEngine"/> class.
    /// </summary>
    public class ParticleEngineTests : IDisposable
    {
        #region Private Fields
        private Mock<IRandomizerService> _mockRandomizerService;
        private ParticleEngine _engine;
        private readonly Mock<ITextureLoader<IParticleTexture>> _mockTextureLoader;
        private readonly Mock<IBehaviorFactory> _mockBehaviorFactory;
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticleEngineTests"/>.
        /// </summary>
        public ParticleEngineTests()
        {
            this._mockRandomizerService = new Mock<IRandomizerService>();
            this._mockTextureLoader = new Mock<ITextureLoader<IParticleTexture>>();
            this._mockBehaviorFactory = new Mock<IBehaviorFactory>();

            this._engine = new ParticleEngine(this._mockTextureLoader.Object, this._mockRandomizerService.Object);
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void Enabled_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            this._engine.Enabled = false;

            // Assert
            Assert.False(this._engine.Enabled);
        }

        [Fact]
        public void ParticlePools_WhenGettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);
            this._engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            this._engine.LoadTextures();
            this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Act
            var actual = this._engine.ParticlePools;

            // Assert
            Assert.Single(actual);
            Assert.Equal(effect, this._engine.ParticlePools[0].Effect);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void ClearPools_WhenInvoked_DisposesOfManagedResources()
        {
            // Arrange
            var mockPool1Texture = new Mock<IParticleTexture>();
            var mockPool2Texture = new Mock<IParticleTexture>();
            var textureALoaded = false;

            this._mockTextureLoader.Setup(m => m.LoadTexture(It.IsAny<string>())).Returns<string>((textureName) =>
            {
                // Load the correct texture depending on the pool.
                // All pools use the same istance of texture loader so we have
                // to mock out the correct texture to go with the correct pool,
                // so we can verify that each pool is disposing of there textures
                if (textureALoaded)
                {
                    return mockPool2Texture.Object;
                }
                else
                {
                    textureALoaded = true;
                    return mockPool1Texture.Object;
                }
            });

            var effect = new ParticleEffect();
            var engine = new ParticleEngine(this._mockTextureLoader.Object, this._mockRandomizerService.Object);

            // Create 2 pools
            engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            engine.LoadTextures();

            // Act
            engine.ClearPools();

            // Assert
            mockPool1Texture.Verify(m => m.Dispose(), Times.Once());
            mockPool2Texture.Verify(m => m.Dispose(), Times.Once());
            Assert.Empty(engine.ParticlePools);
        }

        [Fact]
        public void LoadTextures_WhenInvoked_LoadsParticlePoolTextures()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
            };
            var effect = new ParticleEffect("texture-name", settings);
            this._engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            this._engine.LoadTextures();
            this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Act
            var actual = this._engine.ParticlePools;

            // Assert
            this._mockTextureLoader.Verify(m => m.LoadTexture("texture-name"), Times.Once());
        }

        [Fact]
        public void Update_WithTexturesNotLoaded_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<Exception>(() =>
            {
                this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));
            }, "The textures must be loaded first.");
        }

        [Fact]
        public void Update_WhenDisabled_DoesNotUpdateParticles()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);
            var mockBehavior = new Mock<IBehavior>();

            this._mockBehaviorFactory.Setup(m => m.CreateBehaviors(settings, this._mockRandomizerService.Object))
                .Returns(new IBehavior[] { mockBehavior.Object });
            this._engine.Enabled = false;
            this._engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            this._engine.LoadTextures();

            // Act
            this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Never());
        }

        [Fact]
        public void Update_WhenEnabled_UpdatesAllParticles()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings)
            {
                TotalParticlesAliveAtOnce = 2
            };
            var mockBehavior = new Mock<IBehavior>();
            mockBehavior.SetupGet(p => p.Enabled).Returns(true);
            mockBehavior.SetupGet(p => p.Value).Returns("0");

            this._mockRandomizerService.Setup(m => m.GetValue(It.IsAny<int>(), It.IsAny<int>())).Returns(16);
            this._mockBehaviorFactory.Setup(m => m.CreateBehaviors(settings, this._mockRandomizerService.Object))
                .Returns(new IBehavior[] { mockBehavior.Object });

            this._engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            this._engine.LoadTextures();

            // Act
            this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));
            this._engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Exactly(3));
        }

        [Fact]
        public void Dispose_WhenInvoked_DisposesOfManagedResources()
        {
            // Arrange
            var mockPool1Texture = new Mock<IParticleTexture>();
            var mockPool2Texture = new Mock<IParticleTexture>();
            var textureALoaded = false;

            this._mockTextureLoader.Setup(m => m.LoadTexture(It.IsAny<string>())).Returns<string>((textureName) =>
            {
                // Load the correct texture depending on the pool.
                // All pools use the same istance of texture loader so we have
                // to mock out the correct texture to go with the correct pool,
                // so we can verify that each pool is disposing of there textures
                if (textureALoaded)
                {
                    return mockPool2Texture.Object;
                }
                else
                {
                    textureALoaded = true;
                    return mockPool1Texture.Object;
                }
            });

            var effect = new ParticleEffect();
            var engine = new ParticleEngine(this._mockTextureLoader.Object, this._mockRandomizerService.Object);

            // Create 2 pools
            engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            engine.CreatePool(effect, this._mockBehaviorFactory.Object);
            engine.LoadTextures();

            // Act
            engine.Dispose();
            engine.Dispose();

            // Assert
            mockPool1Texture.Verify(m => m.Dispose(), Times.Once());
            mockPool2Texture.Verify(m => m.Dispose(), Times.Once());
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Asserts if an action does not throw a null reference exception.
        /// </summary>
        /// <param name="action">The action to catch the exception against.</param>
        private void DoesNotThrowNullReference(Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(NullReferenceException))
                {
                    Assert.True(false, $"Expected not to raise a {nameof(NullReferenceException)} exception.");
                }
                else
                {
                    Assert.True(true);
                }
            }
        }

        /// <summary>
        ///  Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this._mockRandomizerService = null;
            this._engine = null;
        }
        #endregion
    }
}
