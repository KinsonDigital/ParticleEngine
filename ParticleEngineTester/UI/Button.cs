// <copyright file="Button.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Button : Control
    {
        private IRenderer renderer;
        private ITexture texture;
        private Color[] normalPixels;
        private Color[] mouseHoverPixels;
        private Color[] mouseDownPixels;
        private Color[] disabledPixels;

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

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (Enabled)
            {
                if (this.isMouseOver)
                {
                    if (this.isMouseDown)
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

        private byte IncreaseValue(byte value, float percentage)
        {
            int intValue = value;

            var result = intValue + (intValue * percentage);

            result = result > 255 ? 255 : result;

            return (byte)result;
        }
    }
}
