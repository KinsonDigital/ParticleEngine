// <copyright file="Program.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable SA1200 // Using directives should be placed correctly
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Serilog;
using Serilog.Core;
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
        private static readonly string AppPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\";
        private static readonly string LogPath = $@"{AppPath}Logs\";

        /// <summary>
        /// Gets the error logger used to log errors to a file.
        /// </summary>
        public static Logger? ErrorLogger { get; private set; }

        [STAThread]
        private static void Main()
        {
            SetupLogDirectory();

            var shortDateTime = DateTime.Now.ToShortDateString().Replace('/', '-');

            ErrorLogger = new LoggerConfiguration()
                .WriteTo.File(
                    $"{LogPath}error-logs-{shortDateTime}.txt",
                    outputTemplate: "\n{Timestamp:HH:mm:ss} | {Message}\n\t{Exception}")
                .CreateLogger();

            var main = new Main();
            main.Run();
        }

        /// <summary>
        /// Sets up the log directory where logs will be saved.
        /// </summary>
        private static void SetupLogDirectory()
        {
            if (Directory.Exists(LogPath))
            {
                return;
            }

            Directory.CreateDirectory(LogPath);
        }
    }
}
