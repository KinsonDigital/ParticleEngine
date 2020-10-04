// <copyright file="ColorTransitionScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System.Diagnostics.CodeAnalysis;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles that transition from one color to another for the purpose of testing.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ColorTransitionScene : ParticleEngineSceneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTransitionScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public ColorTransitionScene(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
        }

        /// <inheritdoc/>
        public override void LoadContent()
        {
            var settings = new BehaviorSettings[]
            {
                new ColorTransitionBehaviorSettings()
                {
                    EasingFunctionType = EasingFunction.EaseIn,
                    LifeTime = 3000,
                    StartColor = new ParticleColor(255, 0, 255, 0),
                    StopColor = new ParticleColor(255, 178, 0, 255),
                },
            };

            var effect = new ParticleEffect("Graphics/gear-particle", settings)
            {
                BurstEnabled = false,
                SpawnLocation = SceneCenter.ToPointF(),
                SpawnRateMin = 1000,
                SpawnRateMax = 1000,
            };

            Engine.CreatePool(effect, new BehaviorFactory());

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime) => base.Draw(gameTime);
    }
}
