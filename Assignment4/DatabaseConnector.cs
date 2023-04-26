using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class DatabaseConnector : IDisposable
    {
        private readonly SqlConnection _connection;

        public DatabaseConnector(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<Dictionary<string, object>> ExecuteQuery(string query, int id)
        {
            List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();

            using (var cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                if (_connection.State != System.Data.ConnectionState.Open)
                    _connection.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            dict.Add(reader.GetName(i), reader.GetValue(i));
                        }

                        result.Add(dict);
                    }
                }
            }

            return result;
        }

        public void Dispose()
        {
            _connection.Dispose();
        }
    }



}
