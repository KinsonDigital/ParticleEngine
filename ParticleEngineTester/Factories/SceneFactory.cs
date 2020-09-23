// <copyright file="SceneFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable SA1512 // Single-line comments should not be followed by blank line
#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
// TODO: Create unit tests for this class.
#pragma warning restore SA1512 // Single-line comments should not be followed by blank line
#pragma warning restore SA1515 // Single-line comment should be preceded by blank line

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

        /// <summary>
        /// Initializes a new instance of the <see cref="SceneFactory"/> class.
        /// </summary>
        /// <param name="renderer">Used to render created controls.</param>
        /// <param name="contentLoader">Used to load content for controls.</param>
        public SceneFactory(IRenderer renderer, IContentLoader contentLoader)
        {
            SceneFactory.renderer = renderer;
            SceneFactory.contentLoader = contentLoader;
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
                throw new ArgumentException(nameof(sceneKey), $"The given scene key '{sceneKey}' is invalid.");
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
                case "green-color-scene":
                    newScene = new GreenColorScene(renderer, contentLoader, sceneKey);
                    break;
                case "blue-color-scene":
                    newScene = new BlueColorScene(renderer, contentLoader, sceneKey);
                    break;
                case "alpha-color-scene":
                    newScene = new AlphaColorScene(renderer, contentLoader, sceneKey);
                    break;
                case "color-transition-scene":
                    newScene = new ColorTransitionScene(renderer, contentLoader, sceneKey);
                    break;
                case "bursting-effect-scene":
                    newScene = new BurstingEffectScene(renderer, contentLoader, sceneKey);
                    break;
                case "spawn-location-scene":
                    newScene = new SpawnLocationScene(renderer, contentLoader, new MouseInput(), sceneKey);
                    break;
                case "random-choice-scene":
                    newScene = new RandomChoiceScene(renderer, contentLoader, sceneKey);
                    break;
            }

            return newScene;
        }
    }
}
