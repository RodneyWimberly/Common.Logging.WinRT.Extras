using Common.Logging.Simple;
using System;
using System.IO;
using System.Text;

namespace Common.Logging.WinRT.Extras
{
    public class SimpleFileLogger : AbstractSimpleLogger
    {
        private readonly object _syncRoot;
        public bool WriteToDebug { get; set; }

        public string LogFileName { get; set; }

        public SimpleFileLogger(string logName, LogLevel logLevel, bool showLevel, bool showDateTime,
            bool showLogName, string dateTimeFormat, string logFileName, bool writeToDebug)
            : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
            _syncRoot = new object();
            LogFileName = logFileName;
            WriteToDebug = writeToDebug;
        }

        protected override void WriteInternal(LogLevel level, object message, Exception exception)
        {
            if (string.IsNullOrWhiteSpace(LogFileName))
                throw new ConfigurationException("LogFileName is empty and therefore the logger can't write any information");

            StringBuilder logEvent = new StringBuilder();
            FormatOutput(logEvent, level, message, exception);

            lock (_syncRoot)
            {
                string directory = Path.GetDirectoryName(LogFileName);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                File.WriteAllText(LogFileName, logEvent.ToString());
                if(WriteToDebug)
                    global::System.Diagnostics.Debug.WriteLine(logEvent);
            }
        }

        public override bool IsTraceEnabled { get; }
        public override bool IsDebugEnabled { get; }
        public override bool IsErrorEnabled { get; }
        public override bool IsFatalEnabled { get; }
        public override bool IsInfoEnabled { get; }
        public override bool IsWarnEnabled { get; }
    }
}
