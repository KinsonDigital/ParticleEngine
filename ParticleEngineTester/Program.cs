// <copyright file="Program.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable SA1200 // Using directives should be placed correctly
using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
#pragma warning restore SA1200 // Using directives should be placed correctly

[assembly: InternalsVisibleTo(assemblyName: "ParticleEngineTesterTests", AllInternalsVisible = true)]

namespace ParticleEngineTester
{
    /// <summary>
    /// Runs the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class Program
    {
        /// <summary>
        /// Gets the error logger used to log errors to a file.
        /// </summary>
        public static ILogger Logger { get; private set; } = new ErrorLogger();

        [STAThread]
        private static void Main()
        {
            var main = new Main();
            main.Run();
        }
    }
}
