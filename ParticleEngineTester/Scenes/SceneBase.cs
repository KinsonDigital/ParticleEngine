// <copyright file="SceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System;
    using Microsoft.Xna.Framework;

    public abstract class SceneBase : IScene
    {
        protected readonly IRenderer renderer;
        protected readonly IContentLoader contentLoader;

        public event EventHandler<EventArgs> EnabledChanged;

        public event EventHandler<EventArgs> UpdateOrderChanged;

        public event EventHandler<EventArgs> DrawOrderChanged;

        public event EventHandler<EventArgs> VisibleChanged;

        public SceneBase(IRenderer renderer, IContentLoader contentLoader)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
            }

            if (contentLoader is null)
            {
                throw new ArgumentNullException(nameof(contentLoader), "The parameter must not be null.");
            }

            this.renderer = renderer;
            this.contentLoader = contentLoader;
        }

        public string Name { get; set; } = string.Empty;

        public bool Enabled { get; set; } = true;

        public int UpdateOrder { get; set; } = 1;

        public int DrawOrder { get; set; } = 1;

        public bool Visible { get; set; } = true;

        public virtual void LoadContent()
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
