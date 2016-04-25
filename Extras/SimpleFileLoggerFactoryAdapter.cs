using Common.Logging.Configuration;
using Common.Logging.Simple;

namespace Common.Logging.WinRT.Extras
{
    public class SimpleFileLoggerFactoryAdapter : AbstractSimpleLoggerFactoryAdapter
    {
        private readonly string _logFileName;
        private readonly bool _writeToDebug;
        public SimpleFileLoggerFactoryAdapter(NameValueCollection properties) : base(properties)
        {
        }

        public SimpleFileLoggerFactoryAdapter(LogLevel level, bool showDateTime, bool showLogName, bool showLevel, string dateTimeFormat, string logFileName, bool writeToDebug)
            : base(level, showDateTime, showLogName, showLevel, dateTimeFormat)
        {
            _logFileName = logFileName;
            _writeToDebug = writeToDebug;
        }

        protected override ILog CreateLogger(string name, LogLevel level, bool showLevel, bool showDateTime, bool showLogName,
            string dateTimeFormat)
        {
            return new SimpleFileLogger(name, level, showLevel, showDateTime, showLogName, dateTimeFormat, _logFileName, _writeToDebug);
        }
    }
}
