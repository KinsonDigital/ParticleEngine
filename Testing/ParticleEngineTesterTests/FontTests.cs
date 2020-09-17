using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ParticleEngineTester;
using ParticleEngineTesterTests.Helpers;
using Xunit;

namespace ParticleEngineTesterTests
{
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
