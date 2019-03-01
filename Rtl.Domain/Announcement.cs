using System;

namespace Rtl.Domain
{
    public class Announcement
    {
        public Show Show { get; private set; }

        public DateTime Airdate { get; private set; }

        public Announcement(Show show, DateTime airdate)
        {
            if (show == null)
            {
                throw new ArgumentNullException(nameof(show));
            }

            Show = show;
            Airdate = airdate;
        }
    }
}