using System.Net;

namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string address, string destination);
    }

    public class FileDownloader : IFileDownloader
    {
        public void DownloadFile(string address, string destination)
        {
            new WebClient().DownloadFile(address,destination);
        }
    }
}