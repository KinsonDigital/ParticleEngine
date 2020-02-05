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
        #endregion


        #region Constructors
        public ParticleEngineTests()
        {
            _mockRandomizerService = new Mock<IRandomizerService>();
            _mockTextureLoader = new Mock<ITextureLoader<IFakeTexture>>();

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
        #endregion


        #region Method Tests
        [Fact]
        public void CreatePool_WhenInvoked_CreatesParticlePool()
        {
            ////Arrange
            //var settings = new BehaviorSetting[]
            //{
            //    new BehaviorSetting()
            //};
            //var effect = new ParticleEffect(It.IsAny<string>(), settings);
            //var expected = new ParticlePool[]
            //{
            //    new ParticlePool<object>(effect, _mockRandomizerService.Object)
            //};

            ////Act
            //_engine.CreatePool(effect);
            //var actual = _engine.ParticlePools;

            ////Assert
            //Assert.Single(actual);
            ////TODO: Setup equals comparison for ParticlePool
            //Assert.Equal(expected, actual);
        }


        [Fact]
        public void Equals_WhenBothEqual_ReturnsTrue()
        {
            //Arrange
            //var poolA = new ParticlePool()

            //Act

            //Assert
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


        //[Fact]
        public void Update_WhenDisabled_DoesNotUpdateParticles()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);
            _engine.CreatePool(effect);
            _engine.LoadTextures();

            //Act
            _engine.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            
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
