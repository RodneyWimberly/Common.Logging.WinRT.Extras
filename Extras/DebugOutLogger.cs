using Common.Logging.Simple;
using System;
using System.Text;

namespace Common.Logging.WinRT.Extras
{
    public class DebugOutLogger : AbstractSimpleLogger
    {
        public DebugOutLogger(string logName, LogLevel logLevel, bool showLevel, bool showDateTime, bool showLogName, string dateTimeFormat)
          : base(logName, logLevel, showLevel, showDateTime, showLogName, dateTimeFormat)
        {
        }

        protected override void WriteInternal(LogLevel level, object message, Exception e)
        {
            StringBuilder logEvent = new StringBuilder();
            FormatOutput(logEvent, level, message, e);
            global::System.Diagnostics.Debug.WriteLine(logEvent);
        }
    }
}
