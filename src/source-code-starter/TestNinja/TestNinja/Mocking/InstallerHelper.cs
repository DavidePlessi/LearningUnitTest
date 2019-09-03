using System.Net;

namespace TestNinja.Mocking
{
    public class InstallerHelper
    {
        private string _setupDestinationFile;
        private readonly IFileDownloader _fileDownloader;

        public InstallerHelper(string setupDestinationFile = "", IFileDownloader fileDownloader = null)
        {
            _setupDestinationFile = setupDestinationFile;
            _fileDownloader = fileDownloader ?? new FileDownloader();
        }

        public bool DownloadInstaller(string customerName, string installerName)
        {
            try
            {
                _fileDownloader.DownloadFile(
                    string.Format("http://example.com/{0}/{1}", customerName, installerName),
                    _setupDestinationFile
                );

                return true;
            }
            catch (WebException)
            {
                return false; 
            }
        }
    }
}