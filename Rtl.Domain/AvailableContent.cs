using System.Collections.Generic;
using System.Linq;

namespace Rtl.Domain
{
    public class AvailableContent
    {
        private readonly IList<Announcement> _announcements = new List<Announcement>();

        public void Update(Announcement announcement)
        {
            var simularAnnouncements = _announcements
                .Where(x => x.Airdate == announcement.Airdate && x.Show.Name == announcement.Show.Name)
                .ToList();

            foreach (var simularAnnouncement in simularAnnouncements)
            {
                _announcements.Remove(simularAnnouncement);
            }

            _announcements.Add(announcement);
        }

        public IEnumerable<Show> GetShows()
        {
            return _announcements
                .Select(x => x.Show)
                .Distinct();
        }
    }
}