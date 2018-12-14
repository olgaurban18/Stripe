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

        public void Debug(string message)
        {
            if (Level <= LogLevel.Debug)
            {
                var options = new LoggerOptions(LogType.Debug, message);
                Task.Run(() => _writer.WriteToFile(Path, options));
            }
        }

        public void Info(string message)
        {
            if (Level <= LogLevel.Info)
            {
                var options = new LoggerOptions(LogType.Info, message);
                Task.Run(() => _writer.WriteToFile(Path, options));
            }
        }

        public void Error(string message)
        {
            if (Level <= LogLevel.Error)
            {
                var options = new LoggerOptions(LogType.Error, message);
                Task.Run(() => _writer.WriteToFile(Path, options));
            }
        }
    }
}