using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Assignment4
{
    public class DatabaseManager
    {
        private readonly SqlConnection _connection = new SqlConnection(@"Server=BS-1027\SQLEXPRESS;Database=ORMtest;Trusted_Connection=True;Encrypt=False");

        public void DeleteObject(object obj, string foreign_key, string refId)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();

            Type type = obj.GetType();

            string tableName = string.Empty;

            if (type.IsClass)
            {
                tableName = type.Name;
            }

            string FkColumn = $"{tableName.ToLower()}id";

            PropertyInfo[] properties = type.GetProperties();

            var id = properties.FirstOrDefault(x => x.Name.ToLower() == "id")?.GetValue(obj);

            if (id == null)
            {
                throw new Exception("Object Must Have An ID Property.");
            }

            string? fkId = id.ToString();

            if (foreign_key != null && refId != null)
            {
                dict.Add(foreign_key, refId);
            }

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj);
                if (value == null) continue;
                if (value is string || value.GetType().IsValueType)
                {
                    dict.Add(property.Name, value);
                }
                else if (value is IList list)
                {
                    foreach (var item in list)
                    {
                        //  DeleteObjectFromDb(item.GetType(), GetPropertyValue(item, "id"));
                        DeleteObject(item, FkColumn, fkId);
                    }
                }
                else
                {
                    //   DeleteObjectFromDb(value.GetType(), GetPropertyValue(value, "id"));
                    DeleteObject(value, FkColumn, fkId);
                }
            }



            DeleteObjectFromDb(tableName, (int)id);
        }

        public void DeleteObjectFromDb(string tableName, int id)
        {
            if (id == null) return;

            string deleteQuery = $"DELETE FROM {tableName} WHERE {id}=@id";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, _connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                try
                {
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                }
            }
        }

        public void GenerateInsertSql(string tableName, Dictionary<string, object> columnValues)
        {
            var columns = string.Join(", ", columnValues.Keys);

            var parameters = string.Join(", ", columnValues.Select(x => "@" + x.Key));

            var query = new StringBuilder();

            query.Append($"INSERT INTO {tableName} ({columns}) VALUES ({parameters})");

            Console.WriteLine(query);

            using (SqlCommand cmd = new SqlCommand(query.ToString(), _connection))
            {
                foreach (var x in columnValues)
                {
                    cmd.Parameters.AddWithValue("@" + x.Key, x.Value);
                }
                try
                {
                    _connection.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (_connection.State != ConnectionState.Closed)
                    {
                        _connection.Close();
                    }
                }



            }


        }


        private object GetPropertyValue(object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName)?.GetValue(obj, null);
        }

    }

}