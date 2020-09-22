// <copyright file="ILabel.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A piece of text that can be rendered to the screen.
    /// </summary>
    public interface ILabel : IControl
    {
        /// <summary>
        /// Gets or sets the text of the <see cref="ILabel"/>.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the text is bold.
        /// </summary>
        bool IsBold { get; set; }

        /// <summary>
        /// Gets or sets the forecolor of the label text.
        /// </summary>
        Color Forecolor { get; set; }
    }
}
