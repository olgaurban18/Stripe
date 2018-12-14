using System.Threading.Tasks;

namespace WcfService1
{
    public class Logger: ILogger
    {
        private readonly IWriter _writer;
        private static string Path;

        public Logger(IWriter writer)
        {
            _writer = writer;
            Path =  AppSettingsHelper.GetSettingValue(Constants.LoggerPath);
        }

        public Logger(IWriter writer, string path)
        {
            _writer = writer;
            Path = path;
        }

        public void Debug(string message)
        {
            var options = new LoggerOptions(Constants.LogLevel.Debug, message);
            Task.Run(() => _writer.WriteToFile(Path, options));
        }

        public void Info(string message)
        {
            var options = new LoggerOptions(Constants.LogLevel.Info, message);
            Task.Run(() => _writer.WriteToFile(Path, options));
        }

        public void Error(string message)
        {
            var options = new LoggerOptions(Constants.LogLevel.Error, message);
            Task.Run(() => _writer.WriteToFile(Path, options));
        }
    }
}