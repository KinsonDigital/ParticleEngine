using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ParticleEngineTester
{
    public interface IFont
    {
        SpriteFont InternalFont { get; }

        string Text { get; set; }

        int Width { get; }

        int Height { get; }
    }
}
