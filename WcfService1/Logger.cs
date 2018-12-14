using System;
using System.Threading.Tasks;
using static WcfService1.Constants;

namespace WcfService1
{
    public class Logger : ILogger
    {
        private readonly IWriter _writer;
        private static string Path;
        private static LogLevel Level;

        public Logger(IWriter writer)
        {
            _writer = writer;
            Path = AppSettingsHelper.GetSettingValue(LoggerPath);

            var value = AppSettingsHelper.GetSettingValue(LoggerLevel);
            Level = (LogLevel)Enum.Parse(typeof(LogLevel), value, true);
        }

        public Logger(IWriter writer, string path, LogLevel level)
        {
            _writer = writer;
            Path = path;
            Level = level;
        }

        public void Log(LoggerOptions options)
        {
            if (Level <= options.Level)
            {
                Task.Run(() => _writer.WriteToFile(Path, options.Message));
            }
        }
    }
}