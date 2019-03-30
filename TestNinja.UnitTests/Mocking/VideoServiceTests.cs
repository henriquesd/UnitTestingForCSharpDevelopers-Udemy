using Moq;
using NUnit.Framework;
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

        [SetUp]
        public void SetUp()
        {
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _videoService.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
