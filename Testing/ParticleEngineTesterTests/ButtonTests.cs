// <copyright file="ButtonTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests
{
    using System;
    using System.Linq;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Graphics;
    using Microsoft.Xna.Framework.Input;
    using Moq;
    using ParticleEngineTester;
    using Xunit;

    public class ButtonTests
    {
        private readonly Mock<ISpriteRenderer> mockRenderer;
        private readonly Mock<IContentLoader> mockContentLoader;
        private readonly Mock<IMouse> mockMouse;
        private readonly Mock<ITexture> mockTexture;
        private Color[] normalPixels;

        public ButtonTests()
        {
            this.mockRenderer = new Mock<ISpriteRenderer>();

            this.mockTexture = new Mock<ITexture>();
            this.mockTexture.SetupGet(p => p.Width).Returns(50);
            this.mockTexture.SetupGet(p => p.Height).Returns(50);
            this.mockTexture.Setup(m => m.GetData(It.IsAny<Color[]>())).Callback<Color[]>((data) =>
            {
                this.normalPixels = data;

                for (var i = 0; i < this.normalPixels.Length; i++)
                {
                    this.normalPixels[i] = new Color(10, 10, 10, 255);
                }
            });

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.Load("normal")).Returns(this.mockTexture.Object);

            this.mockMouse = new Mock<IMouse>();
            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                return new MouseState(50, 55, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            });
        }

        #region Prop Tests
        [Fact]
        public void UpdateOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var button = CreateButton();

            // Act
            button.UpdateOrder = 1234;
            var actual = button.UpdateOrder;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void DrawOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var button = CreateButton();

            // Act
            button.DrawOrder = 1234;
            var actual = button.DrawOrder;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void Visible_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var button = CreateButton();

            // Act
            button.Visible = false;
            var actual = button.Visible;

            // Assert
            Assert.False(actual);
        }
        #endregion

        #region Method Tests
        [Fact]
        public void Update_WithMouseNotOverButton_RendersNormalTexture()
        {
            // Arrange
            var colorData = new Color[0];
            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                return new MouseState(5, 10, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            });

            this.mockTexture.Setup(m => m.SetData(It.IsAny<Color[]>()))
                .Callback<Color[]>((data) =>
                {
                    colorData = data;
                });
            var button = CreateButton();

            button.Location = new Vector2(50, 50);

            // Act
            button.Update(new GameTime());
            button.Draw(new GameTime());

            // Assert
            this.mockRenderer.Verify(m => m.Draw(this.mockTexture.Object, new Vector2(50, 50), It.IsAny<Color>()), Times.Once());
            this.mockTexture.Verify(m => m.SetData(It.IsAny<Color[]>()), Times.Once());
            Assert.Equal(2500, colorData.Length);
            Assert.True(colorData.All(clr => clr.R == 10 && clr.G == 10 && clr.B == 10 && clr.A == 255));
        }

        [Fact]
        public void Update_WithMouseIsOverButton_RendersMouseHoverTexture()
        {
            // Arrange
            var colorData = new Color[0];

            this.mockTexture.Setup(m => m.SetData(It.IsAny<Color[]>()))
                .Callback<Color[]>((data) =>
                {
                    colorData = data;
                });

            var button = CreateButton();
            button.Location = new Vector2(50, 50);

            // Act
            button.Update(new GameTime());
            button.Draw(new GameTime());

            // Assert
            this.mockRenderer.Verify(m => m.Draw(this.mockTexture.Object, new Vector2(50, 50), It.IsAny<Color>()), Times.Once());
            Assert.Equal(2500, colorData.Length);
            Assert.True(colorData.All(clr => clr.R == 10 && clr.G == 11 && clr.B == 10 && clr.A == 255));
        }

        [Fact]
        public void Update_WithMouseDown_RendersMouseDownTexture()
        {
            // Arrange
            var colorData = new Color[0];

            this.mockTexture.Setup(m => m.SetData(It.IsAny<Color[]>()))
                .Callback<Color[]>((data) =>
                {
                    colorData = data;
                });

            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                return new MouseState(75, 75, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
            });

            var button = CreateButton();
            button.Location = new Vector2(50, 50);

            // Act
            button.Update(new GameTime());
            button.Draw(new GameTime());

            // Assert
            this.mockRenderer.Verify(m => m.Draw(this.mockTexture.Object, new Vector2(50, 50), It.IsAny<Color>()), Times.Once());
            Assert.Equal(2500, colorData.Length);
            Assert.True(colorData.All(clr => clr.R == 10 && clr.G == 15 && clr.B == 10 && clr.A == 255));
        }

        [Fact]
        public void Update_WithMouseClick_InvokesClickEvent()
        {
            // Arrange
            var getStateInvokeCount = 0;

            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                getStateInvokeCount += 1;

                if (getStateInvokeCount == 1)
                {
                    return new MouseState(75, 75, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
                else
                {
                    return new MouseState(75, 75, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
            });

            var button = CreateButton();
            button.Location = new Vector2(50, 50);

            // Act & Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                button.Click += handler;
            }, (handler) => // Detach
            {
                button.Click -= handler;
            }, () => // Test Code
            {
                button.Update(new GameTime());
                button.Update(new GameTime());
            });
        }

        [Fact]
        public void Update_WhileDisabled_RendersDisabledTexture()
        {
            // Arrange
            var colorData = new Color[0];

            this.mockTexture.Setup(m => m.SetData(It.IsAny<Color[]>()))
                .Callback<Color[]>((data) =>
                {
                    colorData = data;
                });

            var button = CreateButton();
            button.Enabled = false;
            button.Location = new Vector2(50, 50);

            // Act
            button.Update(new GameTime());
            button.Draw(new GameTime());

            // Assert
            this.mockRenderer.Verify(m => m.Draw(this.mockTexture.Object, new Vector2(50, 50), It.IsAny<Color>()), Times.Once());
            Assert.Equal(2500, colorData.Length);
            Assert.True(colorData.All(clr => clr.R == 17 && clr.G == 17 && clr.B == 17 && clr.A == 255),
                $"Current Color: R:{colorData[0].R},G:{colorData[0].G},B:{colorData[0].B},A:{colorData[0].A}");
        }
        #endregion

        /// <summary>
        /// Creates a button to simply unit tests.
        /// </summary>
        /// <returns>A <see cref="Button"/> instance.</returns>
        private Button CreateButton()
        {
            return new Button(this.mockRenderer.Object,
                              this.mockContentLoader.Object,
                              this.mockMouse.Object,
                              "normal");
        }
    }
}
