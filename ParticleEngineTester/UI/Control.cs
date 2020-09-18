// <copyright file="Control.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// A user interface control to be rendered.
    /// </summary>
    public abstract class Control : IControl
    {
        private readonly IMouse mouse;
        private MouseState currentMouseState;
        private MouseState previousMouseState;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Control"/> class.
        /// </summary>
        /// <param name="mouse">The mouse that will be interacting with the control.</param>
        public Control(IMouse mouse) => this.mouse = mouse;

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? Click;

#pragma warning disable CS0067 // The event is never used
        /// <inheritdoc/>
        public event EventHandler<EventArgs>? EnabledChanged;

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? UpdateOrderChanged;

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? DrawOrderChanged;

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? VisibleChanged;
#pragma warning restore CS0067 // The event is never used

        /// <inheritdoc/>
        public Vector2 Location { get; set; } = Vector2.Zero;

        /// <inheritdoc/>
        public int Width => GetWidth is null ? 0 : GetWidth();

        /// <inheritdoc/>
        public int Height => GetHeight is null ? 0 : GetHeight();

        /// <inheritdoc/>
        public int Left
        {
            get => (int)Location.X;
            set => Location = new Vector2(value, Location.Y);
        }

        /// <inheritdoc/>
        public int Right
        {
            get => Left + Width;
            set => Location = new Vector2(value - Width, Location.Y);
        }

        /// <inheritdoc/>
        public int Top
        {
            get => (int)Location.Y;
            set => Location = new Vector2(Location.Y, value);
        }

        /// <inheritdoc/>
        public int Bottom
        {
            get => Top + Height;
            set => Location = new Vector2(Location.X, value - Height);
        }

        /// <inheritdoc/>
        public bool Enabled { get; set; } = true;

        /// <inheritdoc/>
        public int UpdateOrder { get; set; } = 1;

        /// <inheritdoc/>
        public int DrawOrder { get; set; } = 1;

        /// <inheritdoc/>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets or sets the width of the control.
        /// </summary>
        internal Func<int>? GetWidth { get; set; }

        /// <summary>
        /// Gets or sets the height of the control.
        /// </summary>
        internal Func<int>? GetHeight { get; set; }

        /// <summary>
        /// Gets a value indicating whether the mouse is in the down position over the <see cref="Control"/>.
        /// </summary>
        protected bool IsMouseDown { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the mouse is positioned over the <see cref="Control"/>.
        /// </summary>
        protected bool IsMouseOver { get; private set; }

        /// <inheritdoc/>
        public virtual void Update(GameTime gameTime)
        {
            this.currentMouseState = this.mouse.GetState();

            var isMouseOver = new Rectangle((int)Location.X, (int)Location.Y, Width, Height)
                .Contains(this.currentMouseState.X, this.currentMouseState.Y);

            // If the mouse is over the button
            if (isMouseOver)
            {
                IsMouseOver = true;

                switch (this.currentMouseState.LeftButton)
                {
                    case ButtonState.Released:
                        IsMouseDown = false;
                        break;
                    case ButtonState.Pressed:
                        IsMouseDown = true;
                        break;
                }
            }
            else
            {
                IsMouseOver = false;
            }

            if (IsMouseOver && this.currentMouseState.LeftButton == ButtonState.Released && this.previousMouseState.LeftButton == ButtonState.Pressed)
            {
                this.Click?.Invoke(this, new EventArgs());
            }

            this.previousMouseState = this.currentMouseState;
        }

        /// <inheritdoc/>
        public virtual void Draw(GameTime gameTime)
        {
        }

        /// <inheritdoc/>
        public virtual void OnClick() => this.Click?.Invoke(this, new EventArgs());

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (!(this.Click is null))
                {
                    foreach (var subscription in this.Click.GetInvocationList())
                    {
                        this.Click -= subscription as EventHandler<EventArgs>;
                    }
                }
            }

            this.isDisposed = true;
        }
    }
}
