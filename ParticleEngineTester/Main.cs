// <copyright file="Main.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ParticleEngineTester.Factories;
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

        /// <summary>
        /// Gets the width of the window.
        /// </summary>
        internal static int WindowWidth { get; private set; }

        /// <summary>
        /// Gets the height of the window.
        /// </summary>
        internal static int WindowHeight { get; private set; }

        /// <summary>
        /// Gets the center location of the window.
        /// </summary>
        internal static Vector2 WindowCenter => new Vector2(WindowWidth / 2, WindowHeight / 2);

        /// <inheritdoc/>
        protected override void Initialize()
        {
            WindowWidth = Window.ClientBounds.Width;
            WindowHeight = Window.ClientBounds.Height;

            Content.RootDirectory = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Content\bin\";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Window.Title = "Particle Engine Tester";

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.renderer = new Renderer(this.spriteBatch);
            this.contentLoader = new ContentLoader(Content);

            this.sceneManager = new SceneManager(this.renderer, this.contentLoader, Window.ClientBounds.Width, Window.ClientBounds.Height);

            CreateScenes();

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

        private void Menu_MouseEnter(object? sender, EventArgs e) => Window.Title = "Mouse Enter Menu";

        private void Menu_MouseLeave(object? sender, EventArgs e) => Window.Title = "Mouse Leave Menu";

        private void Menu_MenuItemClicked(object? sender, MenuItemClickedEventArgs e) => Window.Title = e.MenuItemName;

        private void Menu_Click(object? sender, ClickedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.Name))
            {
                return;
            }
        }

        private void CreateScenes()
        {
            if (this.renderer is null)
            {
                throw new ArgumentNullException(nameof(this.renderer), "The parameter must not be null.");
            }

            if (this.contentLoader is null)
            {
                throw new ArgumentNullException(nameof(this.contentLoader), "The parameter must not be null.");
            }

            IScene menuScene = new MenuScene(this.renderer, this.contentLoader, "menu-scene");

            this.sceneManager?.AddScene(menuScene);
        }
    }
}
