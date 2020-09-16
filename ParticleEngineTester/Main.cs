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

    public class Main : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private SpriteFont fps;
        private ISpriteRenderer renderer;
        private IContentLoader contentLoader;
        private ISceneManger sceneManager;

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

        protected override void Initialize()
        {
            Content.RootDirectory = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\Content\bin\";
            GraphicsDevice.BlendState = BlendState.AlphaBlend;
            Window.Title = "Particle Engine Tester";

            this.spriteBatch = new SpriteBatch(GraphicsDevice);
            this.renderer = new SpriteRenderer(this.spriteBatch);
            this.contentLoader = new ContentLoader(Content);

            this.sceneManager = new SceneManager(this.renderer, this.contentLoader, Window.ClientBounds.Width, Window.ClientBounds.Height);
            IScene sampleScene1 = new SampleScene1(this.renderer, this.contentLoader);
            IScene sampleScene2 = new SampleScene2(this.renderer, this.contentLoader);
            this.sceneManager.AddScene(sampleScene1);
            this.sceneManager.AddScene(sampleScene2);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.sceneManager.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            this.sceneManager.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            this.renderer.Begin();

            this.sceneManager.Draw(gameTime);

            this.renderer.End();

            base.Draw(gameTime);
        }
    }
}
