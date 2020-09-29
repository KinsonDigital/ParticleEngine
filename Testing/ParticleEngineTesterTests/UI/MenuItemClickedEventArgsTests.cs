// <copyright file="MenuItemClickedEventArgsTests.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.UI
{
    using ParticleEngineTester.UI;
    using Xunit;

    /// <summary>
    /// Tests the <see cref="MenuItemClickedEventArgs"/> class.
    /// </summary>
    public class MenuItemClickedEventArgsTests
    {
        #region Constructor Tests
        [Fact]
        public void Ctor_WhenInvoked_SetsItemNameProperty()
        {
            // Arrange
            var itemName = "test-item";

            // Act
            var eventArgs = new MenuItemClickedEventArgs(itemName);
            var actual = eventArgs.MenuItemName;

            // Assert
            Assert.Equal("test-item", actual);
        }
        #endregion
    }
}
