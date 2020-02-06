﻿using KDParticleEngine;
using KDParticleEngine.Behaviors;
using KDParticleEngine.Services;
using KDParticleEngineTests.Fakes;
using Moq;
using System;
using System.Drawing;
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
                TotalParticlesAliveAtOnce = 2
            };

            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(It.IsAny<BehaviorSetting[]>(), It.IsAny<IRandomizerService>()))
                .Returns(() =>
                {
                    return new IBehavior[] { new FakeBehavior(settings[0], _mockRandomizerService.Object) };
                });
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));
            var actual = pool.TotalLivingParticles;

            //Assert
            Assert.Equal(1, actual);
        }


        [Fact]
        public void TotalDeadParticles_WhenGettingValue_ReturnsCorrectValue()
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

            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(It.IsAny<BehaviorSetting[]>(), It.IsAny<IRandomizerService>()))
                .Returns(() =>
                {
                    return new IBehavior[] { new FakeBehavior(settings[0], _mockRandomizerService.Object) };
                });
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));
            var actual = pool.TotalDeadParticles;

            //Assert
            Assert.Equal(9, actual);
        }
        #endregion


        #region Method Tests
        [Theory]
        [InlineData(10, 20)]
        [InlineData(20, 10)]
        public void Update_WhenGeneratingRandomSpawnRate_CorrectlyUsesMinAndMax(int rateMin, int rateMax)
        {
            //Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<BehaviorSetting[]>())
            {
                SpawnRateMin = rateMin,
                SpawnRateMax = rateMax
            };

            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, It.IsAny<ITextureLoader<object>>(), effect, _mockRandomizerService.Object);

            //Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            _mockRandomizerService.Verify(m => m.GetValue(rateMin < rateMax ? rateMin : rateMax, rateMax > rateMin ? rateMax : rateMin), Times.Once());
        }


        [Fact]
        public void Update_WhenInvoked_SpawnsNewParticle()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };

            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<BehaviorSetting[]>())
            {
                TotalParticlesAliveAtOnce = 2
            };

            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(It.IsAny<BehaviorSetting[]>(), It.IsAny<IRandomizerService>()))
                .Returns(() =>
                {
                    return new IBehavior[] { new FakeBehavior(settings[0], _mockRandomizerService.Object) };
                });
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, It.IsAny<ITextureLoader<object>>(), effect, _mockRandomizerService.Object);

            //Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Assert
            Assert.Equal(1, pool.TotalLivingParticles);
        }


        [Fact]
        public void KillAllParticles_WhenInvoked_KillsAllParticles()
        {
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };

            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<BehaviorSetting[]>())
            {
                TotalParticlesAliveAtOnce = 2
            };

            _mockBehaviorFactory.Setup(m => m.CreateBehaviors(It.IsAny<BehaviorSetting[]>(), It.IsAny<IRandomizerService>()))
                .Returns(() =>
                {
                    return new IBehavior[] { new FakeBehavior(settings[0], _mockRandomizerService.Object) };
                });
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, It.IsAny<ITextureLoader<object>>(), effect, _mockRandomizerService.Object);
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            //Act
            pool.KillAllParticles();

            //Assert
            Assert.Equal(0, pool.TotalLivingParticles);
        }


        [Fact]
        public void LoadTexture_WhenInvoked_LoadsTextureWithEffectTextureName()
        {
            //Arrange
            var effect = new ParticleEffect("texture-name", It.IsAny<BehaviorSetting[]>());
            var pool = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Act
            pool.LoadTexture();

            //Assert
            _mockTextureLoader.Verify(m => m.LoadTexture("texture-name"), Times.Once());
        }


        [Fact]
        public void Equals_WithDifferentObjectTypes_ReturnsFalse()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect("texture-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                TypeOfBehavior = BehaviorType.EaseIn,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 99,
                UseColorsFromList = true
            };

            var poolA = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);
            var otherObj = new object();

            //Act
            var actual = poolA.Equals(otherObj);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithEqualObjects_ReturnsTrue()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect("texture-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                TypeOfBehavior = BehaviorType.EaseIn,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 99,
                UseColorsFromList = true
            };

            var poolA = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);
            var poolB = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);

            //Act
            var actual = poolA.Equals(poolB);

            //Assert
            Assert.True(actual);
        }


        [Fact]
        public void Equals_WithNonEqualObjects_ReturnsFalse()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effectA = new ParticleEffect("texture-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                TypeOfBehavior = BehaviorType.EaseIn,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 99,
                UseColorsFromList = true
            };

            var effectB = new ParticleEffect("texture-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                TypeOfBehavior = BehaviorType.EaseIn,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 100,
                UseColorsFromList = true
            };

            var poolA = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effectA, _mockRandomizerService.Object);
            var poolB = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effectB, _mockRandomizerService.Object);

            //Act
            var actual = poolA.Equals(poolB);

            //Assert
            Assert.False(actual);
        }


        [Fact]
        public void GetHashCode_WhenInvoked_ReturnsCorrectValue()
        {
            //Arrange
            var settings = new BehaviorSetting[]
            {
                new BehaviorSetting()
            };
            var effect = new ParticleEffect("texture-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                TypeOfBehavior = BehaviorType.EaseIn,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 99,
                UseColorsFromList = true
            };

            _mockTextureLoader.Setup(m => m.LoadTexture(It.IsAny<string>())).Returns(new object());
            var poolA = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);
            var poolB = new ParticlePool<object>(_mockBehaviorFactory.Object, _mockTextureLoader.Object, effect, _mockRandomizerService.Object);
            
            poolA.LoadTexture();
            poolB.LoadTexture();

            //Act

            var hashA = poolA.GetHashCode();
            var hashB = poolB.GetHashCode();

            var actual = poolA.GetHashCode() == poolB.GetHashCode();

            //Assert
            Assert.True(actual);
        }
        #endregion
    }
}
