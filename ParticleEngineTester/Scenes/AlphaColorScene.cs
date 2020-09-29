// <copyright file="AlphaColorScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System.Diagnostics.CodeAnalysis;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles with Alpha color component for the purpose of testing.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AlphaColorScene : ParticleEngineSceneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlphaColorScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public AlphaColorScene(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
        }

        /// <inheritdoc/>
        public override void LoadContent()
        {
            var settings = new BehaviorSettings[]
            {
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.AlphaColorComponent,
                    StartMin = 255,
                    StartMax = 255,
                    ChangeMin = -255,
                    ChangeMax = -255,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 3000,
                    EasingFunctionType = EasingFunction.EaseIn,
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
