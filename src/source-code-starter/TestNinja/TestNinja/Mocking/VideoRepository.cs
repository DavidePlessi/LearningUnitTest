using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestNinja.Mocking
{
    public interface IVideoRepository
    {
        IEnumerable<Video> GetUnprocessed();
    }

    public class VideoRepository : IVideoRepository
    {
        public IEnumerable<Video> GetUnprocessed()
        {
            using (var context = new VideoContext())
            {
                return context.Videos.Where(x => !x.IsProcessed);
            }
        }
    }
}