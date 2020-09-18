// <copyright file="Label.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A piece of text that can be rendered to the screen.
    /// </summary>
    public class Label : Control
    {
        private readonly IRenderer renderer;
        private readonly IFont font;

        /// <summary>
        /// Initializes a new instance of the <see cref="Label"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the <see cref="Label"/>.</param>
        /// <param name="contentLoader">Loads the content for the <see cref="Label"/>.</param>
        /// <param name="mouse">The mouse that will interact with the label.</param>
        /// <param name="fontContent">The font content to be loaded.</param>
        public Label(
            IRenderer renderer,
            IContentLoader contentLoader,
            IMouse mouse,
            string fontContent)
            : base(mouse)
        {
            this.renderer = renderer;
            this.font = contentLoader.LoadFont(fontContent);

            GetWidth = () =>
            {
                return this.font.Width;
            };

            GetHeight = () =>
            {
                return this.font.Height;
            };
        }

        /// <summary>
        /// Gets or sets the text of the <see cref="Label"/>.
        /// </summary>
        public string Text
        {
            get => this.font.Text;
            set => this.font.Text = value;
        }

        /// <summary>
        /// Gets or sets the forecolor of the label text.
        /// </summary>
        public Color Forecolor { get; set; } = Color.Black;

        /// <inheritdoc/>
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            this.renderer.DrawText(this.font, this.font.Text, Location, Forecolor);
            base.Draw(gameTime);
        }
    }
}
