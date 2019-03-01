using Rtl.Domain;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Rtl.Data.Sql.Builders;

namespace Rtl.Data.Sql
{
    public class AnnouncementRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;

        public AnnouncementRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task Save(IEnumerable<Announcement> announcements)
        {
            var serializedAnnouncements = new XmlBuilder()
                .WithRootnodeName("ArrayOfAnnouncement")
                .WithAnnouncements(announcements)
                .Build();

            using (var connection = await _connectionFactory.CreateOpenConnection())
            using (var command = new SqlCommand("ProcessAnnouncements", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("xml", serializedAnnouncements.ToString());
                await command.ExecuteNonQueryAsync();
            }
        }
    }



}
