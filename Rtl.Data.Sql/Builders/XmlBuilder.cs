using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Rtl.Domain;

namespace Rtl.Data.Sql.Builders
{
    public class XmlBuilder
    {
        private readonly List<Announcement> _announcements = new List<Announcement>();
        private string _rootnode;

        public XmlBuilder WithRootnodeName(string rootnode)
        {
            _rootnode = rootnode;
            return this;
        }

        public XmlBuilder WithAnnouncements(IEnumerable<Announcement> announcement)
        {
            _announcements.AddRange(announcement);
            return this;
        }

        public XElement Build()
        {
            var result = new XElement(_rootnode);
            foreach (var announcement in _announcements)
            {
                var show = announcement.Show;
                var cast = show.Cast;

                var actorsXml = cast.Select(x => new XElement("Actor",
                    new XElement("Name", x.Name),
                    new XElement("Birthdate", $"{x.Birthdate:yyyy-MM-dd}"),
                    new XElement("Id", x.Id)
                ));

                var announcementXml = new XElement("Announcement",
                    new XElement("Airdate", announcement.Airdate),
                    new XElement("Show",
                        new XElement("Id", announcement.Show.Id),
                        new XElement("Name", announcement.Show.Name),
                        new XElement("Cast", new XElement("ArrayOfActor", actorsXml))
                    ));

                result.Add(announcementXml);
            }

            return result;
        }

    }
}
