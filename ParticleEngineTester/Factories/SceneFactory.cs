// <copyright file="SceneFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

// TODO: Create unit tests for this class.

namespace ParticleEngineTester.Factories
{
    using System;
    using System.Linq;
    using ParticleEngineTester.Scenes;

    /// <summary>
    /// Creates scenes that can be rendered to the screen.
    /// </summary>
    public class SceneFactory : ISceneFactory
    {
        private static IRenderer? renderer;
        private static IContentLoader? contentLoader;
        private readonly string[] sceneKeys;

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneFactory"/> class.
        /// </summary>
        /// <param name="renderer">Used to render created controls.</param>
        /// <param name="contentLoader">Used to load content for controls.</param>
        /// <param name="sceneTypeKeys">A list of scene keys to understand what kind of scenes to create.</param>
        public SceneFactory(IRenderer renderer, IContentLoader contentLoader, string[] sceneTypeKeys)
        {
            SceneFactory.renderer = renderer;
            SceneFactory.contentLoader = contentLoader;
            this.sceneKeys = sceneTypeKeys;
        }

        /// <inheritdoc/>
        public IScene? CreateScene(string sceneKey)
        {
            if (renderer is null)
            {
                throw new ArgumentNullException(nameof(renderer), "The parameter must not be null.");
            }

            if (contentLoader is null)
            {
                throw new ArgumentNullException(nameof(contentLoader), "The parameter must not be null.");
            }

            if (!SceneList.SceneKeys.Contains(sceneKey))
            {
                throw new ArgumentException(nameof(sceneKey), "The given scene key is invalid.");
            }

            IScene? newScene = null;

            switch (sceneKey)
            {
                case "angular-velocity-scene":
                    newScene = new AngularVelocityScene(renderer, contentLoader, sceneKey);
                    break;
                case "x-velocity-scene":
                    newScene = new XVelocityScene(renderer, contentLoader, sceneKey);
                    break;
                case "y-velocity-scene":
                    newScene = new YVelocityScene(renderer, contentLoader, sceneKey);
                    break;
                case "size-scene":
                    newScene = new SizeScene(renderer, contentLoader, sceneKey);
                    break;
                case "red-color-scene":
                    newScene = new RedColorScene(renderer, contentLoader, sceneKey);
                    break;
            }

            return newScene;
        }
    }
}
