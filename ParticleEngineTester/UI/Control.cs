using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace ParticleEngineTester.UI
{
    public abstract class Control : IControl
    {
        protected MouseState currentMouseState;
        protected MouseState previousMouseState;
        protected bool isMouseDown = false;
        protected bool isMouseOver = false;
        private readonly IMouse mouse;
        private bool isDisposed;

        public Control(IMouse mouse) => this.mouse = mouse;

        public event EventHandler<EventArgs>? Click;

        public event EventHandler<EventArgs>? EnabledChanged;

        public event EventHandler<EventArgs>? UpdateOrderChanged;

        public event EventHandler<EventArgs>? DrawOrderChanged;

        public event EventHandler<EventArgs>? VisibleChanged;

        public Vector2 Location { get; set; } = Vector2.Zero;

        internal Func<int> GetWidth { get; set; }

        internal Func<int> GetHeight { get; set; }

        public int Width => GetWidth();

        public int Height => GetHeight();

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

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 1;

        public int DrawOrder { get; set; } = 1;

        public bool Visible { get; set; } = true;

        public virtual void Update(GameTime gameTime)
        {
            this.currentMouseState = this.mouse.GetState();

            var isMouseOver = new Rectangle((int)Location.X, (int)Location.Y, Width, Height)
                .Contains(this.currentMouseState.X, this.currentMouseState.Y);

            // If the mouse is over the button
            if (isMouseOver)
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

        public virtual void Draw(GameTime gameTime)
        {
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var subscription in this.Click?.GetInvocationList())
                {
                    this.Click -= subscription as EventHandler<EventArgs>;
                }
            }

            this.isDisposed = true;
        }

        public virtual void OnClick()
        {
            this.Click?.Invoke(this, new EventArgs());
        }
    }
}
