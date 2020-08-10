// <copyright file="ParticleEffectTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace KDParticleEngineTests
{
    using System.Drawing;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Moq;
    using Xunit;

    /// <summary>
    /// Holds tests for the <see cref="ParticleEffect"/> class.
    /// </summary>
    public class ParticleEffectTests
    {
        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsParticleTextureName()
        {
            // Act
            var effect = new ParticleEffect("effect-name", It.IsAny<EasingBehaviorSettings[]>());

            // Assert
            Assert.Equal("effect-name", effect.ParticleTextureName);
        }


        [Fact]
        public void Ctor_WhenInvoked_SetsBehaviorSettings()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };
            var effect = new ParticleEffect(It.IsAny<string>(), settings);

            // Act
            var actual = effect.BehaviorSettings;

            // Assert
            Assert.Equal(settings[0], actual[0]);
        }
        #endregion


        #region Prop Tests
        [Fact]
        public void ApplyBehaviorTo_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.ApplyBehaviorTo = ParticleAttribute.Angle;
            var actual = effect.ApplyBehaviorTo;

            // Assert
            Assert.Equal(ParticleAttribute.Angle, actual);
        }


        [Fact]
        public void SpawnLocation_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.SpawnLocation = new PointF(11, 22);
            var actual = effect.SpawnLocation;

            // Assert
            Assert.Equal(new PointF(11, 22), actual);
        }


        [Fact]
        public void TintColors_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) };
            var actual = effect.TintColors;

            // Assert
            Assert.Equal(new ParticleColor(11, 22, 33, 44), actual[0]);
        }


        [Fact]
        public void TotalParticlesAliveAtOnce_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.TotalParticlesAliveAtOnce = 1234;
            var actual = effect.TotalParticlesAliveAtOnce;

            // Assert
            Assert.Equal(1234, actual);
        }


        [Fact]
        public void SpawnRateMin_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.SpawnRateMin = 1234;
            var actual = effect.SpawnRateMin;

            // Assert
            Assert.Equal(1234, actual);
        }


        [Fact]
        public void SpawnRateMax_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.SpawnRateMax = 1234;
            var actual = effect.SpawnRateMax;

            // Assert
            Assert.Equal(1234, actual);
        }


        [Fact]
        public void UseColorsFromList_WhenSettingValue_ReturnsCorrectValue()
        {
            // Arrange
            var effect = new ParticleEffect(It.IsAny<string>(), It.IsAny<EasingBehaviorSettings[]>());

            // Act
            effect.UseColorsFromList = true;
            var actual = effect.UseColorsFromList;

            // Assert
            Assert.True(actual);
        }
        #endregion


        #region Method Tests
        [Fact]
        public void Equals_WithDifferentObjects_ReturnsFalse()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };

            var effect = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };
            var otherObj = new object();

            // Act
            var actual = effect.Equals(otherObj);

            // Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithEqualObjects_ReturnsTrue()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };

            var effectA = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            var effectB = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            // Act
            var actual = effectA.Equals(effectB);

            // Assert
            Assert.True(actual);
        }


        [Fact]
        public void Equals_WithNonEqualObjects_ReturnsFalse()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };

            var effectA = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            var effectB = new ParticleEffect("effect-bee", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.GreenColorComponent,
                SpawnLocation = new PointF(99, 88),
                SpawnRateMin = 77,
                SpawnRateMax = 66,
                TotalParticlesAliveAtOnce = 55,
                UseColorsFromList = false,
                TintColors = new ParticleColor[] { new ParticleColor(99, 88, 77, 66) }
            };

            // Act
            var actual = effectA.Equals(effectB);

            // Assert
            Assert.False(actual);
        }


        [Fact]
        public void Equals_WithDifferentTintColorTotals_ReturnsFalse()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };

            var effectA = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            var effectB = new ParticleEffect("effect-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44), new ParticleColor(11, 22, 33, 44) }
            };

            // Act
            var actual = effectA.Equals(effectB);

            // Assert
            Assert.False(actual);
        }


        [Fact]
        public void GetHashCode_WhenInvoked_ReturnsCorrectValue()
        {
            // Arrange
            var settings = new EasingBehaviorSettings[]
            {
                new EasingBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Angle,
                    TypeOfBehavior = BehaviorType.EaseIn,
                    ChangeMin = 11,
                    ChangeMax = 22,
                    StartMin = 33,
                    StartMax = 44,
                    TotalTimeMin = 55,
                    TotalTimeMax = 66
                }
            };

            var effectA = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            var effectB = new ParticleEffect("test-name", settings)
            {
                ApplyBehaviorTo = ParticleAttribute.Angle,
                SpawnLocation = new PointF(11, 22),
                SpawnRateMin = 11,
                SpawnRateMax = 22,
                TotalParticlesAliveAtOnce = 33,
                UseColorsFromList = true,
                TintColors = new ParticleColor[] { new ParticleColor(11, 22, 33, 44) }
            };

            // Act
            var actual = effectA.GetHashCode() == effectB.GetHashCode();

            // Assert
            Assert.True(actual);
        }
        #endregion
    }
}
