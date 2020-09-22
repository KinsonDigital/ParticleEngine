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
        /// Creates a mouse.
        /// </summary>
        /// <returns>A mouse object.</returns>
        IMouse CreateMouse();
    }
}
