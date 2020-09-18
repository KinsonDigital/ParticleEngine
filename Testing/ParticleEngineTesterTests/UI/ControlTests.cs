// <copyright file="ControlTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.UI
{
    using System;
    using Microsoft.Xna.Framework;
    using Microsoft.Xna.Framework.Input;
    using Moq;
    using ParticleEngineTester;
    using ParticleEngineTesterTests.Fakes;
    using ParticleEngineTesterTests.Helpers;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="Control"/> class.
    /// </summary>
    public class ControlTests
    {
        private readonly Mock<IMouse> mockMouse;

        /// <summary>
        /// Initializes a new instance of the <see cref="ControlTests"/> class.
        /// </summary>
        public ControlTests() => this.mockMouse = new Mock<IMouse>();

        #region Prop Tests
        [Fact]
        public void UpdateOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.UpdateOrder = 1234;
            var actual = control.UpdateOrder;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void DrawOrder_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();
            var hasInvoked = false;

            control.Click += (sender, e) =>
            {
                hasInvoked = true; // This should not be invoked for the purpose of the test
            };

            // Act
            control.Dispose();
            control.Dispose();
            control.OnClick();

            // Assert
            Assert.False(hasInvoked);
        }

        [Fact]
        public void Visible_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.Visible = false;
            var actual = control.Visible;

            // Assert
            Assert.False(actual);
        }

        [Fact]
        public void Width_WhenGettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            var actual = control.Width;

            // Assert
            Assert.Equal(100, actual);
        }

        [Fact]
        public void Height_WhenGettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            var actual = control.Height;

            // Assert
            Assert.Equal(100, actual);
        }

        [Fact]
        public void Left_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.Left = 1234;
            var actual = control.Left;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void Right_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.Right = 1000;
            var actual = control.Right;

            // Assert
            Assert.Equal(1000, actual);
            Assert.Equal(900, control.Left);
        }

        [Fact]
        public void Top_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.Top = 1234;
            var actual = control.Top;

            // Assert
            Assert.Equal(1234, actual);
        }

        [Fact]
        public void Bottom_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();

            // Act
            control.Bottom = 1000;
            var actual = control.Bottom;

            // Assert
            Assert.Equal(1000, actual);
            Assert.Equal(900, control.Top);
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
                    return new MouseState(75, 75, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
                else
                {
                    return new MouseState(75, 75, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
            });

            var control = CreateControl();
            control.Location = new Vector2(50, 50);

            // Act & Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                control.Click += handler;
            }, (handler) => // Detach
            {
                control.Click -= handler;
            }, () => // Test Code
            {
                // NOTE: Need to run update at least 2 times to update the state of the mouse
                control.Update(new GameTime());
                control.Update(new GameTime());
            });

            this.mockMouse.Verify(m => m.GetState(), Times.Exactly(2));
        }

        [Fact]
        public void Update_WithMouseClickAndNoEventSubscription_DoesNotNullReferenceException()
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

            var control = CreateControl();

            control.Location = new Vector2(50, 50);

            // Act & Assert
            // NOTE: Need to run update at least 2 times to update the state of the mouse
            AssertHelpers.DoesNotThrowNullReference(() =>
            {
                control.Update(new GameTime());
                control.Update(new GameTime());
            });
        }
        #endregion

        /// <summary>
        /// Creates a button to simply unit tests.
        /// </summary>
        /// <returns>A <see cref="Button"/> instance.</returns>
        private FakeControl CreateControl()
        {
            return new FakeControl(this.mockMouse.Object);
        }
    }
}
