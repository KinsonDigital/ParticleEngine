// <copyright file="LabelTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.UI
{
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTester.UI;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Label"/> class.
    /// </summary>
    public class LabelTests
    {
        private readonly Mock<IRenderer> mockRenderer;
        private readonly Mock<IContentLoader> mockContentLoader;
        private readonly Mock<IMouse> mockMouse;
        private readonly Mock<IFont> mockStandardFont;
        private readonly Mock<IFont> mockBoldFont;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabelTests"/> class.
        /// </summary>
        public LabelTests()
        {
            this.mockRenderer = new Mock<IRenderer>();

            this.mockStandardFont = new Mock<IFont>();
            this.mockStandardFont.SetupGet(p => p.Width).Returns(100);
            this.mockStandardFont.SetupGet(p => p.Height).Returns(25);
            this.mockStandardFont.SetupProperty(p => p.Text);

            this.mockBoldFont = new Mock<IFont>();
            this.mockBoldFont.SetupGet(p => p.Width).Returns(100);
            this.mockBoldFont.SetupGet(p => p.Height).Returns(25);
            this.mockBoldFont.SetupProperty(p => p.Text);

            this.mockContentLoader = new Mock<IContentLoader>();
            this.mockContentLoader.Setup(m => m.LoadFont("Fonts/standard-font")).Returns(this.mockStandardFont.Object);
            this.mockContentLoader.Setup(m => m.LoadFont("Fonts/bold-font")).Returns(this.mockBoldFont.Object);

            this.mockMouse = new Mock<IMouse>();
        }

        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultEnabledResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            var actual = label.Enabled;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultUpdateOrderResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            var actual = label.UpdateOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultDrawOrderResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            var actual = label.DrawOrder;

            // Assert
            Assert.Equal(1, actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultVisibleResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            var actual = label.Visible;

            // Assert
            Assert.True(actual);
        }

        [Fact]
        public void Ctor_WhenInvoked_ReturnsCorrectDefaultColorResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            var actual = label.Forecolor;

            // Assert
            Assert.Equal(Color.Black, actual);
        }
        #endregion

        #region Prop Tests
        [Fact]
        public void Text_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var label = CreateLabel();

            // Act
            label.Text = "test-value";
            var actual = label.Text;

            // Assert
            Assert.Equal("test-value", actual);
        }
        #endregion

        #region Method Tests
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
                    return new MouseState(55, 55, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
                else
                {
                    return new MouseState(55, 55, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
            });

            var label = CreateLabel();
            label.Location = new Vector2(50, 50);

            // Act & Assert
            Assert.Raises<ClickedEventArgs>((handler) => // Attach
            {
                label.Click += handler;
            }, (handler) => // Detach
            {
                label.Click -= handler;
            }, () => // Test Code
            {
                // NOTE: Need to run update at least 2 times to update the state of the mouse
                label.Update(new GameTime());
                label.Update(new GameTime());
            });

            this.mockMouse.Verify(m => m.GetState(), Times.Exactly(2), $"The base '{nameof(Control.Update)}()' method was not invoked.");
        }

        [Fact]
        public void Draw_WhenInvoked_RendersLabel()
        {
            // Arrange
            var label = CreateLabel();
            label.Text = "label-text";
            label.Location = new Vector2(11, 22);
            label.Forecolor = Color.Purple;

            // Act
            label.Draw(new GameTime());

            // Assert
            this.mockRenderer.Verify(m => m.DrawText(this.mockStandardFont.Object, "label-text", new Vector2(11, 22), Color.Purple));
        }
        #endregion

        private Label CreateLabel()
        {
            return new Label(
                this.mockRenderer.Object,
                this.mockContentLoader.Object,
                this.mockMouse.Object);
        }
    }
}
