using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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
        private Mock<IVideoRepository> _videoContext;

        [SetUp]
        public void SetUp()
        {
            // Create new mock file reader
            _fileReader = new Mock<IFileReader>();
            
            _videoContext = new Mock<IVideoRepository>();
            //Pass the mock obj
            _service = new VideoService(_fileReader.Object, _videoContext.Object);
        }
        
        [Test]
        public void ReadVideoTitle_EmtyFile_ReturnError()
        {
            // Implement the Read method
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            var resu = _service.ReadVideoTitle();
            Assert.That(resu, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void GetUnprocessedVideoAsCsv_AllVideoProcessed_ReturnEmptyString()
        {
            _videoContext.Setup(vc => vc.GetUnprocessed()).Returns(new List<Video>());

            var res = _service.GetUnprocessedVideosAsCsv();
            
            Assert.That(res, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideoAsCsv_SomeUnprocessedVideo_ReturnIdJoined()
        {
            _videoContext.Setup(vc => vc.GetUnprocessed()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var res = _service.GetUnprocessedVideosAsCsv();
            
            Assert.That(res, Is.EqualTo("1,2,3"));
        }
    }
}