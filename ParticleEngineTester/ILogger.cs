// <copyright file="ILogger.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

namespace ParticleEngineTester
{
    using System;

    /// <summary>
    /// Logs errors to a log file.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Write an error log event to a log file.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="message">The log message to log.</param>
        void Error(Exception exception, string message);
    }
}
