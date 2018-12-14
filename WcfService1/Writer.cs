using System;
using System.IO;
using System.Threading;

namespace WcfService1
{
    public class Writer : IWriter
    {
        private static string BasePath => AppDomain.CurrentDomain.BaseDirectory;
       // private static string Path => AppSettingsHelper.GetSettingValue(Constants.LoggerPath);

        private static readonly ReaderWriterLockSlim ReadWriteLock = new ReaderWriterLockSlim();

        public void WriteToFile(string path, WriterOptions options)
        {
            var fullPath = $"{BasePath}{path}";

            ReadWriteLock.EnterWriteLock();
            try
            {
                if (!File.Exists(fullPath))
                {
                    var swNew = File.CreateText(fullPath);
                    swNew.WriteLine(options.Message);
                    swNew.Close();
                }
                else
                {
                    var swAppend = File.AppendText(fullPath);
                    swAppend.WriteLine(options.Message);
                    swAppend.Close();
                }
            }
            finally
            {
                ReadWriteLock.ExitWriteLock();
            }
        }
    }
}