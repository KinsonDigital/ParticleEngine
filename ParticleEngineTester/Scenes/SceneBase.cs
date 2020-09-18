// <copyright file="SceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A base scene that can be used to create new scenes.
    /// </summary>
    public abstract class SceneBase : IScene
    {
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneBase"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">Loads content related for all of the scenes.</param>
        /// <param name="name">The name of the scene.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="renderer"/>, <paramref name="contentLoader"/>,
        ///     or <paramref name="name"/> parameters are null.
        /// </exception>
        public SceneBase(IRenderer renderer, IContentLoader contentLoader, string name)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
            }

            if (contentLoader is null)
            {
                throw new ArgumentNullException(nameof(contentLoader), "The parameter must not be null.");
            }

            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name), "The parameter must not be null.");
            }

            Renderer = renderer;
            ContentLoader = contentLoader;
            Name = name;
        }

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

        /// <summary>
        /// Gets or sets the name of the scene.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <inheritdoc/>
        public bool Enabled { get; set; } = true;

        /// <inheritdoc/>
        public int UpdateOrder { get; set; } = 1;

        /// <inheritdoc/>
        public int DrawOrder { get; set; } = 1;

        /// <inheritdoc/>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// Gets renderer that renders the scene.
        /// </summary>
        protected IRenderer Renderer { get; private set; }

        /// <summary>
        /// Gets content loader to load the scene's content.
        /// </summary>
        protected IContentLoader ContentLoader { get; private set; }

        /// <inheritdoc/>
        public virtual void LoadContent()
        {
        }

        /// <inheritdoc/>
        public virtual void Draw(GameTime gameTime)
        {
        }

        /// <inheritdoc/>
        public virtual void Update(GameTime gameTime)
        {
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            this.isDisposed = true;
        }
    }
}
