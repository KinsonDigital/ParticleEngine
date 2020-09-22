// <copyright file="SceneManager.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using ParticleEngineTester.UI;

    /// <summary>
    /// Manages scenes.
    /// </summary>
    public class SceneManager : ISceneManger
    {
        private const int ButtonSpacing = 10;
        private readonly List<IScene> scenes = new List<IScene>();
        private readonly int windowHeight;
        private readonly int windowWidth;
        private readonly Control previousButton;
        private readonly Control nextButton;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render any graphics for the scenes.</param>
        /// <param name="contentLoader">Loads content for scenes in the scene manager.</param>
        /// <param name="windowWidth">The width of the window.</param>
        /// <param name="windowHeight">The height of the window.</param>
        public SceneManager(IRenderer renderer, IContentLoader contentLoader, int windowWidth, int windowHeight)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
            }

            if (contentLoader is null)
            {
                throw new ArgumentNullException(nameof(contentLoader), "The parameter must not be null.");
            }

            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

#pragma warning disable IDE0017 // Simplify object initialization
            this.nextButton = new Button(renderer, contentLoader, new MouseInput(), "Graphics/next-button");
            this.nextButton.Right = this.windowWidth - ButtonSpacing;
            this.nextButton.Bottom = this.windowHeight - ButtonSpacing;
            this.nextButton.Click += NextButton_Click;

            this.previousButton = new Button(renderer, contentLoader, new MouseInput(), "Graphics/prev-button");
            this.previousButton.Right = this.nextButton.Left - ButtonSpacing;
            this.previousButton.Bottom = this.windowHeight - ButtonSpacing;
            this.previousButton.Click += PreviousButton_Click;
#pragma warning restore IDE0017 // Simplify object initialization
        }

#pragma warning disable CS0067 // The event is never used

        /// <inheritdoc/>
        public event EventHandler<EventArgs>? EnabledChanged;

        /// <inheritdoc/>?
        public event EventHandler<EventArgs>? UpdateOrderChanged;

        /// <inheritdoc/>?
        public event EventHandler<EventArgs>? DrawOrderChanged;

        /// <inheritdoc/>?
        public event EventHandler<EventArgs>? VisibleChanged;

#pragma warning restore CS0067 // The event is never used

        /// <inheritdoc/>
        public bool Enabled { get; set; } = true;

        /// <inheritdoc/>
        public bool Visible { get; set; } = true;

        /// <inheritdoc/>
        public int UpdateOrder { get; set; } = 1;

        /// <inheritdoc/>
        public int DrawOrder { get; set; } = 1;

        /// <inheritdoc/>
        public ReadOnlyCollection<IScene> Scenes => new ReadOnlyCollection<IScene>(this.scenes);

        /// <summary>
        /// Gets the index of the currently active scene.
        /// </summary>
        public int CurrentSceneIndex { get; private set; }

        /// <inheritdoc/>
        public void AddScene(IScene scene)
        {
            if (SceneNameAlReadyExists(scene.Name))
            {
                throw new Exception($"A scene with the name '{scene.Name}' already has been added to the '{nameof(SceneManager)}'.  Duplicate scene names not aloud.'");
            }

            this.scenes.Add(scene);
        }

        /// <inheritdoc/>
        public void LoadContent()
        {
            foreach (var scene in this.scenes)
            {
                scene.LoadContent();
            }
        }

        /// <inheritdoc/>
        public void NextScene()
        {
            if (this.scenes.Count <= 0)
            {
                return;
            }

            CurrentSceneIndex = CurrentSceneIndex >= this.scenes.Count - 1
                ? this.scenes.Count - 1
                : CurrentSceneIndex + 1;
        }

        /// <inheritdoc/>
        public void PreviousScene()
        {
            if (this.scenes.Count <= 0)
            {
                return;
            }

            CurrentSceneIndex = CurrentSceneIndex <= 0
                ? 0
                : CurrentSceneIndex - 1;
        }

        /// <inheritdoc/>
        public void Update(GameTime gameTime)
        {
            if (!Enabled)
            {
                return;
            }

            if (this.scenes.Count > 0)
            {
                this.scenes[CurrentSceneIndex].Update(gameTime);
            }

            this.previousButton.Update(gameTime);
            this.nextButton.Update(gameTime);
        }

        /// <inheritdoc/>
        public void Draw(GameTime gameTime)
        {
            if (!Visible)
            {
                return;
            }

            if (this.scenes.Count > 0)
            {
                this.scenes[CurrentSceneIndex].Draw(gameTime);
            }

            this.previousButton.Draw(gameTime);
            this.nextButton.Draw(gameTime);
        }

        /// <inheritdoc/>
        public void Dispose() => throw new NotImplementedException();

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

            if (disposing)
            {
                foreach (var scene in this.scenes)
                {
                    scene.Dispose();
                }
            }

            this.isDisposed = true;
        }

        private void PreviousButton_Click(object? sender, EventArgs e) => PreviousScene();

        private void NextButton_Click(object? sender, EventArgs e) => NextScene();

        /// <summary>
        /// Returns true if the a scene with the given <paramref name="name"/> already exists.
        /// </summary>
        /// <param name="name">The name of the scene.</param>
        /// <returns>True if the scene already exists.</returns>
        private bool SceneNameAlReadyExists(string name)
        {
            var total = 0;

            foreach (var scene in this.scenes)
            {
                if (scene.Name == name)
                {
                    total += 1;

                    if (total >= 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
