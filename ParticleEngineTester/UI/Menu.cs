// <copyright file="Menu.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester.UI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using ParticleEngineTester.Factories;

    /// <summary>
    /// A menu of items that can be rendered to the screen.
    /// </summary>
    public class Menu : Control
    {
        private readonly IList<ILabel> menuItems = new List<ILabel>();
        private readonly IControlFactory ctrlFactory;
        private bool isDisposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Menu"/> class.
        /// </summary>
        /// <param name="ctrlFactory">Creates controls necessary to compose a menu.</param>
        public Menu(IControlFactory ctrlFactory)
            : base(ctrlFactory.CreateMouse()) => this.ctrlFactory = ctrlFactory;

        /// <summary>
        /// Invoked when the mouse clicks the <see cref="Menu"/>.
        /// </summary>
        public new event EventHandler<ClickedEventArgs>? Click;

        /// <summary>
        /// Invoked when the mouse enters on top of the <see cref="Menu"/>.
        /// </summary>
        public new event EventHandler<EventArgs>? MouseEnter;

        /// <summary>
        /// Invoked when the mouse leaves the <see cref="Menu"/>.
        /// </summary>
        public new event EventHandler<EventArgs>? MouseLeave;

        /// <summary>
        /// Invoked when the mouse clicks the menu item.
        /// </summary>
        public event EventHandler<MenuItemClickedEventArgs>? ItemClicked;

        /// <summary>
        /// Gets the list of menu items.
        /// </summary>
        public ReadOnlyCollection<ILabel> MenuItems => new ReadOnlyCollection<ILabel>(this.menuItems);

        /// <summary>
        /// Gets or sets the vertical spacing in pixels between each menu item.
        /// </summary>
        public int VerticalSpacing { get; set; } = 5;

        /// <summary>
        /// Gets the width of the <see cref="Menu"/>.
        /// </summary>
        public new int Width => this.menuItems.Count <= 0 ? 0 : this.menuItems.Max(i => i.Width);

        /// <summary>
        /// Gets the height of the <see cref="Menu"/>.
        /// </summary>
        public new int Height => this.menuItems.Count <= 0 ? 0 : this.menuItems.Sum(i => i.Height) + (VerticalSpacing * (this.menuItems.Count - 1));

        /// <summary>
        /// Adds a new <see cref="Menu"/> item to the menu with the given <paramref name="text"/>.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The text of the menu item.</param>
        public void Add(string name, string text)
        {
            var newLabel = this.ctrlFactory.CreateLabel(name, text);
            newLabel.Text = text;

            newLabel.Click += MenuItems_Click;
            newLabel.MouseEnter += MenuItems_MouseEnter;
            newLabel.MouseLeave += MenuItems_MouseLeave;
            newLabel.Name = name;

            this.menuItems.Add(newLabel);
        }

        /// <summary>
        /// Adds a new <see cref="Menu"/> item to the menu with the given <paramref name="text"/>.
        /// </summary>
        /// <param name="name">The name of the menu item.</param>
        /// <param name="text">The text of the menu item.</param>
        /// <param name="foreColor">The forecolor of the menu item text.</param>
        public void Add(string name, string text, Color foreColor)
        {
            var newLabel = this.ctrlFactory.CreateLabel(name, text);
            newLabel.Text = text;
            newLabel.Forecolor = foreColor;

            newLabel.Click += MenuItems_Click;
            newLabel.MouseEnter += MenuItems_MouseEnter;
            newLabel.MouseLeave += MenuItems_MouseLeave;
            newLabel.Name = name;

            this.menuItems.Add(newLabel);
        }

        /// <inheritdoc/>
        public override void Update(GameTime gameTime)
        {
            UpdateLabelLocations();

            foreach (var item in this.menuItems)
            {
                item.Update(gameTime);
            }

            base.Update(gameTime);
        }

        /// <inheritdoc/>
        public override void Draw(GameTime gameTime)
        {
            foreach (var item in this.menuItems)
            {
                item.Draw(gameTime);
            }

            base.Draw(gameTime);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="disposing">True to dispose of managed resources.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.isDisposed || IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                foreach (var item in this.menuItems)
                {
                    item.Dispose();
                }
            }

            this.isDisposed = true;
            base.Dispose(disposing);
        }

        /// <summary>
        /// Invoked any the menu item is clicked.
        /// </summary>
        private void MenuItems_Click(object? sender, EventArgs e)
        {
            if (!(sender is ILabel label))
            {
                return;
            }

            ItemClicked?.Invoke(label, new MenuItemClickedEventArgs(label.Name));
            this.Click?.Invoke(this, new ClickedEventArgs(Name));
        }

        /// <summary>
        /// Invoked when the mouse enters the menu.
        /// </summary>
        private void MenuItems_MouseEnter(object? sender, EventArgs e)
        {
            if (!(sender is ILabel label))
            {
                return;
            }

            label.IsBold = true;
            this.MouseEnter?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Invoked when the mouse leaves the menu.
        /// </summary>
        private void MenuItems_MouseLeave(object? sender, EventArgs e)
        {
            if (!(sender is ILabel label))
            {
                return;
            }

            label.IsBold = false;
            this.MouseLeave?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Updates the location of all the labels.
        /// </summary>
        /// <remarks>
        ///     The left side of all the menu items should be all left aligned and equal.
        ///     Each menu item should be below the previous from top to bottom (first item to last).
        /// </remarks>
        private void UpdateLabelLocations()
        {
            for (var i = 0; i < this.menuItems.Count; i++)
            {
                if (i == 0)
                {
                    this.menuItems[i].Location = Location;
                }
                else
                {
                    this.menuItems[i].Left = (int)Location.X;
                    this.menuItems[i].Top = this.menuItems[i - 1].Bottom + VerticalSpacing;
                }
            }
        }
    }
}
