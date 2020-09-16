// <copyright file="Button.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Headers;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    public class Button : IButton
    {
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private ISpriteRenderer renderer;
        private ITexture texture;
        private readonly IMouse mouse;
        private bool isMouseDown = false;
        private bool isMouseOver = false;
        private Color[] normalPixels;
        private Color[] mouseHoverPixels;
        private Color[] mouseDownPixels;
        private Color[] disabledPixels;

        public event EventHandler<EventArgs>? Click;

        public event EventHandler<EventArgs>? EnabledChanged;

        public event EventHandler<EventArgs>? UpdateOrderChanged;

        public event EventHandler<EventArgs>? DrawOrderChanged;

        public event EventHandler<EventArgs>? VisibleChanged;

        public Button(
            ISpriteRenderer renderer,
            IContentLoader contentLoader,
            IMouse mouse,
            string normalTextureContent)
        {
            this.renderer = renderer;

            this.texture = contentLoader.Load(normalTextureContent);

            this.mouse = mouse;

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
        }

        private byte IncreaseValue(byte value, float percentage)
        {
            int intValue = value;

            var result = intValue + (intValue * percentage);

            result = result > 255 ? 255 : result;
            result = result < 0 ? 0 : result;

            return (byte)result;
        }

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 1;

        public int DrawOrder { get; set; } = 1;

        public bool Visible { get; set; } = true;

        public Vector2 Location { get; set; } = Vector2.Zero;

        public int Width => this.texture.Width;

        public int Height => this.texture.Height;

        public int Left
        {
            get => (int)Location.X;
            set
            {
                Location = new Vector2(value, Location.Y);
            }
        }

        public int Right
        {
            get => Left + Width;
            set
            {
                Location = new Vector2(value - Width, Location.Y);
            }
        }

        public int Top
        {
            get => (int)Location.Y;
            set
            {
                Location = new Vector2(Location.Y, value);
            }
        }

        public int Bottom
        {
            get => Top + Height;
            set
            {
                Location = new Vector2(Location.X, value - Height);
            }
        }

        public void Update(GameTime gameTime)
        {
            this.currentMouseState = this.mouse.GetState();

            // If the mouse is over the button
            if (IsMouseOver(this.currentMouseState.X, this.currentMouseState.Y))
            {
                this.isMouseOver = true;
                switch (this.currentMouseState.LeftButton)
                {
                    case ButtonState.Released:
                        this.isMouseDown = false;
                        break;
                    case ButtonState.Pressed:
                        this.isMouseDown = true;
                        break;
                }
            }
            else
            {
                this.isMouseOver = false;
            }

            if (this.isMouseOver && this.currentMouseState.LeftButton == ButtonState.Released && this.previousMouseState.LeftButton == ButtonState.Pressed)
            {
                this.Click?.Invoke(this, new EventArgs());
            }

            this.previousMouseState = this.currentMouseState;
        }

        public void Draw(GameTime gameTime)
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
        }

        private bool IsMouseOver(int x, int y)
        {
            return new Rectangle((int)Location.X, (int)Location.Y, this.texture.Width, this.texture.Height).Contains(x, y);
        }
    }
}
