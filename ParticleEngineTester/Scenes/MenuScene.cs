// <copyright file="MenuScene.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.Scenes
{
    using System;
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
        private readonly Color defaultColor = Color.White;
        private readonly Color highlightColor = new Color(60, 179, 113, 255);

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

        /// <summary>
        /// Invoked when a scene has changed.
        /// </summary>
        public event EventHandler<MenuItemClickedEventArgs>? MenuClicked;

        /// <inheritdoc/>
        public override void LoadContent()
        {
            this.menu.VerticalSpacing = 15;
            this.menu.ItemClicked += Menu_ItemClicked;

            foreach (var sceneKey in SceneList.SceneKeys)
            {
                this.menu.Add(sceneKey, sceneKey.ToTitle(), this.defaultColor);
            }

            this.menu.Location = new Vector2(SceneCenter.X - (this.menu.Width / 2), SceneCenter.Y - (this.menu.Height / 2));
            this.menu.MouseEnter += Menu_MouseEnter;

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
        /// Sets the color of the menu items based on if the mouse is over the item.
        /// </summary>
        private void Menu_MouseEnter(object? sender, EventArgs e)
        {
            if (!(sender is Menu menu))
            {
                return;
            }

            // Set the fore color of the items depending on if the mouse is over the item
            foreach (var item in menu.MenuItems)
            {
                item.Forecolor = item.IsMouseOver ? this.highlightColor : this.defaultColor;
            }
        }

        /// <summary>
        /// Invoked when a menu item has been clicked, and loads a scene.
        /// </summary>
        private void Menu_ItemClicked(object? sender, MenuItemClickedEventArgs e) => this.MenuClicked?.Invoke(this, e);
    }
}
