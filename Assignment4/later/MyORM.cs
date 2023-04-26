using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.later
{
    public class MyORM
    {
        private readonly IConnectionStringReader _connectionStringReader;
        private readonly SqlConnection _connection;
        //public MyORM(IConnectionStringReader connectionStringReader)
        //{
        //    _connectionStringReader = connectionStringReader;
        //    _connection = _connectionStringReader.GetConnectionString();
        //}

        public void Insert(Course item)
        {
            //InsertIntoDatabase Obj1 = new InsertIntoDatabase(_connection);
            //Obj1.InsertObjectIntoDb(item, null, null);
        }
        public void Update(Course item)
        {

        }
        public void Delete(Course item)
        {

        }
        public void Delete(int id)
        {

        }
        public void GetById(int id)
        {

        }
        public void GetAll()
        {

        }

    }
}
