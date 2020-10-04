// <copyright file="ErrorLogger.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;
    using System.IO;
    using System.Reflection;
    using Serilog;
    using Serilog.Core;

    /// <summary>
    /// Logs errors to a log file.
    /// </summary>
    public class ErrorLogger : ILogger
    {
        private static readonly string AppPath = $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\";
        private static readonly string LogPath = $@"{AppPath}Logs\";
        private Logger? errorLogger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorLogger"/> class.
        /// </summary>
        public ErrorLogger()
        {
            if (Directory.Exists(LogPath))
            {
                return;
            }

            Directory.CreateDirectory(LogPath);
        }

        /// <inheritdoc/>
        public void Error(Exception exception, string message)
        {
            if (this.errorLogger is null)
            {
                var shortDateTime = DateTime.Now.ToShortDateString().Replace('/', '-');
                this.errorLogger = new LoggerConfiguration()
                    .WriteTo.File(
                        $"{LogPath}error-logs-{shortDateTime}.txt",
                        outputTemplate: "\n{Timestamp:HH:mm:ss} | {Message}\n\t{Exception}")
                    .CreateLogger();
            }

            this.errorLogger?.Error(exception, message);
        }
    }
}
