// <copyright file="ISceneManger.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.Collections.ObjectModel;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Manages scenes to be updated and rendered.
    /// </summary>
    public interface ISceneManger : IUpdateable, IDrawable, IDisposable
    {
        /// <summary>
        /// Occurs when the scene has changed.
        /// </summary>
        event EventHandler<SceneChangedEventArgs>? SceneChanged;

        /// <summary>
        /// Gets a list of the scenes in the <see cref="ISceneManager"/>.
        /// </summary>
        ReadOnlyCollection<IScene> Scenes { get; }

        /// <summary>
        /// Gets the index for the current scene.
        /// </summary>
        int CurrentSceneIndex { get; }

        /// <summary>
        /// Adds the given <see cref="IScene"/>.
        /// </summary>
        /// <param name="scene">The scene to add.</param>
        void AddScene(IScene scene);

        /// <summary>
        /// Activates a <see cref="IScene"/> that matches the given name.
        /// </summary>
        /// <param name="name">The name of the scene to activate.</param>
        void ActivateScene(string name);

        /// <summary>
        /// Loads the scenes content.
        /// </summary>
        void LoadContent();

        /// <summary>
        /// Moves to the next scene.
        /// </summary>
        void NextScene();

        /// <summary>
        /// Moves to the next scene.
        /// </summary>
        void PreviousScene();
    }
}
