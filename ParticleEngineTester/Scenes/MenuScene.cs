// <copyright file="MenuScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using Microsoft.Xna.Framework;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.UI;

    /// <summary>
    /// The main menu scene used to choose scenes.
    /// </summary>
    public class MenuScene : SceneBase
    {
        private readonly ControlFactory ctrlFactory;
        private readonly Menu menu;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuScene"/> class.
        /// </summary>
        /// <param name="renderer">The renderer used to render the scene.</param>
        /// <param name="contentLoader">The loader used to load the scene content.</param>
        /// <param name="name">The name of the scene.</param>
        public MenuScene(IRenderer renderer, IContentLoader contentLoader, string name)
            : base(renderer, contentLoader, name)
        {
            this.ctrlFactory = new ControlFactory(Renderer, ContentLoader);
            this.menu = new Menu(this.ctrlFactory);
        }

        /// <inheritdoc/>
        public override void LoadContent()
        {
            this.menu.Add("Item 1", "Item #1");
            this.menu.VerticalSpacing = 15;
            this.menu.MenuItemClicked += Menu_MenuItemClicked;

            this.menu.Location = new Vector2(Main.WindowCenter.X - (this.menu.Width / 2), Main.WindowCenter.Y - (this.menu.Height / 2));

            base.LoadContent();
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            this.menu.Update(gameTime);

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            this.menu.Draw(gameTime);

            base.Draw(gameTime);
        }

        /// <summary>
        /// Invoked when a menu item has been clicked, and loads a scene.
        /// </summary>
        private void Menu_MenuItemClicked(object? sender, MenuItemClickedEventArgs e)
        {
            switch (e.MenuItemName)
            {
                default:
                    break;
            }
        }
    }
}
