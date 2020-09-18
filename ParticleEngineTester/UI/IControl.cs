// <copyright file="IControl.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A user interface control to be rendered.
    /// </summary>
    public interface IControl : IUpdateable, IDrawable, IDisposable
    {
        /// <summary>
        /// Invoked when the mouse clicks the <see cref="IControl"/>.
        /// </summary>
        event EventHandler<EventArgs>? Click;

        /// <summary>
        /// Gets or sets the location of the control.
        /// </summary>
        Vector2 Location { get; set; }

        /// <summary>
        /// Gets the width of the control.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the height of the control.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets or sets the position of the left side of the control.
        /// </summary>
        int Left { get; set; }

        /// <summary>
        /// Gets or sets the position of the right side of the control.
        /// </summary>
        int Right { get; set; }

        /// <summary>
        /// Gets or sets the position of the top of the control.
        /// </summary>
        int Top { get; set; }

        /// <summary>
        /// Gets or sets the position of the bottom the control.
        /// </summary>
        int Bottom { get; set; }

        /// <summary>
        /// Invokes the <see cref="Click"/> event.
        /// </summary>
        void OnClick();
    }
}
