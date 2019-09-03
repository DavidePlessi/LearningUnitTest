using System.Net;
using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        [SetUp]
        public void SetUp()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(fileDownloader: _fileDownloader.Object);
        }
        
        [Test]
        public void DownloadInstaller_FileFoundAndSaved_ReturnTrue()
        {
            _fileDownloader
                .Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()));
            var res = _installerHelper.DownloadInstaller("customer", "installer");
            Assert.That(res, Is.True);
        }
        
        [Test]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader
                .Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();
            var res = _installerHelper.DownloadInstaller("customer", "installer");
            Assert.That(res, Is.False);
        }
    }
}