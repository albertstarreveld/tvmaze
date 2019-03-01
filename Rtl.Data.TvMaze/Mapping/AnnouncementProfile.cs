using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rtl.Data.TvMaze.Mapping
{
    public class AnnouncementProfile : AutoMapper.Profile
    {
        public AnnouncementProfile()
        {
            this.CreateMap<Proxy.Announcement, Domain.Announcement>()
                .ConvertUsing(x => Map(x));
        }

        private static Domain.Announcement Map(Proxy.Announcement announcement)
        {
            var show = Map(announcement.show);

            var day = announcement.airdate.ToString("yyyy-MM-dd");
            var isoDateString = $"{day}T{announcement.airtime}:00";

            DateTime airdate;
            if (!DateTime.TryParse(isoDateString, out airdate))
            {
                throw new FormatException($"Cannot parse date {day} and time {announcement.airtime}. Unable to process TvMaze announcements..");
            }

            return new Domain.Announcement(show, airdate);
        }

        private static Domain.Show Map(Proxy.Show show)
        {
            return new Domain.Show(show.id, show.name, show.Cast.Select(Map));
        }

        private static Domain.Actor Map(Proxy.Character character)
        {
            return new Domain.Actor(character.person.id, character.person.name, character.person.birthday);
        }
    }
}
