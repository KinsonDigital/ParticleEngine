// <copyright file="SampleScene2.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using Microsoft.Xna.Framework;

    public class SampleScene2 : SceneBase
    {
        private ITexture sampleTexture;

        public SampleScene2(IRenderer renderer, IContentLoader contentLoader)
            : base(renderer, contentLoader)
        {
            Name = "SampleScene2";
        }

        public override void LoadContent()
        {
            this.sampleTexture = this.contentLoader.LoadTexture("Graphics/sample-2");

            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.renderer.Draw(this.sampleTexture, new Vector2(315, 250), Color.White);

            base.Draw(gameTime);
        }
    }
}
