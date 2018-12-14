namespace WcfService1
{
    public interface IWriter
    {
        void WriteToFile(string path, string message);
    }
}