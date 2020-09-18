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
        /// Gets the name of the scene.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Loads content for a scene.
        /// </summary>
        void LoadContent();
    }
}
