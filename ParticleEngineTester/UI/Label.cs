using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace ParticleEngineTester.UI
{
    public class Label : Control
    {
        private readonly IRenderer renderer;
        private readonly IFont font;

        public Label(
            IRenderer renderer,
            IContentLoader contentLoader,
            IMouse mouse,
            string fontContent)
            : base(mouse)
        {
            this.renderer = renderer;
            this.font = contentLoader.LoadFont(fontContent);

            GetWidth = () =>
            {
                return this.font.Width;
            };

            GetHeight = () =>
            {
                return this.font.Height;
            };
        }

        public string Text
        {
            get => this.font.Text;
            set => this.font.Text = value;
        }

        public Color Forecolor { get; set; } = Color.Black;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            this.renderer.DrawText(this.font, this.font.Text, Location, Forecolor);
            base.Draw(gameTime);
        }
    }
}
