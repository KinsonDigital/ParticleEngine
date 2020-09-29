// <copyright file="Main.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.IO;
    using System.Reflection;
    using KDParticleEngine;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.Scenes;
    using ParticleEngineTester.UI;
    using EngineVersion = KDParticleEngine.Version;

    /// <summary>
    /// The main part of the application.
    /// </summary>
    public class Main : Game
    {
        private static string appVersion = "not set";
        private static string particleEngineVersion = "not set";
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch? spriteBatch;
        private SpriteFont? versionFont;
        private float versionTextHeight;
        private IRenderer? renderer;
        private IContentLoader? contentLoader;
        private ISceneManger? sceneManager;
        private IControlFactory? ctrlFactory;
        private SceneFactory? sceneFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="Main"/> class.
        /// </summary>
        public Main()
        {
            this.graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += Window_ClientSizeChanged;

            TargetElapsedTime = TimeSpan.FromSeconds(1d / 60d);

            this.graphics.PreparingDeviceSettings += (sender, e) =>
            {
                e.GraphicsDeviceInformation.PresentationParameters.PresentationInterval = PresentInterval.Immediate;
            };

            var versionInfo = Assembly.GetExecutingAssembly()?.GetName().Version;

            appVersion = versionInfo is null ? "error: version unknown" : $"Particle Engine Tester: v{versionInfo.Major}.{versionInfo.Minor}.{versionInfo.Build}";
            particleEngineVersion = EngineVersion.GetVersion();
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
            this.graphics.SetWindowSize(1200, 1000);

            WindowWidth = Window.ClientBounds.Width;
            WindowHeight = Window.ClientBounds.Height;

            Content.RootDirectory = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Content\";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Window.Title = "Particle Engine Tester";

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.renderer = new Renderer(this.spriteBatch);
            this.contentLoader = new ContentLoader(Content);

            this.ctrlFactory = new ControlFactory(this.renderer, this.contentLoader);
            this.sceneFactory = new SceneFactory(this.renderer, this.contentLoader);

            this.sceneManager = new SceneManager(this.ctrlFactory, Window.ClientBounds.Width, Window.ClientBounds.Height);
            this.sceneManager.SceneChanged += SceneManager_SceneChanged;

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

            this.versionFont = Content.Load<SpriteFont>("Fonts/version-font");
            this.versionTextHeight = this.versionFont.MeasureString(appVersion).Y;

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
            GraphicsDevice.Clear(new Color(45, 45, 45, 255));

            this.renderer?.Begin();

            this.sceneManager?.Draw(gameTime);

            RenderVersions();

            this.renderer?.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Invoked when the scene manager changes scenes.
        /// </summary>
        private void SceneManager_SceneChanged(object? sender, SceneChangedEventArgs e)
        {
            var sections = e.CurrentScene.Split("-");

            Window.Title = $"{sections[0].ToUpperFirstChar()} {sections[1].ToUpperFirstChar()}";
        }

        /// <summary>
        /// Occurs every time the scene is changed.
        /// </summary>
        private void MenuScene_MenuClicked(object? sender, MenuItemClickedEventArgs e) => this.sceneManager?.ActivateScene(e.MenuItemName);

        /// <summary>
        /// Renders the version numbers to the bottom left corner of the window.
        /// </summary>
        private void RenderVersions()
        {
            var testerVersionPosition = new Vector2(10, WindowHeight - ((this.versionTextHeight * 2) + 20));
            var engineVersionPosition = new Vector2(10, testerVersionPosition.Y + this.versionTextHeight + 10);

            this.spriteBatch?.DrawString(this.versionFont, appVersion, testerVersionPosition, new Color(255, 255, 0, 255));

            this.spriteBatch?.DrawString(this.versionFont, particleEngineVersion, engineVersionPosition, new Color(255, 255, 0, 255));
        }

        /// <summary>
        /// Creates all of the scenes for the application.
        /// </summary>
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

            var menuScene = new MenuScene(this.renderer, this.contentLoader, "menu-scene");

            menuScene.MenuClicked += MenuScene_MenuClicked;

            this.sceneManager?.AddScene(menuScene);

            foreach (var sceneKey in SceneList.SceneKeys)
            {
                var newScene = this.sceneFactory?.CreateScene(sceneKey);

                if (!(newScene is null))
                {
                    this.sceneManager?.AddScene(newScene);
                }
            }
        }
    }
}
