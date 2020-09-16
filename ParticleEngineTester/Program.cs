// <copyright file="Program.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>


#pragma warning disable SA1200 // Using directives should be placed correctly
using System;
using System.Runtime.CompilerServices;
#pragma warning restore SA1200 // Using directives should be placed correctly

[assembly: InternalsVisibleTo(assemblyName: "ParticleEngineTesterTests", AllInternalsVisible = true)]

namespace ParticleEngineTester
{
    /// <summary>
    /// Runs the application.
    /// </summary>
    public static class Program
    {
        [STAThread]
        private static void Main()
        {
            var main = new Main();

            main.Run();
        }
    }
}
