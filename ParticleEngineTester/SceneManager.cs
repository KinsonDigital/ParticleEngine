// <copyright file="SceneManager.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.UI;

    /// <summary>
    /// Manages scenes.
    /// </summary>
    public class SceneManager : ISceneManger
    {
        private const int ButtonSpacing = 10;
        private readonly List<IScene> scenes = new List<IScene>();
        private readonly IControlFactory ctrlFactory;
        private readonly IControl prevButton;
        private readonly IControl nextButton;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneManager"/> class.
        /// </summary>
        /// <param name="ctrlFactory">Creates controls for the scene manager.</param>
        public SceneManager(IControlFactory ctrlFactory)
        {
            this.ctrlFactory = ctrlFactory;

#pragma warning disable IDE0017 // Simplify object initialization
            this.prevButton = ctrlFactory.CreateButton("prev-btn", "Graphics/prev-button");
            this.prevButton.Click += PreviousButton_Click;

            this.nextButton = ctrlFactory.CreateButton("next-btn", "Graphics/next-button");
            this.nextButton.Click += NextButton_Click;
#pragma warning restore IDE0017 // Simplify object initialization

            UpdateButtonStates();
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

        /// <inheritdoc/>
        public event EventHandler<SceneChangedEventArgs>? SceneChanged;

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
        public int CurrentSceneIndex { get; private set; } = -1;

        /// <inheritdoc/>
        public void AddScene(IScene scene)
        {
            if (SceneNameAlReadyExists(scene.Name))
            {
                throw new Exception($"A scene with the name '{scene.Name}' already has been added to the '{nameof(SceneManager)}'.  Duplicate scene names not aloud.'");
            }

            this.scenes.Add(scene);

            CurrentSceneIndex = this.scenes.Count == 1 ? 0 : CurrentSceneIndex;
        }

        /// <inheritdoc/>
        public void ActivateScene(string name)
        {
            var sceneIndex = GetSceneIndexByName(name);

            if (sceneIndex == -1)
            {
                return;
            }

            var prevSceneIndex = CurrentSceneIndex;

            CurrentSceneIndex = sceneIndex;

            // Only invoked the scene changed event if the scene has actually changed.
            if (prevSceneIndex != CurrentSceneIndex)
            {
                this.SceneChanged?.Invoke(this, new SceneChangedEventArgs(this.scenes[prevSceneIndex].Name, this.scenes[CurrentSceneIndex].Name));
            }
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

            var prevSceneIndex = CurrentSceneIndex;

            CurrentSceneIndex = CurrentSceneIndex >= this.scenes.Count - 1
                ? this.scenes.Count - 1
                : CurrentSceneIndex + 1;

            this.SceneChanged?.Invoke(this, new SceneChangedEventArgs(this.scenes[prevSceneIndex].Name, this.scenes[CurrentSceneIndex].Name));
        }

        /// <inheritdoc/>
        public void PreviousScene()
        {
            if (this.scenes.Count <= 0)
            {
                return;
            }

            var prevSceneIndex = CurrentSceneIndex;

            CurrentSceneIndex = CurrentSceneIndex <= 0
                ? 0
                : CurrentSceneIndex - 1;

            this.SceneChanged?.Invoke(this, new SceneChangedEventArgs(this.scenes[prevSceneIndex].Name, this.scenes[CurrentSceneIndex].Name));
        }

        /// <inheritdoc/>
        public void Update(GameTime gameTime)
        {
            if (!Enabled)
            {
                return;
            }

            if (this.scenes.Count > 0 && CurrentSceneIndex != -1)
            {
                this.scenes[CurrentSceneIndex].Update(gameTime);
            }

            UpdateButtonStates();

            this.prevButton.Update(gameTime);
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

            this.prevButton.Draw(gameTime);
            this.nextButton.Draw(gameTime);
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

            if (disposing)
            {
                foreach (var scene in this.scenes)
                {
                    scene.Dispose();
                }
            }

            this.isDisposed = true;
        }

        /// <summary>
        /// Invoked when the previous scene button has been clicked.
        /// </summary>
        private void PreviousButton_Click(object? sender, ClickedEventArgs e) => PreviousScene();

        /// <summary>
        /// Invoked when the next scene button has been clicked.
        /// </summary>
        private void NextButton_Click(object? sender, ClickedEventArgs e) => NextScene();

        /// <summary>
        /// Updates the state of the previous and next buttons locations and enabled/disabled state.
        /// </summary>
        private void UpdateButtonStates()
        {
            // Update the locations of the buttons
            this.prevButton.Right = this.nextButton.Left - ButtonSpacing;
            this.prevButton.Bottom = Main.WindowHeight - ButtonSpacing;

            this.nextButton.Right = Main.WindowWidth - ButtonSpacing;
            this.nextButton.Bottom = Main.WindowHeight - ButtonSpacing;

            this.prevButton.Enabled = CurrentSceneIndex > 0;
            this.nextButton.Enabled = CurrentSceneIndex < this.scenes.Count - 1;
        }

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
                }
            }

            return total > 0;
        }

        /// <summary>
        /// Gets the index of a scene that matches the given scene <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the scene.</param>
        /// <returns>The index of the scene.</returns>
        private int GetSceneIndexByName(string name)
        {
            var sceneExists = SceneNameAlReadyExists(name);

            if (sceneExists)
            {
                for (var i = 0; i < this.scenes.Count; i++)
                {
                    if (this.scenes[i].Name == name)
                    {
                        return i;
                    }
                }
            }

            return -1;
        }
    }
}
