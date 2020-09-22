// <copyright file="SceneChangedEventArgs.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;

    /// <summary>
    /// Holds information when a scene has been changed.
    /// </summary>
    public class SceneChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SceneChangedEventArgs"/> class.
        /// </summary>
        /// <param name="previousScene">The previous scene before the scene change.</param>
        /// <param name="currentScene">The current scene after the scene change.</param>
        public SceneChangedEventArgs(string previousScene, string currentScene)
        {
            PreviousScene = previousScene;
            CurrentScene = currentScene;
        }

        /// <summary>
        /// Gets the previous scene before the scene change.
        /// </summary>
        public string PreviousScene { get; private set; }

        /// <summary>
        /// Gets the current scene after the scene change.
        /// </summary>
        public string CurrentScene { get; private set; }
    }
}
