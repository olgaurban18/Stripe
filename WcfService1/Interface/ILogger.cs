﻿namespace WcfService1
{
    public interface ILogger
    {
        void Debug(string message);
        void Info(string message);
        void Error(string message);
    }
}