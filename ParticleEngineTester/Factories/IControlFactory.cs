// <copyright file="IControlFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Factories
{
    using ParticleEngineTester.UI;

    /// <summary>
    /// Creates various controls.
    /// </summary>
    public interface IControlFactory
    {
        /// <summary>
        /// Creates a label with the given <paramref name="name"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="name">The name to give the <see cref="ILabel"/>.</param>
        /// <param name="text">The text to give the <see cref="ILabel"/>.</param>
        /// <returns>A label object.</returns>
        ILabel CreateLabel(string name, string text);

        /// <summary>
        /// Creates a button with the given <paramref name="name"/> and <paramref name="buttonContent"/>.
        /// </summary>
        /// <param name="name">The name of the button control.</param>
        /// <param name="buttonContent">The graphical content of the button control.</param>
        /// <returns>A button with a name and graphic that defines its looks.</returns>
        IButton CreateButton(string name, string buttonContent);

        /// <summary>
        /// Creates a mouse.
        /// </summary>
        /// <returns>A mouse object.</returns>
        IMouse CreateMouse();
    }
}
