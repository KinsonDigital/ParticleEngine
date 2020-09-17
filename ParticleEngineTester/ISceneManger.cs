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

        void LoadContent();

        void NextScene();

        void PreviousScene();
    }
}
