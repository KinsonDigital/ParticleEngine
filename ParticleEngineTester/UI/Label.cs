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
        private string cachedText = string.Empty;

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
            get => this.standardFont is null || this.boldFont is null ? this.cachedText : this.standardFont.Text;
            set
            {
                // If the font is null, then the text cannot be set.
                // Cache the text instead until the update method is called
                if (this.standardFont is null || this.boldFont is null)
                {
                    this.cachedText = value;
                }
                else
                {
                    this.standardFont.Text = value;
                    this.boldFont.Text = value;
                }
            }
        }

        /// <inheritdoc/>
        public bool IsBold { get; set; }

        /// <inheritdoc/>
        public Color Forecolor { get; set; } = Color.Black;

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            // If the font is not null and the cached text is not null,
            // set the font text to the cached text and then empty the cache.
            // This is to allow the text value to be saved when setting the Text
            // property in constructor initializers without throwing a
            // null reference exception
            if (!(this.standardFont is null) && !string.IsNullOrEmpty(this.cachedText))
            {
                this.standardFont.Text = this.cachedText;
                this.cachedText = string.Empty;
            }

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            this.renderer.DrawText(IsBold ? this.boldFont : this.standardFont, this.standardFont.Text, Location, Forecolor);
            base.Draw(gameTime);
        }
    }
}
