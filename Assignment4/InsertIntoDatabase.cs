﻿using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Assignment4
{
    public class InsertIntoDatabase
    {
        private readonly SqlConnection _connection = new SqlConnection(@"Server=BS-1027\SQLEXPRESS;Database=ORMtest;Trusted_Connection=True;Encrypt=False");
        public void InsertObjectIntoDb(object obj, string foreignKey, string foreignKeyRefID)
        {
            Dictionary<string, object> objMapper = new Dictionary<string, object>();

            Type type = obj.GetType();

            string tableName = null;

            tableName = type.IsClass ? type.Name : null;

            string foreignKeyName = $"{tableName.ToLower()}id";

            string? foreignKeyID = type.GetProperty("id").ToString();


            PropertyInfo[] properties = type.GetProperties();

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
                        InsertObjectIntoDb(item, foreignKeyName, foreignKeyID);
                    }
                }
                else
                {
                    InsertObjectIntoDb(value, foreignKeyName, foreignKeyID);
                }
            }

            SqlBuilder2(tableName, objMapper);
        }



        public void SqlBuilder(string tableName, Dictionary<string, object> columnValues)
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



        public void SqlBuilder2(string tableName, Dictionary<string, object> columnValues)
        {
            var columns = string.Join(", ", columnValues.Keys);
            var parameters = string.Join(", ", columnValues.Keys.Select(k => "@" + k));

            var query = $"INSERT INTO {tableName} ({columns}) VALUES ({parameters})";

            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                foreach (var x in columnValues)
                {
                    cmd.Parameters.AddWithValue("@" + x.Key, x.Value ?? DBNull.Value);
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









    }
}