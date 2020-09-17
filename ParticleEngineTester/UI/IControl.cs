using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ParticleEngineTester.UI
{
    public interface IControl : IUpdateable, IDrawable, IDisposable
    {
        event EventHandler<EventArgs>? Click;

        Vector2 Location { get; set; }

        int Width { get; }

        int Height { get; }

        int Left { get; set; }

        int Right { get; set; }

        int Top { get; set; }

        int Bottom { get; set; }

        void OnClick();
    }
}
