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

    public class DataExtractor
    {
        private readonly DatabaseConnector _connector;

        public DataExtractor(DatabaseConnector connector)
        {
            _connector = connector;
        }

        public List<Dictionary<string, object>> ExtractData(object obj, int id, string tableName)
        {
            var result = new List<Dictionary<string, object>>();

            StringBuilder query = new StringBuilder();

            query.Append($"select * from {obj.GetType().Name} where {tableName}id = @id");

            
            var data = _connector.ExecuteQuery(query.ToString(), id);
            result.AddRange(data);

            Type type = obj.GetType();

            PropertyInfo[] properties = type.GetProperties();

            if (type.IsClass)
            {
                tableName = type.Name;
            }

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj);

                if (value == null) continue;

                else if (value is string || value.GetType().IsValueType) continue;

                else if (value is IList list)
                {
                    foreach (var item in list)
                    {
                        var subData = ExtractData(item, id, tableName);
                        result.AddRange(subData);
                    }
                }
                else
                {
                    var subData = ExtractData(value, id, tableName);
                    result.AddRange(subData);
                }
            }

            return result;
        }
    


    public List<Dictionary<string, object>> ExtractAllData(object obj, string tableName)
    {
        var result = new List<Dictionary<string, object>>();

        StringBuilder query = new StringBuilder();

        query.Append($"select * from {obj.GetType().Name}");

        var data = _connector.ExecuteQuery(query.ToString(), 0);

        result.AddRange(data);

        Type type = obj.GetType();

        PropertyInfo[] properties = type.GetProperties();

        if (type.IsClass)
        {
            tableName = type.Name;
        }

        foreach (PropertyInfo property in properties)
        {
            var value = property.GetValue(obj);

            if (value == null) continue;

            else if (value is string || value.GetType().IsValueType) continue;

            else if (value is IList list)
            {
                foreach (var item in list)
                {
                    var subData = ExtractAllData(item, tableName);
                    result.AddRange(subData);
                }
            }
            else
            {
                var subData = ExtractAllData(value, tableName);
                result.AddRange(subData);
            }
        }

        return result;
    }
    }
    public class DataPrinter
    {
        public void PrintData(List<Dictionary<string, object>> data)
        {
            foreach (var obj in data)
            {
                foreach (var item in obj)
                {
                    Console.Write(item.Key + ":" + item.Value + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
