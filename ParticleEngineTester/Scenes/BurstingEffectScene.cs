// <copyright file="BurstingEffectScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using KDParticleEngine.Services;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles with a bursting effect for the purpose of testing.
    /// </summary>
    public class BurstingEffectScene : ParticleEngineSceneBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BurstingEffectScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public BurstingEffectScene(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
        }

        /// <inheritdoc/>
        public override void LoadContent()
        {
            const int totalTime = 4000;

            var burstingSettings = new BehaviorSettings[]
            {
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.RedColorComponent,
                    StartMin = 170,
                    StartMax = 170,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = totalTime,
                    TotalTimeMax = totalTime,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.GreenColorComponent,
                    StartMin = 119,
                    StartMax = 119,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = totalTime,
                    TotalTimeMax = totalTime,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.BlueColorComponent,
                    StartMin = 80,
                    StartMax = 80,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = totalTime,
                    TotalTimeMax = totalTime,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.X,
                    StartMin = SceneCenter.X - 50,
                    StartMax = SceneCenter.X + 50,
                    ChangeMin = -400,
                    ChangeMax = 400,
                    TotalTimeMin = totalTime - 2000,
                    TotalTimeMax = totalTime - 2000,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Y,
                    StartMin = SceneCenter.Y, // Will bounce off the bottom of the window
                    StartMax = SceneCenter.Y,
                    ChangeMin = SceneHeight - SceneCenter.Y - 12,
                    ChangeMax = SceneHeight - SceneCenter.Y - 12,
                    TotalTimeMin = totalTime,
                    TotalTimeMax = totalTime,
                    EasingFunctionType = EasingFunction.EaseOutBounce,
                },
            };

            var burstingEffect = new ParticleEffect("Graphics/gear-particle", burstingSettings)
            {
                SpawnLocation = SceneCenter.ToPointF(),
                SpawnRateMin = 500,
                SpawnRateMax = 500,
                TotalParticlesAliveAtOnce = 100,
                BurstEnabled = true,
                BurstSpawnRateMin = 62,
                BurstSpawnRateMax = 62,
                BurstOnTime = 3000,
                BurstOffTime = 4000,
            };

            Engine.CreatePool(burstingEffect, new BehaviorFactory());

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime) => base.Update(gameTime);

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime) => base.Draw(gameTime);
    }
}
