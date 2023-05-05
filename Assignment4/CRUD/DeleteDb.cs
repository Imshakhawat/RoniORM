using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.CRUD
{
    public class DeleteDb
    {
        private readonly SqlConnection _connection = new SqlConnection(@"Server=BS-1027\SQLEXPRESS;Database=ORMtest;Trusted_Connection=True;Encrypt=False");
        public void DeleteOperation(object obj, string foreignKey, object foreignKeyRefID)
        {
            Dictionary<string, object> objMapper = new Dictionary<string, object>();

            Type type = obj.GetType();

            string tableName = null;

            tableName = type.IsClass ? type.Name : null;

            string foreignKeyName = $"{tableName.ToLower()}id";

            PropertyInfo[] properties = type.GetProperties();

            var foreignKeyID = properties.FirstOrDefault(x => x.Name.ToLower() == "id")?.GetValue(obj); type.GetProperty("id").ToString();




            if (foreignKey != null && foreignKeyRefID != null)
            {
                objMapper.Add(foreignKey, foreignKeyRefID);
            }

            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(obj);
                if (value == null) continue;
                if (value is string || value.GetType().IsValueType)
                {
                    objMapper.Add(property.Name, value);
                }
                else if (value is IList list)
                {
                    foreach (var item in list)
                    {
                        DeleteOperation(item, foreignKeyName, foreignKeyID);
                    }
                }
                else
                {
                    DeleteOperation(value, foreignKeyName, foreignKeyID);
                }
            }
            int x = Convert.ToInt32(foreignKeyID);
            SqlBuilder(tableName, x);



        }




        public void SqlBuilder(string tableName, int id)
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



    }
}