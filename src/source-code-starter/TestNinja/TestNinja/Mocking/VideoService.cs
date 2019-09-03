using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    /* Refactoring towards a loosely-coupled design:
     *
     * 1 - Make a new class FileReader
     * 2 - Move var str = File.ReadAllText("video.txt"); of ReadVideoTitle in the new class
     * 3 - Extract an interfase from FileReaderClass
     * 4 - Add a new class inside TestNinja.UnitTests called FakeFileReader that implement the interface IFileReader
     */
    
    /* Dependency injection via method parameters
     *
     * In this case pass the reader object as a parameter for the ReadVideoTitle method
        public string ReadVideoTitle(IFileReader reader)
     * Inside the test class call the method with the fake reader    
        var resu = service.ReadVideoTitle(new FakeFileReader());
     */
    
    /* Dependency injection via properties
     *
     * 1 - Declare a prop IFileReader
     * 2 - Make a constructor that initialize that prop
     * 
        public IFileReader FileReader { get; set; }

        public VideoService()
        {
            FileReader = new FileReader();
        }
     * Inside the test class replace the prop with the fake one:
        service.FileReader = new FakeFileReader();
     */
    
    /* Dependency injection via contrsuctor
     *
     * 1 - Declare a private field IFileReader
     * 2 - Make a constructor that accept the reader as parameter
     * 3 - Fill the field with the passed reader 
        private IFileReader _fileReader;

        public VideoService(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
     * We can also add a new constructor without parameter that initialize with the production reader the field  
        public VideoService()
        {
            _fileReader = new FileReader();
        } 
     * or set the fisrt parameter of the first constructor optional        
        public VideoService(IFileReader fileReader = null)
        {
            _fileReader = fileReader ?? new FileReader();
        }
     * 
     */
    
// BEFORE   
//    public class VideoService
//    {
//        public string ReadVideoTitle()
//        {
//            var str = File.ReadAllText("video.txt");
//            var video = JsonConvert.DeserializeObject<Video>(str);
//            if (video == null)
//                return "Error parsing the video.";
//            return video.Title;
//        }
//
//        public string GetUnprocessedVideosAsCsv()
//        {
//            var videoIds = new List<int>();
//            
//            using (var context = new VideoContext())
//            {
//                var videos = 
//                    (from video in context.Videos
//                        where !video.IsProcessed
//                        select video).ToList();
//                
//                foreach (var v in videos)
//                    videoIds.Add(v.Id);
//
//                return String.Join(",", videoIds);
//            }
//        }
//    }
//
//    public class Video
//    {
//        public int Id { get; set; }
//        public string Title { get; set; }
//        public bool IsProcessed { get; set; }
//    }
//
//    public class VideoContext : DbContext
//    {
//        public DbSet<Video> Videos { get; set; }
//    }

// AFTER
    
    public class VideoService : IDisposable
    {
        private readonly IFileReader _fileReader;
        private readonly IVideoRepository _videoRepository;
        
        public VideoService(IFileReader fileReader = null, IVideoRepository videoRepository = null)
        {
            _fileReader = fileReader ?? new FileReader();
            _videoRepository = videoRepository ?? new VideoRepository();
        }
        
        public string ReadVideoTitle()
        {
            var str = _fileReader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
            => string.Join(",", _videoRepository
                .GetUnprocessed()
                .Select(v => v.Id)
            );
        public void Dispose()
        {
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