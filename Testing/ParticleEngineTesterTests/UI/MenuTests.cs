// <copyright file="MenuTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.UI
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTester.Factories;
    using ParticleEngineTester.UI;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Menu"/> class.
    /// </summary>
    public class MenuTests
    {
        private readonly Mock<IControlFactory> mockCtrlFactory;
        private readonly Mock<IContentLoader> mockContentLoader;
        private readonly Mock<IMouse> mockMouse;
        private readonly Mock<IFont> mockStandardFont;
        private readonly Mock<IFont> mockBoldFont;

        /// <summary>
        /// Initializes a new instance of the <see cref="MenuTests"/> class.
        /// </summary>
        public MenuTests()
        {
            this.mockStandardFont = new Mock<IFont>();
            this.mockStandardFont.SetupProperty(p => p.Text);
            this.mockStandardFont.SetupGet(p => p.Width).Returns(100);
            this.mockStandardFont.SetupGet(p => p.Height).Returns(100);

            this.mockBoldFont = new Mock<IFont>();
            this.mockBoldFont.SetupProperty(p => p.Text);

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.LoadFont("Fonts/standard-font")).Returns(this.mockStandardFont.Object);
            this.mockContentLoader.Setup(m => m.LoadFont("Fonts/bold-font")).Returns(this.mockBoldFont.Object);

            this.mockMouse = new Mock<IMouse>();
            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                return new MouseState(52, 52, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            });

            this.mockCtrlFactory = new Mock<IControlFactory>();
            this.mockCtrlFactory.Setup(m => m.CreateMouse()).Returns(this.mockMouse.Object);
        }

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectVerticalSpacingDefaultValue()
        {
            // Act
            var menu = CreateMenu();

            // Assert
            Assert.Equal(5, menu.VerticalSpacing);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void Add_WhenInvoking2ParamOverload_CreatesMenuItem()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            mockLabel.SetupProperty(p => p.Name);
            mockLabel.SetupProperty(p => p.Text);

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");
            var actual = menu.MenuItems;

            // Assert
            Assert.Single(actual);
            Assert.Equal("test-menu", actual[0].Name);
            Assert.Equal("test-item", actual[0].Text);
        }

        [Fact]
        public void Add_WhenInvoking2ParamOverload_RaisesClickEventWhenClicked()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");

            // Assert
            Assert.Raises<ClickedEventArgs>((handler) => // Attach
            {
                menu.Click += handler;
            }, (handler) => // Detach
            {
                menu.Click -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.Click += null, new ClickedEventArgs("test-menu"));
            });
        }

        [Fact]
        public void Add_WhenInvoking2ParamOverloadWithNullLabel_DoesNotRaiseMouseEnterEventWhenMouseEntersMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");

            // Assert
            AssertHelpers.DoesNotRaise<EventArgs>((handler) => // Attach
            {
                menu.MouseEnter += handler;
            }, (handler) => // Detach
            {
                menu.MouseEnter -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseEnter += null, null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking2ParamOverloadWithNullLabel_DoesNotRaiseMouseLeaveEventWhenMouseLeavesMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");

            // Assert
            AssertHelpers.DoesNotRaise<EventArgs>((handler) => // Attach
            {
                menu.MouseLeave += handler;
            }, (handler) => // Detach
            {
                menu.MouseLeave -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseLeave += null, null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking2ParamOverload_RaisesMouseEnterEventWhenMouseEntersMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");

            // Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                menu.MouseEnter += handler;
            }, (handler) => // Detach
            {
                menu.MouseEnter -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseEnter += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking2ParamOverload_RaisesMouseLeaveEventWhenMouseLeavesMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item");

            // Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                menu.MouseLeave += handler;
            }, (handler) => // Detach
            {
                menu.MouseLeave -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseLeave += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverload_CreatesMenuItem()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            mockLabel.SetupProperty(p => p.Name);
            mockLabel.SetupProperty(p => p.Text);
            mockLabel.SetupProperty(p => p.Forecolor);

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);
            var actual = menu.MenuItems;

            // Assert
            Assert.Single(actual);
            Assert.Equal("test-menu", actual[0].Name);
            Assert.Equal("test-item", actual[0].Text);
            Assert.Equal(Color.DarkOrange, actual[0].Forecolor);
        }

        [Fact]
        public void Add_With3ParamOverloadAndNullLabel_DoesNotRaiseClickEvent()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            AssertHelpers.DoesNotThrow<NullReferenceException>(() =>
            {
                mockLabel.Raise(e => e.Click += null, null, new ClickedEventArgs(It.IsAny<string>()));
            });
        }

        [Fact]
        public void Add_With3ParamOverloadAndNoItemClickedEventSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            AssertHelpers.DoesNotThrow<NullReferenceException>(() =>
            {
                mockLabel.Raise(e => e.Click += null, new ClickedEventArgs(It.IsAny<string>()));
            });
        }

        [Fact]
        public void Add_With3ParamOverloadAndNoClickEventSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            AssertHelpers.DoesNotThrow<NullReferenceException>(() =>
            {
                mockLabel.Raise(e => e.Click += null, new ClickedEventArgs(It.IsAny<string>()));
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverload_RaisesClickEventWhenClicked()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            // Assert
            Assert.Raises<ClickedEventArgs>((handler) => // Attach
            {
                menu.Click += handler;
            }, (handler) => // Detach
            {
                menu.Click -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.Click += null, new ClickedEventArgs("test-menu"));
            });
        }

        [Fact]
        public void Add_With3ParamOverloadAndNoMouseEnterEventSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            AssertHelpers.DoesNotThrow<NullReferenceException>(() =>
            {
                mockLabel.Raise(e => e.MouseEnter += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverloadWithNullLabel_DoesNotRaiseMouseEnterEventWhenMouseEntersMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            // Assert
            AssertHelpers.DoesNotRaise<EventArgs>((handler) => // Attach
            {
                menu.MouseEnter += handler;
            }, (handler) => // Detach
            {
                menu.MouseEnter -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseEnter += null, null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_With3ParamOverloadAndNoMouseLeaveEventSubscription_DoesNotThrowNullReferenceException()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            AssertHelpers.DoesNotThrow<NullReferenceException>(() =>
            {
                mockLabel.Raise(e => e.MouseLeave += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverloadWithNullLabel_DoesNotRaiseMouseLeaveEventWhenMouseLeavesMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            // Assert
            AssertHelpers.DoesNotRaise<EventArgs>((handler) => // Attach
            {
                menu.MouseLeave += handler;
            }, (handler) => // Detach
            {
                menu.MouseLeave -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseLeave += null, null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverload_RaisesMouseEnterEventWhenMouseEntersMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            // Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                menu.MouseEnter += handler;
            }, (handler) => // Detach
            {
                menu.MouseEnter -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseEnter += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Add_WhenInvoking3ParamOverload_RaisesMouseLeaveEventWhenMouseLeavesMenuArea()
        {
            // Arrange
            var mockLabel = new Mock<ILabel>();
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu", "test-item")).Returns(mockLabel.Object);

            var menu = CreateMenu();

            // Act
            menu.Add("test-menu", "test-item", Color.DarkOrange);

            // Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                menu.MouseLeave += handler;
            }, (handler) => // Detach
            {
                menu.MouseLeave -= handler;
            }, () =>
            {
                mockLabel.Raise(e => e.MouseLeave += null, EventArgs.Empty);
            });
        }

        [Fact]
        public void Update_WhenInvoked_UpdatesAllMenuItems()
        {
            // Arrange
            var menu = CreateMenu();
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupProperty(p => p.Name);
            mockLabelA.SetupProperty(p => p.Text);

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupGet(p => p.Name).Returns("menu-menu-B");
            mockLabelB.SetupGet(p => p.Text).Returns("menu-item-B");

            this.mockCtrlFactory.Setup(m => m.CreateLabel("menu-menu-A", "menu-item-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("menu-menu-B", "menu-item-B")).Returns(mockLabelB.Object);

            menu.Add("menu-menu-A", "menu-item-A");
            menu.Add("menu-menu-B", "menu-item-B");

            var gameTime = new GameTime(new TimeSpan(0, 0, 0, 0, 16), new TimeSpan(0, 0, 0, 0, 16));

            // Act
            menu.Update(gameTime);

            // Assert
            mockLabelA.Verify(m => m.Update(gameTime), Times.Once());
            mockLabelB.Verify(m => m.Update(gameTime), Times.Once());
        }

        [Fact]
        public void Update_WhenInvoked_ProperlySetsMenuItemLocations()
        {
            // Arrange
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupProperty(p => p.Name);
            mockLabelA.SetupProperty(p => p.Text);
            mockLabelA.SetupProperty(p => p.Location);
            mockLabelA.SetupGet(p => p.Height).Returns(25);
            mockLabelA.SetupGet(p => p.Bottom).Returns(225);

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupProperty(p => p.Name);
            mockLabelB.SetupProperty(p => p.Text);
            mockLabelB.SetupProperty(p => p.Left);
            mockLabelB.SetupProperty(p => p.Top);

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-A", "test-item-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-B", "test-item-B")).Returns(mockLabelB.Object);

            var menu = CreateMenu();
            menu.Location = new Vector2(100, 200);

            menu.Add("test-menu-A", "test-item-A");
            menu.Add("test-menu-B", "test-item-B");

            var gameTime = new GameTime(new TimeSpan(0, 0, 0, 0, 16), new TimeSpan(0, 0, 0, 0, 16));

            // Act
            menu.Update(gameTime);

            // Assert
            Assert.Equal(new Vector2(100, 200), menu.MenuItems[0].Location); // Label A
            Assert.Equal(100, menu.MenuItems[1].Left); // Label B
            Assert.Equal(230, menu.MenuItems[1].Top); // Label B
        }

        [Fact]
        public void Draw_WhenInvoked_DrawAllMenuItems()
        {
            // Arrange
            var menu = CreateMenu();
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupGet(p => p.Text).Returns("menu-item-A");

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupGet(p => p.Text).Returns("menu-item-B");

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-item-A", "menu-item-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-item-B", "menu-item-B")).Returns(mockLabelB.Object);

            menu.Add("test-item-A", "menu-item-A");
            menu.Add("test-item-B", "menu-item-B");

            var gameTime = new GameTime(new TimeSpan(0, 0, 0, 0, 16), new TimeSpan(0, 0, 0, 0, 16));

            // Act
            menu.Draw(gameTime);

            // Assert
            mockLabelA.Verify(m => m.Draw(gameTime), Times.Once());
            mockLabelB.Verify(m => m.Draw(gameTime), Times.Once());
        }

        [Fact]
        public void Dispose_WhenInvoked_DisposesAllMenuItems()
        {
            // Arrange
            var menu = CreateMenu();
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupGet(p => p.Text).Returns("menu-item-A");

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupGet(p => p.Text).Returns("menu-item-B");

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-item-A", "menu-item-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-item-B", "menu-item-B")).Returns(mockLabelB.Object);

            menu.Add("test-item-A", "menu-item-A");
            menu.Add("test-item-B", "menu-item-B");

            var gameTime = new GameTime(new TimeSpan(0, 0, 0, 0, 16), new TimeSpan(0, 0, 0, 0, 16));

            // Act
            menu.Dispose();
            menu.Dispose();

            // Assert
            mockLabelA.Verify(m => m.Dispose(), Times.Once());
            mockLabelB.Verify(m => m.Dispose(), Times.Once());
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void VerticalSpacing_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var menu = CreateMenu();

            // Act
            menu.VerticalSpacing = 10;
            var actual = menu.VerticalSpacing;

            // Assert
            Assert.Equal(10, actual);
        }

        [Fact]
        public void Width_WhenGettingValueWithNoLabels_ReturnsCorrectResult()
        {
            // Arrange
            var menu = CreateMenu();

            // Act
            var actual = menu.Width;

            // Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void Width_WhenGettingValueWith2Labels_ReturnsCorrectResult()
        {
            // Arrange
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupGet(p => p.Width).Returns(10);

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupGet(p => p.Width).Returns(20);

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-A", "test-menu-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-B", "test-menu-B")).Returns(mockLabelB.Object);

            var menu = CreateMenu();

            menu.Add("test-menu-A", "test-menu-A");
            menu.Add("test-menu-B", "test-menu-B");

            // Act
            var actual = menu.Width;

            // Assert
            Assert.Equal(20, actual);
        }

        [Fact]
        public void Height_WhenGettingValueWithNoLabels_ReturnsCorrectResult()
        {
            // Arrange
            var menu = CreateMenu();

            // Act
            var actual = menu.Height;

            // Assert
            Assert.Equal(0, actual);
        }

        [Fact]
        public void Height_WhenGettingValueWith2Labels_ReturnsCorrectResult()
        {
            // Arrange
            var mockLabelA = new Mock<ILabel>();
            mockLabelA.SetupGet(p => p.Height).Returns(10);

            var mockLabelB = new Mock<ILabel>();
            mockLabelB.SetupGet(p => p.Height).Returns(20);

            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-A", "test-menu-A")).Returns(mockLabelA.Object);
            this.mockCtrlFactory.Setup(m => m.CreateLabel("test-menu-B", "test-menu-B")).Returns(mockLabelB.Object);

            var menu = CreateMenu();

            menu.Add("test-menu-A", "test-menu-A");
            menu.Add("test-menu-B", "test-menu-B");

            // Act
            var actual = menu.Height;

            // Assert
            Assert.Equal(35, actual);
        }
        #endregion

        /// <summary>
        /// Creates a new instance of <see cref="Menu"/> for testing.
        /// </summary>
        /// <returns>A menu instance.</returns>
        private Menu CreateMenu()
        {
            return new Menu(this.mockCtrlFactory.Object);
        }
    }
}
