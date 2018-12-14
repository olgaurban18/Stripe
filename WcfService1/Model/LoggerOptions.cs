using System;

namespace WcfService1
{
    public class LoggerOptions : WriterOptions
    {
        public LoggerOptions(string type, string message)
        {
           // Path = ;
            Message = $"[{type}][{DateTime.UtcNow}]:{message}";
        }
    }
}