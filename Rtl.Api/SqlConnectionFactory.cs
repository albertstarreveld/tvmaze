using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rtl.Api
{
    public class SqlConnectionFactory : Rtl.Data.Sql.SqlConnectionFactory
    {
        protected override string GetConnectionString()
        {
            // todo: read from config..
            return "Server=localhost;Database=Rtl;Trusted_Connection=True;";
        }
    }
}
