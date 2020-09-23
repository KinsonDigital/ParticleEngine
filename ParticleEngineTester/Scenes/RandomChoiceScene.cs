// <copyright file="RandomChoiceScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System.Collections.ObjectModel;
    using KDParticleEngine;
    using KDParticleEngine.Behaviors;
    using Microsoft.Xna.Framework;

    /// <summary>
    /// Shows particles with X velocity for the purpose of testing.
    /// </summary>
    public class RandomChoiceScene : ParticleEngineSceneBase
    {
        private readonly int spriteWidth;
        private readonly int spriteHeight;
        private readonly int locationRectWidth;
        private readonly int locationRectHeight;
        private readonly int screenEdgeMargin = 150;
        private Rectangle topLeftRect = default;
        private Rectangle topRightRect = default;
        private Rectangle bottomLeftRect = default;
        private Rectangle bottomRightRect = default;
        private Color topLeftRectClr = Color.White;
        private Color topRightRectClr = Color.White;
        private Color bottomRightRectClr = Color.White;
        private Color bottomLeftRectClr = Color.White;
        private Color green = new Color(0, 255, 0, 255);

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomChoiceScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public RandomChoiceScene(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
            this.spriteWidth = 33;
            this.spriteHeight = 33;

            this.locationRectWidth = this.spriteWidth * 2;
            this.locationRectHeight = this.spriteHeight * 2;

            this.topLeftRect.X = this.screenEdgeMargin;
            this.topLeftRect.Y = this.screenEdgeMargin;
            this.topLeftRect.Width = this.locationRectWidth;
            this.topLeftRect.Height = this.locationRectHeight;

            this.topRightRect.X = SceneWidth - this.screenEdgeMargin;
            this.topRightRect.Y = this.screenEdgeMargin;
            this.topRightRect.Width = this.locationRectWidth;
            this.topRightRect.Height = this.locationRectHeight;

            this.bottomRightRect.X = SceneWidth - this.screenEdgeMargin;
            this.bottomRightRect.Y = SceneHeight - this.screenEdgeMargin;
            this.bottomRightRect.Width = this.locationRectWidth;
            this.bottomRightRect.Height = this.locationRectHeight;

            this.bottomLeftRect.X = this.screenEdgeMargin;
            this.bottomLeftRect.Y = SceneHeight - this.screenEdgeMargin;
            this.bottomLeftRect.Width = this.locationRectWidth;
            this.bottomLeftRect.Height = this.locationRectHeight;
        }

        /// <inheritdoc/>
        public override void LoadContent()
        {
            var settings = new BehaviorSettings[]
            {
                new RandomChoiceBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.X,
                    Data = new ReadOnlyCollection<string>(new[]
                    {
                        this.topLeftRect.Center.X.ToString(),
                        this.topRightRect.Center.X.ToString(),
                    }),
                    LifeTime = 500,
                },
                new RandomChoiceBehaviorSettings()
                {
                    ApplyToAttribute = ParticleAttribute.Y,
                    Data = new ReadOnlyCollection<string>(new[]
                    {
                        this.topLeftRect.Center.Y.ToString(),
                        this.bottomLeftRect.Center.Y.ToString(),
                    }),
                    LifeTime = 500,
                },
            };

            var effect = new ParticleEffect("Graphics/gear-particle", settings)
            {
                BurstEnabled = false,
                SpawnLocation = SceneCenter.ToPointF(),
                SpawnRateMin = 1000,
                SpawnRateMax = 1000,
                TotalParticlesAliveAtOnce = 1,
            };

            Engine.CreatePool(effect, new BehaviorFactory());

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            var particle = Engine.ParticlePools[0].Particles[0];

            if (this.topLeftRect.Contains(particle.Position.X, particle.Position.Y))
            {
                this.topLeftRectClr = this.green;
            }

            if (this.topRightRect.Contains(particle.Position.X, particle.Position.Y))
            {
                this.topRightRectClr = this.green;
            }

            if (this.bottomRightRect.Contains(particle.Position.X, particle.Position.Y))
            {
                this.bottomRightRectClr = this.green;
            }

            if (this.bottomLeftRect.Contains(particle.Position.X, particle.Position.Y))
            {
                this.bottomLeftRectClr = this.green;
            }

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            Renderer.DrawRectangle(this.topLeftRect, this.topLeftRectClr);
            Renderer.DrawRectangle(this.topRightRect, this.topRightRectClr);
            Renderer.DrawRectangle(this.bottomRightRect, this.bottomRightRectClr);
            Renderer.DrawRectangle(this.bottomLeftRect, this.bottomLeftRectClr);
        }
    }
}
