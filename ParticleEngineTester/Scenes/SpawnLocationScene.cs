// <copyright file="SpawnLocationScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;

    /// <summary>
    /// Shows particles with Blue color component for the purpose of testing.
    /// </summary>
    public class SpawnLocationScene : ParticleEngineSceneBase
    {
        private readonly IMouse mouse;
        private MouseState currentMouseState;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnLocationScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="mouse">Used to respond to mouse input.</param>
        /// <param name="name">The name of the scene.</param>
        public SpawnLocationScene(IRenderer renderer, IContentLoader contentLoader, IMouse mouse, string name)
            : base(renderer, contentLoader, name) => this.mouse = mouse;

        /// <inheritdoc/>
        public override void LoadContent()
        {
            var settings = new BehaviorSettings[]
            {
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Size,
                    StartMin = 0.5f,
                    StartMax = 0.5f,
                    ChangeMin = 0,
                    ChangeMax = 0,
                    TotalTimeMin = 1,
                    TotalTimeMax = 1,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.X,
                    StartMin = SceneCenter.X,
                    StartMax = SceneCenter.X,
                    UpdateStartMin = () => this.currentMouseState.X,
                    UpdateStartMax = () => this.currentMouseState.X,
                    ChangeMin = -200,
                    ChangeMax = 200,
                    TotalTimeMin = 1500,
                    TotalTimeMax = 1500,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
                new EasingRandomBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Y,
                    StartMin = SceneCenter.Y,
                    StartMax = SceneCenter.Y,
                    UpdateStartMin = () => this.currentMouseState.Y,
                    UpdateStartMax = () => this.currentMouseState.Y,
                    ChangeMin = -200,
                    ChangeMax = 200,
                    TotalTimeMin = 1500,
                    TotalTimeMax = 1500,
                    EasingFunctionType = EasingFunction.EaseIn,
                },
            };

            var effect = new ParticleEffect("Graphics/gear-particle", settings)
            {
                BurstEnabled = false,
                SpawnLocation = SceneCenter.ToPointF(),
                SpawnRateMin = 125,
                SpawnRateMax = 125,
                TotalParticlesAliveAtOnce = 100,
            };

            Engine.CreatePool(effect, new BehaviorFactory());

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.currentMouseState = this.mouse.GetState();
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime) => base.Draw(gameTime);
    }
}
