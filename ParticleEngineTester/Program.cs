// <copyright file="Program.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;

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
