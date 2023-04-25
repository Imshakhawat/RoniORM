using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class SqlConnectionStringReader : IConnectionStringReader
    {
        public SqlConnection GetConnectionString()
        {
            return new SqlConnection(@"Server=DESKTOP-8VMMQPN\SQLEXPRESS;Database=assignment_4;Trusted_Connection=True;Encrypt=False");
        }
    }
}
