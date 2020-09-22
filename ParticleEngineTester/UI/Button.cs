// <copyright file="Button.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A simple button that can be rendered and clicked using the mouse.
    /// </summary>
    public class Button : Control, IButton
    {
        private readonly IRenderer renderer;
        private readonly ITexture texture;
        private readonly Color[] normalPixels;
        private readonly Color[] mouseHoverPixels;
        private readonly Color[] mouseDownPixels;
        private readonly Color[] disabledPixels;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the <see cref="Button"/>.</param>
        /// <param name="contentLoader">Loads content for the <see cref="Button"/>.</param>
        /// <param name="mouse">The mouse that will interact with the <see cref="Button"/>.</param>
        /// <param name="textureContent">The name of the button texture content to load.</param>
        public Button(
            IRenderer renderer,
            IContentLoader contentLoader,
            IMouse mouse,
            string textureContent)
            : base(mouse)
        {
            this.renderer = renderer;

            this.texture = contentLoader.LoadTexture(textureContent);

            // Create all of the pixels for the different button states
            this.normalPixels = new Color[this.texture.Width * this.texture.Height];
            this.mouseHoverPixels = new Color[this.texture.Width * this.texture.Height];
            this.mouseDownPixels = new Color[this.texture.Width * this.texture.Height];
            this.disabledPixels = new Color[this.texture.Width * this.texture.Height];

            this.texture.GetData(this.normalPixels);

            // Set the mouse hover pixels
            for (var i = 0; i < this.normalPixels.Length; i++)
            {
                this.mouseHoverPixels[i] = new Color(
                    this.normalPixels[i].R,
                    IncreaseValue(this.normalPixels[i].G, 0.15f),
                    this.normalPixels[i].B,
                    this.normalPixels[i].A);
            }

            // Set the mouse down pixels
            for (var i = 0; i < this.normalPixels.Length; i++)
            {
                this.mouseDownPixels[i] = new Color(
                    this.normalPixels[i].R,
                    IncreaseValue(this.normalPixels[i].G, 0.50f),
                    this.normalPixels[i].B,
                    this.normalPixels[i].A);
            }

            // Set the disabled pixels
            for (var i = 0; i < this.normalPixels.Length; i++)
            {
                this.disabledPixels[i] = new Color(
                    IncreaseValue(this.normalPixels[i].R, 0.70f),
                    IncreaseValue(this.normalPixels[i].G, 0.70f),
                    IncreaseValue(this.normalPixels[i].B, 0.70f),
                    this.normalPixels[i].A);
            }

            GetWidth = () =>
            {
                return this.texture.Width;
            };

            GetHeight = () =>
            {
                return this.texture.Height;
            };
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            if (Enabled)
            {
                if (IsMouseOver)
                {
                    if (IsMouseDown)
                    {
                        this.texture.SetData(this.mouseDownPixels);
                    }
                    else
                    {
                        this.texture.SetData(this.mouseHoverPixels);
                    }
                }
                else
                {
                    this.texture.SetData(this.normalPixels);
                }
            }
            else
            {
                this.texture.SetData(this.disabledPixels);
            }

            this.renderer.Draw(this.texture, Location, Color.White);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Returns the given <paramref name="value"/> by the given <paramref name="percentage"/> amount.
        /// </summary>
        /// <param name="value">The value to increase.</param>
        /// <param name="percentage">The percentage to increase the <paramref name="value"/> by.</param>
        /// <returns>The <paramref name="value"/> increased by the given <paramref name="percentage"/>.</returns>
        private byte IncreaseValue(byte value, float percentage)
        {
            int intValue = value;

            var result = intValue + (intValue * percentage);

            result = result > 255 ? 255 : result;

            return (byte)result;
        }
    }
}
