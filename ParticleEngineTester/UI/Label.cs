// <copyright file="Label.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A piece of text that can be rendered to the screen.
    /// </summary>
    public class Label : Control, ILabel
    {
        private readonly IRenderer renderer;
        private readonly IFont standardFont;
        private readonly IFont boldFont;
        private string text = string.Empty;

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
            IMouse mouse)
            : base(mouse)
        {
            this.renderer = renderer;
            this.standardFont = contentLoader.LoadFont("Fonts/standard-font");
            this.boldFont = contentLoader.LoadFont("Fonts/bold-font");

            GetWidth = () =>
            {
                return IsBold ? this.boldFont.Width : this.standardFont.Width;
            };

            GetHeight = () =>
            {
                return IsBold ? this.boldFont.Height : this.standardFont.Height;
            };
        }

        /// <inheritdoc/>
        public string Text
        {
            get => this.text;
            set
            {
                this.text = value;
                this.standardFont.Text = value;
                this.boldFont.Text = value;
            }
        }

        /// <inheritdoc/>
        public bool IsBold { get; set; }

        /// <inheritdoc/>
        public Color Forecolor { get; set; } = Color.Black;

        /// <inheritdoc/>
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            this.renderer.DrawText(IsBold ? this.boldFont : this.standardFont, this.standardFont.Text, Location, Forecolor);
            base.Draw(gameTime);
        }
    }
}
