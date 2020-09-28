// <copyright file="RedColorScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles with red color component for the purpose of testing.
    /// </summary>
    public class RedColorScene : ParticleEngineSceneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RedColorScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public RedColorScene(IRenderer renderer, IContentLoader contentLoader, string name)
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
                    ApplyToAttribute = ParticleAttribute.RedColorComponent,
                    StartMin = 0,
                    StartMax = 0,
                    ChangeMin = 255,
                    ChangeMax = 255,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 3000,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.GreenColorComponent,
                    StartMin = 0,
                    StartMax = 0,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = 1,
                    TotalTimeMax = 1,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.BlueColorComponent,
                    StartMin = 0,
                    StartMax = 0,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = 1,
                    TotalTimeMax = 1,
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
