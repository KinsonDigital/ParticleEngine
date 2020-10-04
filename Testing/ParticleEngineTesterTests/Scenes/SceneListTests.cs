// <copyright file="SceneListTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Scenes
{
    using ParticleEngineTester.Scenes;
    using Xunit;

    public class SceneListTests
    {
        #region Prop Tests
        [Fact]
        public void SceneKeys_WhenGettingValue_ReturnsCorrectResult()
        {
            // Act
            var actual = SceneList.SceneKeys;

            // Assert
            Assert.Equal(12, actual.Length);
            Assert.Equal("angular-velocity-scene", actual[0]);
            Assert.Equal("x-velocity-scene", actual[1]);
            Assert.Equal("y-velocity-scene", actual[2]);
            Assert.Equal("size-scene", actual[3]);
            Assert.Equal("red-color-scene", actual[4]);
            Assert.Equal("green-color-scene", actual[5]);
            Assert.Equal("blue-color-scene", actual[6]);
            Assert.Equal("alpha-color-scene", actual[7]);
            Assert.Equal("color-transition-scene", actual[8]);
            Assert.Equal("bursting-effect-scene", actual[9]);
            Assert.Equal("spawn-location-scene", actual[10]);
            Assert.Equal("random-choice-scene", actual[11]);
        }
        #endregion
    }
}
