// <copyright file="YVelocityScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System.Diagnostics.CodeAnalysis;
    using System.Drawing;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles with X velocity for the purpose of testing.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class YVelocityScene : ParticleEngineSceneBase
    {
        private ParticleEffect? effect;

        /// <summary>
        /// Initializes a new instance of the <see cref="YVelocityScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public YVelocityScene(IRenderer renderer, IContentLoader contentLoader, string name)
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
                    ApplyToAttribute = ParticleAttribute.Y,
                    StartMin = SceneCenter.Y - 200,
                    UpdateStartMin = () => SceneCenter.Y - 200,
                    StartMax = SceneCenter.Y - 200,
                    UpdateStartMax = () => SceneCenter.Y - 200,
                    ChangeMin = 380,
                    ChangeMax = 380,
                    TotalTimeMin = 3000,
                    TotalTimeMax = 3000,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
            };

            this.effect = new ParticleEffect("Graphics/gear-particle", settings)
            {
                BurstEnabled = false,
                SpawnLocation = SceneCenter.ToPointF(),
                SpawnRateMin = 1000,
                SpawnRateMax = 1000,
            };

            Engine.CreatePool(this.effect, new BehaviorFactory());

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            if (!(this.effect is null))
            {
                this.effect.SpawnLocation = new PointF(SceneCenter.X, SceneCenter.Y - 200);
            }

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime) => base.Draw(gameTime);
    }
}
