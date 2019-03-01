using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Rtl.Data.Sql.Builders;
using Rtl.Domain;

namespace Rtl.Data.Sql
{
    public interface IShowRepository
    {
        Task<IEnumerable<Show>> FindAll(int pageSize, int page);
    }

    public class ShowRepository : IShowRepository
    {
        private readonly SqlConnectionFactory _connectionFactory;
        
        public ShowRepository(SqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<IEnumerable<Show>> FindAll(int pageSize, int page)
        {
            using (var connection = await _connectionFactory.CreateOpenConnection())
            using (var command = new SqlCommand("FindShows", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("pageSize", pageSize);
                command.Parameters.AddWithValue("page", page);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    return await Map(reader);
                }
            }
        }

        private static async Task<IEnumerable<Show>> Map(DbDataReader reader)
        {
            var showsBuilder = new ShowCollectionBuilder();

            // Read cast
            while (await reader.ReadAsync())
            {
                var actor = new ActorBuilder()
                    .WithBirthDate(reader["birthdate"] as DateTime?)
                    .WithId((int)reader["id"])
                    .WithName((string)reader["name"])
                    .Build();

                var showId = (int)reader["show"];
                showsBuilder = showsBuilder
                    .WithActor(actor)
                    .WithCast(showId, actor.Id);
            }
            
            await reader.NextResultAsync();

            // Read shows
            while (await reader.ReadAsync())
            {
                var id = (int)reader["id"];
                var name = (string)reader["name"];

                showsBuilder = showsBuilder
                    .WithShow(id, name);
            }

            return showsBuilder.Build();
        }
    }
}
