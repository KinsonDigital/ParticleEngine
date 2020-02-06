using System;
using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using KDParticleEngineTests.XUnitHelpers;
using Moq;
using Xunit;

namespace KDParticleEngineTests
{
    /// <summary>
    /// Unit tests to test the <see cref="KDParticleEngine.ParticleEngine{Texture}"/> class.
    /// </summary>
    public class ParticleEngineTests : IDisposable
    {
        #region Private Fields
        private Mock<IRandomizerService> _mockRandomizerService;
        private ParticleEngine<IFakeTexture> _engine;
        private readonly Mock<ITextureLoader<IFakeTexture>> _mockTextureLoader;
        private readonly Mock<IBehaviorFactory> _mockBehaviorFactory;
        #endregion


        #region Constructors
        public ParticleEngineTests()
        {
            _mockRandomizerService = new Mock<IRandomizerService>();
            _mockTextureLoader = new Mock<ITextureLoader<IFakeTexture>>();
            _mockBehaviorFactory = new Mock<IBehaviorFactory>();

            _engine = new ParticleEngine<IFakeTexture>(_mockTextureLoader.Object, _mockRandomizerService.Object);
        }
        #endregion


        #region Prop Tests
        [Fact]
        public void Enabled_WhenSettingValue_ReturnsCorrectValue()
        {
            //Arrange
            _engine.Enabled = false;

            //Assert
            Assert.False(_engine.Enabled);
        }


        [Fact]
        public void IsReadOnly_WhenGettingValue_ReturnsCorrectValue()
        {
            //Assert
            Assert.False(_engine.IsReadOnly);
        }


        [Fact]
        public void IsFixedSize_WhenGettingValue_ReturnsCorrectValue()
        {
            //Assert
            Assert.False(_engine.IsFixedSize);
        }


        [Fact]
        public void IsSyncrhonized_WhenGettingValue_ReturnsCorrectValue()
        {
            //Assert
            Assert.False(_engine.IsSynchronized);
        }


        [Fact]
        public void ParticlePools_WhenGettingValue_ReturnsCorrectValue()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);
            _engine.CreatePool(effect, _mockBehaviorFactory.Object);
            _engine.LoadTextures();
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Act
            var actual = _engine.ParticlePools;

            //Assert
            Assert.Single(actual);
            Assert.Equal(effect, _engine.ParticlePools[0].Effect);
        }
        #endregion


        #region Method Tests
        [Fact]
        public void LoadTextures_WhenInvoked_LoadsParticlePoolTextures()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect("texture-name", settings);
            _engine.CreatePool(effect, _mockBehaviorFactory.Object);
            _engine.LoadTextures();
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Act
            var actual = _engine.ParticlePools;

            //Assert
            _mockTextureLoader.Verify(m => m.LoadTexture("texture-name"), Times.Once());
        }


        [Fact]
        public void Update_WithTexturesNotLoaded_ThrowsException()
        {
            //Act & Assert
            AssertHelpers.ExceptionHasMessage(() =>
            {
                _engine.Update(new TimeSpan(0, 0, 0, 0, 16));
            }, "The textures must be loaded first.");
        }


        [Fact]
        public void Update_WhenDisabled_DoesNotUpdateParticles()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);
            var mockBehavior = new Mock<IBehavior>();
            
            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(settings, _mockRandomizerService.Object))
                .Returns(new IBehavior[] { mockBehavior.Object });
            _engine.Enabled = false;
            _engine.CreatePool(effect, _mockBehaviorFactory.Object);
            _engine.LoadTextures();

            //Act
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Never());
        }


        [Fact]
        public void Update_WhenEnabled_UpdatesAllParticles()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings)
            {
                TotalParticlesAliveAtOnce = 2
            };
            var mockBehavior = new Mock<IBehavior>();
            mockBehavior.SetupGet(p => p.Enabled).Returns(true);

            _mockRandomizerService.Setup(m => m.GetValue(It.IsAny<int>(), It.IsAny<int>())).Returns(16);
            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(settings, _mockRandomizerService.Object))
                .Returns(new IBehavior[] { mockBehavior.Object });

            _engine.CreatePool(effect, _mockBehaviorFactory.Object);
            _engine.LoadTextures();

            //Act
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            mockBehavior.Verify(m => m.Update(It.IsAny<TimeSpan>()), Times.Exactly(3));
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


        public void Dispose()
        {
            _mockRandomizerService = null;
            _engine = null;
        }
        #endregion
    }
}
