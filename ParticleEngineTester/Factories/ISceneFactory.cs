// <copyright file="ISceneFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Factories
{
    /// <summary>
    /// Creates scenes that can be rendered to the screen.
    /// </summary>
    public interface ISceneFactory
    {
        /// <summary>
        /// Creates a scene using the given scene key.
        /// </summary>
        /// <param name="key">A unique key to determine the type of scene to create..</param>
        /// <returns>The created scene.</returns>
        IScene? CreateScene(string key);
    }
}
