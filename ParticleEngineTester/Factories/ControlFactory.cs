// <copyright file="ControlFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Factories
{
    using System;
    using ParticleEngineTester.UI;

    /// <summary>
    /// Creates various controls.
    /// </summary>
    public class ControlFactory : IControlFactory
    {
        private static IRenderer? renderer;
        private static IContentLoader? contentLoader;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlFactory"/> class.
        /// </summary>
        /// <param name="renderer">Used to render created controls.</param>
        /// <param name="contentLoader">Used to load content for controls.</param>
        public ControlFactory(IRenderer renderer, IContentLoader contentLoader)
        {
            ControlFactory.renderer = renderer;
            ControlFactory.contentLoader = contentLoader;
        }

        /// <summary>
        /// Creates a label with the given <paramref name="name"/> and <paramref name="text"/>.
        /// </summary>
        /// <param name="name">The name to give the <see cref="ILabel"/>.</param>
        /// <param name="text">The text to give the <see cref="ILabel"/>.</param>
        /// <returns>A label object.</returns>
        public ILabel CreateLabel(string name, string text = "")
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
            }

            if (contentLoader is null)
            {
                throw new ArgumentNullException(nameof(contentLoader), "The parameter must not be null.");
            }

            var newLabel = new Label(renderer, contentLoader, new MouseInput())
            {
                Name = name,
                Text = text,
            };

            return newLabel;
        }

        /// <summary>
        /// Creates a mouse.
        /// </summary>
        /// <returns>A mouse object.</returns>
        public IMouse CreateMouse() => new MouseInput();
    }
}
