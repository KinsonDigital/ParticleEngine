// <copyright file="ParticleEngineSceneBase.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System;
    using KDParticleEngine;
    using KDParticleEngine.Services;
    using Microsoft.Xna.Framework;
    using XNARect = Microsoft.Xna.Framework.Rectangle;

    /// <summary>
    /// Provides a base for creating a scene with particles that are rendered to the screen.
    /// </summary>
    public abstract class ParticleEngineSceneBase : SceneBase
    {
        private readonly ParticleEngine engine;

        /// <summary>
        /// Initializes a new instance of the <see cref="ParticleEngineSceneBase"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">Loads content related for all of the scenes.</param>
        /// <param name="name">The name of the scene.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if the <paramref name="renderer"/>, <paramref name="contentLoader"/>,
        ///     or <paramref name="name"/> parameters are null.
        /// </exception>
        public ParticleEngineSceneBase(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
            var loader = new ParticleTextureLoader(contentLoader);
            this.engine = new ParticleEngine(loader, new TrueRandomizerService());
        }

        /// <inheritdoc/>
        public override void LoadContent() => base.LoadContent();

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            this.engine.Update(gameTime.ElapsedGameTime);

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            foreach (var pool in this.engine.ParticlePools)
            {
                foreach (var particle in pool.Particles)
                {
                    if (particle.IsAlive)
                    {
                        if (!(pool.PoolTexture is ParticleTexture monoTexture))
                        {
                            throw new NullReferenceException("The mono texture is null.");
                        }

                        var particleDestRect = new XNARect((int)particle.Position.X, (int)particle.Position.Y, (int)(pool.PoolTexture.Width * particle.Size), (int)(pool.PoolTexture.Height * particle.Size));

                        // Setup transparency for the sprite
                        var preMultipliedColor = Color.FromNonPremultiplied(particle.TintColor.R, particle.TintColor.G, particle.TintColor.B, particle.TintColor.A);

                        var srcRect = new XNARect(0, 0, monoTexture.Width, monoTexture.Height);
                        var origin = new Vector2(monoTexture.Width / 2, monoTexture.Height / 2);

                        Renderer.Draw(
                            monoTexture,
                            particleDestRect,
                            srcRect,
                            preMultipliedColor,
                            ToRadians(particle.Angle),
                            origin,
                            0f);
                    }
                }
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// Returns the given <paramref name="angle"/> in radians.
        /// </summary>
        /// <param name="angle">The angle to convert.</param>
        /// <returns>The angle in the unit of radians.</returns>
        private float ToRadians(float angle) => angle * (float)Math.PI / 180f;
    }
}
