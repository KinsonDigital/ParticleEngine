// <copyright file="Main.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.IO;
    using System.Reflection;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ParticleEngineTester.Scenes;
    using ParticleEngineTester.UI;

    /// <summary>
    /// The main part of the application.
    /// </summary>
    public class Main : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch? spriteBatch;
        private IRenderer? renderer;
        private IContentLoader? contentLoader;
        private ISceneManger? sceneManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;

            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            this.graphics.PreparingDeviceSettings += (sender, e) =>
            {
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            };
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            Content.RootDirectory = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Content\bin\";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Window.Title = "Particle Engine Tester";

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.renderer = new Renderer(this.spriteBatch);
            this.contentLoader = new ContentLoader(Content);

            this.sceneManager = new SceneManager(this.renderer, this.contentLoader, Window.ClientBounds.Width, Window.ClientBounds.Height);

            base.Initialize();
        }

        /// <inheritdoc/>
        protected override void LoadContent()
        {
            if (this.renderer is null || this.contentLoader is null)
            {
                throw new Exception("The renderer and content loader must not be null.");
            }

            this.sceneManager?.LoadContent();
        }

        /// <inheritdoc/>
        protected override void Update(GameTime gameTime)
        {
            this.sceneManager?.Update(gameTime);

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.renderer?.Begin();

            this.sceneManager?.Draw(gameTime);

            this.renderer?.End();

            base.Draw(gameTime);
        }
    }
}
