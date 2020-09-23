// <copyright file="ClickedEventArgs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;

    /// <summary>
    /// Gets event data from clicking a control.
    /// </summary>
    public class ClickedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClickedEventArgs"/> class.
        /// </summary>
        /// <param name="name">The name of the control that was clicked.</param>
        public ClickedEventArgs(string name) => Name = name;

        /// <summary>
        /// Gets the name of the control that was clicked.
        /// </summary>
        public string Name { get; set; } = string.Empty;
    }
}
