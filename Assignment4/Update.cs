using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class Update
    {

            private readonly SqlConnection _connection = new SqlConnection(@"Server=DESKTOP-8VMMQPN\SQLEXPRESS;Database=assignment_4;Trusted_Connection=True;Encrypt=False");



            public void UpdateObjectInDb(object obj, string foreign_key, string refId)
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

                var id = properties.FirstOrDefault(x => x.Name.ToLower() == "id").GetValue(obj);

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
                    if (property.Name == "id") continue;
                        dict.Add(property.Name, value);
                    }
                    else if (value is IList list)
                    {
                        // Call the InsertObjectIntoDb method for each item in the list
                        // ...
                        foreach (var item in list)
                        {
                            UpdateObjectInDb(item, FkColumn, fkId);
                        }
                    }
                    else
                    {
                        // Call the InsertObjectIntoDb method for the nested object
                        // ...
                        UpdateObjectInDb(value, FkColumn, fkId);
                    }
                }

                // Generate and execute the update SQL statement
                // ...
                GenerateUpdateSql(tableName, dict, fkId);
            }


            public void GenerateUpdateSql(string tableName, Dictionary<string, object> columnValues, string? id)
            {
                var columns = string.Join(", ", columnValues.Keys.Select(x => $"{x}=@{x}"));

                var query = new StringBuilder();

                query.Append($"UPDATE {tableName} SET {columns} WHERE id=@id");

                Console.WriteLine(query);

                using (SqlCommand cmd = new SqlCommand(query.ToString(), _connection))
                {
                    foreach (var x in columnValues)
                    {
                        cmd.Parameters.AddWithValue("@" + x.Key, x.Value);
                    }
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

