using AutoMapper;
using Rtl.Data.Sql;
using Rtl.Data.TvMaze;
using Rtl.Data.TvMaze.Mapping;
using Rtl.Data.TvMaze.Proxy;
using Rtl.Services;

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
                var sqlConnectionFactory = new SqlConnectionFactory();
                var tvMazeProxy = new TvMazeProxy("http://api.tvmaze.com");
                var tvMaze = new TvMazeDownloadService(new TvMazeRepository(tvMazeProxy), new AnnouncementRepository(sqlConnectionFactory));

                tvMaze
                    .SynchronizeOurDatabase("US")
                    .GetAwaiter()
                    .GetResult();
            }
        }
 
    }


}
