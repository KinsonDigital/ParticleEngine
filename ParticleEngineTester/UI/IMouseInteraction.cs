// <copyright file="IMouseInteraction.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;

    /// <summary>
    /// Mouse related interactions that an object can have.
    /// </summary>
    public interface IMouseInteraction
    {
        /// <summary>
        /// Invoked when the mouse clicks the <see cref="Control"/>.
        /// </summary>
        event EventHandler<ClickedEventArgs>? Click;

        /// <summary>
        /// Invoked when the mouse enters on top of the <see cref="Control"/>.
        /// </summary>
        event EventHandler<EventArgs>? MouseEnter;

        /// <summary>
        /// Invoked when the mouse leaves the <see cref="Control"/>.
        /// </summary>
        event EventHandler<EventArgs>? MouseLeave;

        /// <summary>
        /// Gets a value indicating whether the mouse is or is not over the object.
        /// </summary>
        bool IsMouseOver { get; }

        /// <summary>
        /// Invokes the <see cref="Click"/> event.
        /// </summary>
        /// <param name="sender">The control object that invoked the event.</param>
        /// <param name="e">The event related data of the event.</param>
        void OnClick(object? sender, ClickedEventArgs e);
    }
}
