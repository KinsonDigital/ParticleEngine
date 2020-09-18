// <copyright file="FontTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using System;
    using ParticleEngineTester;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    public class FontTests
    {
        #region Constructor Tests
        [Fact]
        public void Ctor_WithNullSpriteFont_ThrowsException()
        {
            // Act & Assert
            AssertHelpers.ThrowsWithMessage<ArgumentNullException>(() =>
            {
                var font = new Font(null);
            }, "The parameter must not be null. (Parameter 'font')");
        }
        #endregion
    }
}
