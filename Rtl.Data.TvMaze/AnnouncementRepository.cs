using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Rtl.Data.TvMaze.Proxy;
using Announcement = Rtl.Domain.Announcement;

namespace Rtl.Data.TvMaze
{
    public interface IAnnouncementRepository
    {
        Task<Announcement[]> Get(string country);
    }

    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly ITvMazeProxy _proxy;

        public AnnouncementRepository(ITvMazeProxy proxy)
        {
            _proxy = proxy;
        }

        public async Task<Announcement[]> Get(string country)
        {
            var announcements = await _proxy.GetShows(country);
            foreach (var announcement in announcements)
            {
                announcement.show.Cast = await _proxy.GetCast(announcement.show.id);
            }

            return announcements
                .Select(Mapper.Map<Announcement>)
                .ToArray();
        }
    }
}
