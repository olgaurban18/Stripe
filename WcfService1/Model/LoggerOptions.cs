using System;

namespace WcfService1
{
    public class LoggerOptions
    {
        public string Message { get; set; }
        public LogLevel Level { get; set; }

        public LoggerOptions(LogLevel level, string message)
        {
            Level = level;
            Message = $"[{level.ToString()}][{DateTime.UtcNow}]:{message}";
        }
    }
}