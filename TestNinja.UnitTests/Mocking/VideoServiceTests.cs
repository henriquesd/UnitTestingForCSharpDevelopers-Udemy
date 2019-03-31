using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        // The commented codes shows implementation before refactor to a better code;

        //[Test]
        //public void ReadVideoTitle_EmptyFile_ReturnError()
        //{
        //    var service = new VideoService();

        //    var result = service.ReadVideoTitle(new FakeFileReader());

        //    Assert.That(result, Does.Contain("error").IgnoreCase);
        //}

        // Test for method thath inject dependency using a property;
        //[Test]
        //public void ReadVideoTitle_EmptyFile_ReturnError()
        //{
        //    var service = new VideoService();
        //    service.FileReader = new FakeFileReader();

        //    var result = service.ReadVideoTitle();

        //    Assert.That(result, Does.Contain("error").IgnoreCase);
        //}

        //[Test]
        //public void ReadVideoTitle_EmptyFile_ReturnError()
        //{
        //    var service = new VideoService(new FakeFileReader());

        //    var result = service.ReadVideoTitle();

        //    Assert.That(result, Does.Contain("error").IgnoreCase);
        //}

        private VideoService _videoService;
        private Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            // _fileReader and _repository is used just in one method (on the VideoService class too),
            // for design perspective it's better to pass the dependency as a parameter to that method,
            // but this feature may not be supported with the dependency injection framework you are using, so your only option might be constructor injection;
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
        
        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>
            {
                new Video { Id = 1 },
                new Video { Id = 2 },
                new Video { Id = 3 }
            });

            var result = _videoService.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
