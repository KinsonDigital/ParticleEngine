// <copyright file="IButton.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using Microsoft.Xna.Framework;

    public interface IButton : IUpdateable, IDrawable
    {
        event EventHandler<EventArgs>? Click;

        Vector2 Location { get; set; }

        int Width { get; }

        int Height { get; }

        int Left { get; set; }

        int Right { get; set; }

        int Top { get; set; }

        int Bottom { get; set; }
    }
}
