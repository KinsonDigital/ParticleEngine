using System;
using Xunit;

namespace KDParticleEngineTests.XUnitHelpers
{
    public static class AssertHelpers
    {
        /// <summary>
        /// Asserts the the exception thrown in the given <paramref name="testCode"/> action
        /// will have an exception message that matches the <paramref name="expectedMessage"/>.
        /// </summary>
        /// <param name="testCode">The code that throws the expected exception.</param>
        /// <param name="expectedMessage">The expected exception message.</param>
        public static void ExceptionHasMessage(Action testCode, string expectedMessage)
        {
            try
            {
                testCode();
            }
            catch (Exception ex)
            {
                Assert.Equal(expectedMessage, ex.Message);
            }
        }
    }
}
