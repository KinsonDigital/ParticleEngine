using System;
using System.Drawing;
using System.Linq;
using KDParticleEngine;
using KDParticleEngine.Services;
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


        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_ProperlySetsParticleList()
        {
            //Assert
            Assert.NotNull(_engine.Particles);
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
