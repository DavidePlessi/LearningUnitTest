using Moq;
using NUnit.Framework;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _service;
        private Mock<IFileReader> _fileReader;

        [SetUp]
        public void SetUp()
        {
            // Create new mock file reader
            _fileReader = new Mock<IFileReader>();
            //Pass the mock obj
            _service = new VideoService(_fileReader.Object);
        }
        
        [Test]
        public void ReadVideoTitle_EmtyFile_ReturnError()
        {
            // Implement the Read method
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            var resu = _service.ReadVideoTitle();
            Assert.That(resu, Does.Contain("error").IgnoreCase);
        }
    }
}