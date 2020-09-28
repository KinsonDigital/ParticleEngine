// <copyright file="MenuItemClickedEventArgs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;

    /// <summary>
    /// Contains event data from clicking an individual menu item.
    /// </summary>
    public class MenuItemClickedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuItemClickedEventArgs"/> class.
        /// </summary>
        /// <param name="itemName">The name of the menu item that was clicked.</param>
        public MenuItemClickedEventArgs(string itemName) => MenuItemName = itemName;

        /// <summary>
        /// Gets the name of the menu item that was clicked.
        /// </summary>
        public string MenuItemName { get; private set; }
    }
}
