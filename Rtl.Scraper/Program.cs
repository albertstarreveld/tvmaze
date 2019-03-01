using AutoMapper;
using Rtl.Data.TvMaze;
using Rtl.Data.TvMaze.Mapping;
using Rtl.Data.TvMaze.Proxy;

namespace Rtl.Scraper
{
    class Program
    {
        static void Main(string[] args)
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<AnnouncementProfile>();
            });
            
            while (true)
            {
                // todo: do something with these hard coded values
                var tvMaze = new AnnouncementRepository(new TvMazeProxy("http://api.tvmaze.com"));
                var announcements = tvMaze.Get("US")
                    .GetAwaiter()
                    .GetResult();
                
                var database = new Rtl.Data.Sql.AnnouncementRepository(new SqlConnectionFactory());
                database.Save(announcements)
                    .GetAwaiter()
                    .GetResult();
            }
        }
 
    }


}
