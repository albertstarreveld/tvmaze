using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Rtl.Data.Sql
{
    public abstract class SqlConnectionFactory
    {
        public async Task<SqlConnection> CreateOpenConnection()
        {
            var connectionString = GetConnectionString();
            var connection = new SqlConnection(connectionString);

            await connection.OpenAsync();
            return connection;
        }

        protected abstract string GetConnectionString();
    }
}
