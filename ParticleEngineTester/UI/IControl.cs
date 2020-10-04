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
    public interface IControl : IDimensions, IUpdateable, IDrawable, IMouseInteraction, IDisposable
    {
        /// <summary>
        /// Gets or sets the name of the control.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="IControl"/> should be enabled.
        /// </summary>
        new bool Enabled { get; set; }
    }
}
