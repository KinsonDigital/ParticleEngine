// <copyright file="AssertHelpers.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTesterTests.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using Xunit;
    using Xunit.Sdk;
    using static Xunit.Assert;

    /// <summary>
    /// Provides helper methods for the <see cref="XUnit"/>'s <see cref="Assert"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class AssertHelpers
    {
        /// <summary>
        /// Verifies that the exact exception is thrown (and not a derived exception type) and that
        /// the exception message matches the given <paramref name="expectedMessage"/>.
        /// </summary>
        /// <typeparam name="T">The type of exception that the test is verifying.</typeparam>
        /// <param name="testCode">The code that will be throwing the expected exception.</param>
        /// <param name="expectedMessage">The expected message of the exception.</param>
        public static void ThrowsWithMessage<T>(Action testCode, string expectedMessage)
            where T : Exception
        {
            Equal(expectedMessage, Throws<T>(testCode).Message);
        }

        public static void DoesNotThrow<T>(Action action)
            where T : Exception
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action), "The parameter must not be null");
            }

            try
            {
                action();
            }
            catch (T)
            {
                True(false, $"Expected the exception {typeof(T).Name} to not be thrown.");
            }
        }

        public static void DoesNotThrowNullReference(Action action)
        {
            if (action is null)
            {
                throw new ArgumentNullException(nameof(action), "The parameter must not be null");
            }

            try
            {
                action();
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                if (ex.GetType() == typeof(NullReferenceException))
                {
                    True(false, $"Expected not to raise a {nameof(NullReferenceException)} exception.");
                }
                else
                {
                    True(true);
                }
            }
        }

        public static void IsNullOrZeroField(object fieldContainer, string name)
        {
            try
            {
                var result = fieldContainer.IsNullOrZeroField(name);

                True(true);
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
            {
                True(false, ex.Message);
            }
        }

        /// <summary>
        /// Asserts that all of the individual <paramref name="expectedItems"/> and <paramref name="actualItems"/>
        /// are equal on a per item basis.
        /// </summary>
        /// <typeparam name="T">The type of item in the lists.</typeparam>
        /// <param name="expectedItems">The list of expected items.</param>
        /// <param name="actualItems">The list of actual items to compare to the expected items.</param>
        /// <remarks>
        ///     Will fail assertion when one item is null and the other is not.
        ///     Will fail assertion when the total number of <paramref name="expectedItems"/> does not match the total number of <paramref name="actualItems"/>.
        /// </remarks>
        public static void ItemsEqual<T>(IEnumerable<T> expectedItems, IEnumerable<T> actualItems)
            where T : class
        {
            if (expectedItems is null && !(actualItems is null))
            {
                True(false, $"Both lists must be null or not null to be equal.\nThe '{nameof(expectedItems)}' is null and the '{nameof(actualItems)}' is not null.");
            }

            if (!(expectedItems is null) && actualItems is null)
            {
                True(false, $"Both lists must be null or not null to be equal.\nThe '{nameof(expectedItems)}' is not null and the '{nameof(actualItems)}' is null.");
            }

            if (expectedItems.Count() != actualItems.Count())
            {
                True(false, $"The quantity of items for '{nameof(expectedItems)}' and '{nameof(actualItems)}' do not match.");
            }

            var expectedArrayItems = expectedItems.ToArray();
            var actualArrayItems = actualItems.ToArray();

            for (var i = 0; i < expectedArrayItems.Length; i++)
            {
                if ((expectedArrayItems[i] is null) && !(actualArrayItems[i] is null))
                {
                    True(false, $"Both the expected and actual item must both be null or not null to be equal.\n\nThe expected item at index '{i}' is null and the actual item at index '{i}' is not null.");
                }

                if (!(expectedArrayItems[i] is null) && (actualArrayItems[i] is null))
                {
                    True(false, $"Both the expected and actual item must both be null or not null to be equal.\n\nThe expected item at index '{i}' is not null and the actual item at index '{i}' is null.");
                }

                if (expectedArrayItems[i] != actualArrayItems[i])
                {
                    True(false, $"The expected and actual item at index '{i}' are not equal.");
                }
            }

            True(true);
        }

        /// <summary>
        /// Asserts that all of the given <paramref name="items"/> are true which is dictacted
        /// by the given <paramref name="arePredicate"/> predicate.
        /// </summary>
        /// <typeparam name="T">The type of item in the list of items.</typeparam>
        /// <param name="items">The list of items to assert.</param>
        /// <param name="arePredicate">Fails the assertion when returning false.</param>
        public static void AllItemsAre<T>(IEnumerable<T> items, Predicate<T> arePredicate)
        {
            if (arePredicate is null)
            {
                throw new ArgumentNullException(nameof(arePredicate), "The parameter must not be null.");
            }

            var itemsToCheck = items.ToArray();

            for (var i = 0; i < itemsToCheck.Length; i++)
            {
                if (arePredicate(itemsToCheck[i]))
                {
                    continue;
                }

                True(false, $"The item '{itemsToCheck[i]}' at index '{i}' returned false with the '{nameof(arePredicate)}'");
            }
        }

        /// <summary>
        /// Asserts that an event should not of been raised.
        /// </summary>
        /// <typeparam name="T">The type of the event arguments to expect if the event was raised.</typeparam>
        /// <param name="attach">Code to attach the event handler.</param>
        /// <param name="detach">Code to detach the event handler.</param>
        /// <param name="testCode">A delegate to the code to be tested.</param>
        public static void DoesNotRaise<T>(Action<EventHandler<T>> attach, Action<EventHandler<T>> detach, Action testCode)
            where T : EventArgs
        {
            RaisedEvent<T> raisedEvent = RaisesInternal(attach, detach, testCode);

            if (raisedEvent == null || !raisedEvent.Arguments.GetType().Equals(typeof(T)))
            {
                True(true);
                return;
            }

            True(false, "The event was raised and should not of been.");
        }

        /// <summary>
        /// Verifies that an event with the exact or a derived event args is raised.
        /// </summary>
        /// <typeparam name="T">The type of the event arguments to expect if the event was raised.</typeparam>
        /// <param name="attach">Code to attach the event handler.</param>
        /// <param name="detach">Code to detach the event handler.</param>
        /// <param name="testCode">A delegate to the code to be tested.</param>
        private static RaisedEvent<T> RaisesInternal<T>(Action<EventHandler<T>> attach, Action<EventHandler<T>> detach, Action testCode)
            where T : EventArgs
        {
            RaisedEvent<T> raisedEvent = null;
            void Obj(object s, T args)
            {
                raisedEvent = new RaisedEvent<T>(s, args);
            }

            attach(Obj);
            testCode();
            detach(Obj);
            return raisedEvent;
        }
    }
}
