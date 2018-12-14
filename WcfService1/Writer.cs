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

        public void WriteToFile(string path, string message)
        {
            var fullPath = $"{BasePath}{path}";

            ReadWriteLock.EnterWriteLock();
            try
            {
                if (!File.Exists(fullPath))
                {
                    var swNew = File.CreateText(fullPath);
                    swNew.WriteLine(message);
                    swNew.Close();
                }
                else
                {
                    var swAppend = File.AppendText(fullPath);
                    swAppend.WriteLine(message);
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