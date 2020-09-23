// <copyright file="IScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// A single scene that can be updated and rendered.
    /// </summary>
    public interface IScene : IUpdateable, IDrawable, IDisposable
    {
        /// <summary>
        /// Gets or sets the name of the scene.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets the width of the scene.
        /// </summary>
        int SceneWidth { get; }

        /// <summary>
        /// Gets the height of the scene.
        /// </summary>
        int SceneHeight { get; }

        /// <summary>
        /// Gets the center location of the scene.
        /// </summary>
        Vector2 SceneCenter { get; }

        /// <summary>
        /// Loads content for a scene.
        /// </summary>
        void LoadContent();
    }
}
