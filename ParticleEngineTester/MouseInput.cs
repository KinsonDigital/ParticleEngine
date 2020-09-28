// <copyright file="MouseInput.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Initializes a new instance of the <see cref="MouseInput"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class MouseInput : IMouse
    {
        /// <summary>
        /// Gets mouse state information that includes position and button presses for the primary window.
        /// </summary>
        /// <returns>Current state of the mouse.</returns>
        public MouseState GetState() => Mouse.GetState();
    }
}
