// <copyright file="ParticlePoolTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests
{
    using System;
    using System.Drawing;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using Moq;
    using Xunit;

    /// <summary>
    /// Holds tests for the <see cref="ParticlePool{Texture}"/> class.
    /// </summary>
    public class ParticlePoolTests
    {
        #region Private Fields
        private readonly Mock<IRandomizerService> _mockRandomizerService;
        private readonly Mock<ITextureLoader<IParticleTexture>> _mockTextureLoader;
        private readonly Mock<IBehaviorFactory> _mockBehaviorFactory;
        private readonly Mock<IBehavior> _mockBehavior;
        private readonly EasingBehaviorSettings[] _settings;
        private readonly ParticleEffect _effect;
        private const string PARTICLE_TEXTURE_NAME = "particle-texture";
        #endregion

        #region Constructors
        /// <summary>
        /// Creates a new instance of <see cref="ParticlePoolTests"/>.
        /// </summary>
        public ParticlePoolTests()
        {
            this._settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
            };
            this._effect = new ParticleEffect(PARTICLE_TEXTURE_NAME, this._settings);

            this._mockRandomizerService = new Mock<IRandomizerService>();
            this._mockTextureLoader = new Mock<ITextureLoader<IParticleTexture>>();

            this._mockBehavior = new Mock<IBehavior>();
            this._mockBehavior.SetupGet(p => p.Value).Returns("0");
            this._mockBehavior.SetupGet(p => p.Enabled).Returns(true);

            this._mockBehaviorFactory = new Mock<IBehaviorFactory>();
            this._mockBehaviorFactory.Setup(m => m.CreateBehaviors(this._settings, this._mockRandomizerService.Object))
                .Returns(() =>
                {
                    return new IBehavior[] { this._mockBehavior.Object };
                });
        }
        #endregion

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsEffectProp()
        {
            // Act
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Assert
            Assert.Equal(this._effect, pool.Effect);
        }

        [Fact]
        public void Ctor_WhenInvoked_CreatesParticles()
        {
            // Act
            this._effect.TotalParticlesAliveAtOnce = 10;

            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Assert
            Assert.Equal(10, pool.Particles.Length);
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void TotalLivingParticles_WhenGettingValue_ReturnsCorrectValue()
        { 
            // Arrange
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));
            var actual = pool.TotalLivingParticles;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void TotalDeadParticles_WhenGettingValue_ReturnsCorrectValue()
        {
            // Arrange
            this._effect.TotalParticlesAliveAtOnce = 10;
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));
            var actual = pool.TotalDeadParticles;

            // Assert
            Assert.Equal(9, actual);
        }
        #endregion

        #region Method Tests
        [Theory]
        [InlineData(10, 20)]
        [InlineData(20, 10)]
        public void Update_WhenGeneratingRandomSpawnRate_CorrectlyUsesMinAndMax(int rateMin, int rateMax)
        {
            // Arrange
            this._effect.SpawnRateMin = rateMin;
            this._effect.SpawnRateMax = rateMax;
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, It.IsAny<ITextureLoader<IParticleTexture>>(), this._effect, this._mockRandomizerService.Object);

            // Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            this._mockRandomizerService.Verify(m => m.GetValue(rateMin < rateMax ? rateMin : rateMax, rateMax > rateMin ? rateMax : rateMin), Times.Once());
        }

        [Fact]
        public void Update_WhenInvoked_SpawnsNewParticle()
        {
            // Arrange
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, It.IsAny<ITextureLoader<IParticleTexture>>(), this._effect, this._mockRandomizerService.Object);

            // Act
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Assert
            Assert.Equal(1, pool.TotalLivingParticles);
        }

        [Fact]
        public void KillAllParticles_WhenInvoked_KillsAllParticles()
        {
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, It.IsAny<ITextureLoader<IParticleTexture>>(), this._effect, this._mockRandomizerService.Object);
            pool.Update(new TimeSpan(0, 0, 0, 0, 16));

            // Act
            pool.KillAllParticles();

            // Assert
            Assert.Equal(0, pool.TotalLivingParticles);
        }

        [Fact]
        public void LoadTexture_WhenInvoked_LoadsTextureWithEffectTextureName()
        {
            // Arrange
            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Act
            pool.LoadTexture();

            // Assert
            this._mockTextureLoader.Verify(m => m.LoadTexture(PARTICLE_TEXTURE_NAME), Times.Once());
        }

        [Fact]
        public void Equals_WithDifferentObjectTypes_ReturnsFalse()
        {
            // Arrange
            this._effect.ApplyBehaviorTo = ParticleAttribute.Angle;
            this._effect.SpawnLocation = new PointF(11, 22);
            this._effect.SpawnRateMin = 33;
            this._effect.SpawnRateMax = 44;
            this._effect.TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) };
            this._effect.TotalParticlesAliveAtOnce = 99;
            this._effect.UseColorsFromList = true;

            var poolA = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);
            var otherObj = new object();

            // Act
            var actual = poolA.Equals(otherObj);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Equals_WithEqualObjects_ReturnsTrue()
        {
            // Arrange
            this._effect.ApplyBehaviorTo = ParticleAttribute.Angle;
            this._effect.SpawnLocation = new PointF(11, 22);
            this._effect.SpawnRateMin = 33;
            this._effect.SpawnRateMax = 44;
            this._effect.TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) };
            this._effect.TotalParticlesAliveAtOnce = 99;
            this._effect.UseColorsFromList = true;

            var poolA = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);
            var poolB = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);

            // Act
            var actual = poolA.Equals(poolB);

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Equals_WithNonEqualObjects_ReturnsFalse()
        {
            // Arrange
            var effectA = new ParticleEffect("texture-name", this._settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 99,
                UseColorsFromList = true
            };

            var effectB = new ParticleEffect("texture-name", this._settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 33,
                SpawnRateMax = 44,
                TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) },
                TotalParticlesAliveAtOnce = 100,
                UseColorsFromList = true
            };

            var poolA = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, effectA, this._mockRandomizerService.Object);
            var poolB = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, effectB, this._mockRandomizerService.Object);

            // Act
            var actual = poolA.Equals(poolB);

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void GetHashCode_WhenInvoked_ReturnsCorrectValue()
        {
            // Arrange
            this._effect.ApplyBehaviorTo = ParticleAttribute.Angle;
            this._effect.SpawnLocation = new PointF(11, 22);
            this._effect.SpawnRateMin = 33;
            this._effect.SpawnRateMax = 44;
            this._effect.TintColors = new ParticleColor[] { new ParticleColor(55, 66, 77, 88) };
            this._effect.TotalParticlesAliveAtOnce = 99;
            this._effect.UseColorsFromList = true;

            var poolA = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);
            var poolB = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object, this._mockTextureLoader.Object, this._effect, this._mockRandomizerService.Object);
            
            poolA.LoadTexture();
            poolB.LoadTexture();

            // Act
            var actual = poolA.GetHashCode() == poolB.GetHashCode();

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Dispose_WhenInvoked_ProperlyFreesManagedResources()
        {
            // Arrange
            var _effect = new ParticleEffect();
            var mockTexture = new Mock<IParticleTexture>();

            this._mockTextureLoader.Setup(m => m.LoadTexture(It.IsAny<string>())).Returns<string>((textureName) => 
            {
                return mockTexture.Object;
            });

            var pool = new ParticlePool<IParticleTexture>(this._mockBehaviorFactory.Object,
                this._mockTextureLoader.Object,
                _effect,
                this._mockRandomizerService.Object);

            pool.LoadTexture();

            // Call this twice to verify that the disposable pattern is implemented correctly.
            // You should be able to call this method twice and not throw an exception
            // Act
            pool.Dispose();
            pool.Dispose();

            // Assert
            mockTexture.Verify(m => m.Dispose(), Times.Once());
        }
        #endregion
    }
}
