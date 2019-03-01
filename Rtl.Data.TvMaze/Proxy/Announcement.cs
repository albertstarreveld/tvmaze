using System;

namespace Rtl.Data.TvMaze.Proxy
{
    public class Announcement
    {
        public DateTime airdate { get; set; }

        public string airtime { get; set; }

        public Show show { get; set; }
    }
}