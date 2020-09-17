using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEngineTester
{
    public class Font : IFont
    {
        private string text = string.Empty;

        public Font(SpriteFont font)
        {
            if (font is null)
            {
                throw new ArgumentNullException(nameof(font), "The parameter must not be null.");
            }

            InternalFont = font;
        }

        [ExcludeFromCodeCoverage]
        public SpriteFont InternalFont { get; }

        [ExcludeFromCodeCoverage]
        public string Text
        {
            get => this.text;
            set => this.text = string.IsNullOrEmpty(value) ? string.Empty : value;
        }

        [ExcludeFromCodeCoverage]
        public int Width => (int)InternalFont.MeasureString(Text).X;

        [ExcludeFromCodeCoverage]
        public int Height => (int)InternalFont.MeasureString(Text).Y;
    }
}
