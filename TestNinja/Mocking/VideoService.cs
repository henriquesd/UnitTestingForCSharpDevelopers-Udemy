using System;
using System.Collections.Generic;
using System.Data.Entity;
using Newtonsoft.Json;
using static TestNinja.Mocking.VideoRepository;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private IFileReader _fileReader;
        private IVideoRepository _repository;

        //public VideoService(IFileReader fileReader = null,
        //      IVideoRepository repository = null)
        //{
        //    _fileReader = fileReader ?? new FileReader();
        //    _repository = repository ?? new VideoRepository();
        //}
        public VideoService(IFileReader fileReader = null,
                            IVideoRepository repository = null)
        {
            // _fileReader and _repository is used just in one method,
            // for design perspective it's better to pass the dependency as a parameter to that method,
            // but this feature may not be supported with the dependency injection framework you are using, so your only option might be constructor injection;
            _fileReader = fileReader ?? new FileReader();
            _repository = repository ?? new VideoRepository();
        }

        // The commented codes shows implementation before refactor to a better code;

        //public string ReadVideoTitle()
        //{
        //    var str = new FileReader().Read("video.txt");
        //    var video = JsonConvert.DeserializeObject<Video>(str);
        //    if (video == null)
        //        return "Error parsing the video.";
        //    return video.Title;
        //}

        // Inject dependency as a method parameter;
        //public string ReadVideoTitle(IFileReader fileReader)
        //{
        //    var str = fileReader.Read("video.txt");
        //    var video = JsonConvert.DeserializeObject<Video>(str);
        //    if (video == null)
        //        return "Error parsing the video.";
        //    return video.Title;
        //}


        // Inject dependency using a property;
        //public IFileReader FileReader { get; set; }

        //public VideoService()
        //{
        //    FileReader = new FileReader();
        //}

        //public string ReadVideoTitle()
        //{
        //    var str = FileReader.Read("video.txt");
        //    var video = JsonConvert.DeserializeObject<Video>(str);
        //    if (video == null)
        //        return "Error parsing the video.";
        //    return video.Title;
        //}



        // In the production code will use this constructor;
        //public VideoService()
        //{
        //    _fileReader = new FileReader(); ;
        //}

        //// In the test code will use this constructor so can pass a fake file reader;
        //public VideoService(IFileReader fileReader)
        //{
        //    // constructor injection;
        //    _fileReader = fileReader;
        //}

        //public VideoService(IFileReader fileReader = null)
        //{
        //    // constructor injection;
        //    /* this aproach works but it's not ideal in enterprise applications,
        //    because in the real world application this class might have a couple of more dependencies,
        //    and you don't want repeat this expression a few times and also make this parameters options,
        //    looks a little bit ugly, that's why there refer to this aproach as 'Poor man's dependency injection',
        //    in the real enterprise application you don't wanna do this, you wanna keep your code as simples as possible,
        //    thats why we use a dependency injection framewok; when you use a property dependency injection framework in your application,
        //    you can simplify your constructor in something like this: (see line 80 and 82) */

        //    // if fileReader is not null will use that to set the private field,
        //    // otherwise if it is null we gonna use a new a FileReader object.
        //    _fileReader = fileReader ?? new FileReader();
        //}


        //public VideoService(IFileReader fileReader)
        //{
        //    // dependency injection framework will take care of creating and
        //    // initializing objects runtime; examples of dependency injection frameworks:
        //    // NInject; StructureMap; Spring.NET; Autofac; Unity; etc - almost all this frameworks follow the same principle;
        //    // Mosh Hamedani recommend NInject or Autofac;
        //    _fileReader = fileReader;
        //}

        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        // This commented code has been refactored on the method below;
        //public string GetUnprocessedVideosAsCsv()
        //{
        //    var videoIds = new List<int>();

        //    using (var context = new VideoContext())
        //    {
        //        var videos =
        //            (from video in context.Videos
        //             where !video.IsProcessed
        //             select video).ToList();

        //        foreach (var v in videos)
        //            videoIds.Add(v.Id);

        //        return String.Join(",", videoIds);
        //    }
        //}

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _repository.GetUnprocessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}