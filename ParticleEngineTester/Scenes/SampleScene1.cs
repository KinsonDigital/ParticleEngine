// <copyright file="SampleScene1.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using Microsoft.Xna.Framework;

    public class SampleScene1 : SceneBase
    {
        private ITexture sampleTexture;

        public SampleScene1(IRenderer renderer, IContentLoader contentLoader)
            : base(renderer, contentLoader)
        {
            Name = "SampleScene1";
        }

        public override void LoadContent()
        {
            this.sampleTexture = this.contentLoader.LoadTexture("Graphics/sample-1");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.renderer.Draw(this.sampleTexture, new Vector2(215, 302), Color.White);

            base.Draw(gameTime);
        }
    }
}
