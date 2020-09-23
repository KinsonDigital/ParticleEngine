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
    using ParticleEngineTester.UI;
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
            control.OnClick(control, new ClickedEventArgs("test-control"));

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
            control.Location = new Vector2(0, 456);

            // Act
            control.Left = 123;
            var actual = control.Left;
            var actualLocation = control.Location;

            // Assert
            Assert.Equal(123, actual);
            Assert.Equal(new Vector2(123, 456), actualLocation);
        }

        [Fact]
        public void Right_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();
            control.Location = new Vector2(0, 456);

            // Act
            control.Right = 200;
            var actualRight = control.Right;
            var actualLocation = control.Location;

            // Assert
            Assert.Equal(200, actualRight);
            Assert.Equal(new Vector2(100, 456), actualLocation);
        }

        [Fact]
        public void Top_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();
            control.Location = new Vector2(123, 0);

            // Act
            control.Top = 456;
            var actualLeft = control.Top;
            var actualLocation = control.Location;

            // Assert
            Assert.Equal(456, actualLeft);
            Assert.Equal(new Vector2(123, 456), actualLocation);
        }

        [Fact]
        public void Bottom_WhenSettingValue_ReturnsCorrectResult()
        {
            // Arrange
            var control = CreateControl();
            control.Location = new Vector2(123, 0);

            // Act
            control.Bottom = 456;
            var actualBottom = control.Bottom;
            var actualLocation = control.Location;

            // Assert
            Assert.Equal(456, actualBottom);
            Assert.Equal(new Vector2(123, 356), actualLocation);
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
            Assert.Raises<ClickedEventArgs>((handler) => // Attach
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
        public void Update_WithMouseEnteringOverControl_InvokesMouseEnterEvent()
        {
            // Arrange
            var getStateInvokeCount = 0;

            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                getStateInvokeCount += 1;

                if (getStateInvokeCount == 1)
                {
                    return new MouseState(0, 0, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
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
                control.MouseEnter += handler;
            }, (handler) => // Detach
            {
                control.MouseEnter -= handler;
            }, () => // Test Code
            {
                // NOTE: Need to run update at least 2 times to update the state of the mouse
                control.Update(new GameTime());
                control.Update(new GameTime());
            });

            this.mockMouse.Verify(m => m.GetState(), Times.Exactly(2));
        }

        [Fact]
        public void Update_WithMouseLeavingOverControl_InvokesMouseLeaveEvent()
        {
            // Arrange
            var getStateInvokeCount = 0;

            this.mockMouse.Setup(m => m.GetState()).Returns(() =>
            {
                getStateInvokeCount += 1;

                if (getStateInvokeCount == 1)
                {
                    return new MouseState(75, 75, 0, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
                else
                {
                    return new MouseState(0, 0, 0, ButtonState.Pressed, ButtonState.Released, ButtonState.Released, ButtonState.Released, ButtonState.Released);
                }
            });

            var control = CreateControl();
            control.Location = new Vector2(50, 50);

            // Act & Assert
            Assert.Raises<EventArgs>((handler) => // Attach
            {
                control.MouseLeave += handler;
            }, (handler) => // Detach
            {
                control.MouseLeave -= handler;
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
