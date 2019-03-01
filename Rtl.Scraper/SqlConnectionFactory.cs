using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rtl.Scraper
{
    public class SqlConnectionFactory : Rtl.Data.Sql.SqlConnectionFactory
    {
        protected override string GetConnectionString()
        {
            // todo: maybe not the best place for a connectionstring...
            return "Server=localhost;Database=Rtl;Trusted_Connection=True;";
        }
    }
}
