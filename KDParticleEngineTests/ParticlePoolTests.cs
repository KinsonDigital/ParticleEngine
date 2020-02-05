using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace KDParticleEngineTests
{
    public class ParticlePoolTests
    {
        #region Private Fields
        private readonly Mock<IRandomizerService> _mockRandomizerService;
        private readonly Mock<ITextureLoader<object>> _mockTextureLoader;
        private readonly Mock<IBehaviorFactory> _mockBehaviorFactory;
        #endregion


        #region Constructors
        public ParticlePoolTests()
        {
            _mockRandomizerService = new Mock<IRandomizerService>();
            _mockTextureLoader = new Mock<ITextureLoader<object>>();
            _mockBehaviorFactory = new Mock<IBehaviorFactory>();
        }
        #endregion


        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsEffectProp()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);

            //Act
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Assert
            Assert.Equal(effect, pool.Effect);
        }


        [Fact]
        public void Ctor_WhenInvoked_CreatesParticles()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings)
            {
                TotalParticlesAliveAtOnce = 10
            };

            //Act
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Assert
            Assert.Equal(10, pool.Particles.Length);
        }
        #endregion


        #region Prop Tests
        [Fact]
        public void TotalLivingParticles_WhenGettingValue_ReturnsCorrectValue()
        { 
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings)
            {
                TotalParticlesAliveAtOnce = 10
            };
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Act


            //Assert
            Assert.Equal(10, pool.Particles.Length);
        }
        #endregion
    }
}
