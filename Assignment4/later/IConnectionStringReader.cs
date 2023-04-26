using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.later
{
    public interface IConnectionStringReader
    {
        public SqlConnection GetConnectionString();
    }
}
