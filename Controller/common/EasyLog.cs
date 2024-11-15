namespace EZip.Controller
{
    using System;
    using System.IO;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// very simple logger that writes to a file
    /// if is debug mode, it also writes to the console
    /// </summary>
    public class EasyLogger
    {
        private readonly string _logFilePath;
        private readonly string _loggerName;
        private readonly object _lock = new();

        /// <summary>
        ///  
        /// </summary>
        /// <param name="logFilePath"></param>
        /// <param name="loggerName"></param>
        public EasyLogger(string logFilePath, string loggerName)
        {
            _logFilePath = logFilePath;
            _loggerName = loggerName;

            var logDirectory = Path.GetDirectoryName(logFilePath);
            if (!Directory.Exists(logDirectory))
            {
                logDirectory = "EZip.log";
                Directory.CreateDirectory(logDirectory);
            }
        }

        public void LogInfo(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log("info", message, filePath, lineNumber);
        }

        public void LogWarning(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            Log("warning", message, filePath, lineNumber);
        }

        public void LogError(string message, Exception? ex = null, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0)
        {
            var errorMessage = ex == null ? message : $"{message} - Exception: {ex.Message}";
            Log("error", errorMessage, filePath, lineNumber);
        }

        private void Log(string logLevel, string message, string filePath, int lineNumber)
        {
            var fileName = Path.GetFileName(filePath);

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            var logMessage = $"[{timestamp}] [{_loggerName}] [{logLevel}] [{fileName}:{lineNumber}] {message}";

            // just to make sure that only one thread writes to the file at a time
            lock (_lock)
            {
                using (var writer = new StreamWriter(_logFilePath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }

            Console.WriteLine(logMessage);
        }
    }

}
