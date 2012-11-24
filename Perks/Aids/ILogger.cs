using System;

namespace Perks.Aids
{
    /// <summary>
    /// Represents any kind of logger.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Writes a tracing message to the log.
        /// </summary>
        /// <param name="message">Tracing message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Trace(string message, params object[] args);

        /// <summary>
        /// Writes a debug message to the log.
        /// </summary>
        /// <param name="message">Debug message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Debug(string message, params object[] args);

        /// <summary>
        /// Writes an informational message to the log.
        /// </summary>
        /// <param name="message">Informational message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Info(string message, params object[] args);

        /// <summary>
        /// Writes a warning message to the log.
        /// </summary>
        /// <param name="message">Warning message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Warn(string message, params object[] args);

        /// <summary>
        /// Writes a warning message to the log.
        /// </summary>
        /// <param name="message">Warning message.</param>
        /// <param name="exception">The exception corresponding to the message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Warn(string message, Exception exception = null, params object[] args);

        /// <summary>
        /// Writes an error message to the log.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <param name="exception">The exception corresponding to the message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Error(string message, Exception exception = null, params object[] args);

        /// <summary>
        /// Writes a fatal error message to the log.
        /// </summary>
        /// <param name="message">Fatal error message.</param>
        /// <param name="exception">The exception corresponding to the message.</param>
        /// <param name="args">Arguments for the message.</param>
        void Fatal(string message, Exception exception = null, params object[] args);
    }
}