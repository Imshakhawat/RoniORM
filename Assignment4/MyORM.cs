using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public class MyORM<G, T> where T: new()
    {
        private readonly T _t;
        public MyORM()
        {
            _t = new T();
        }
        public void Insert(T item)
        {
            //both generic and non generic classes can be inserted using this method. 
            InsertDb insert = new InsertDb();
            insert.InsertOperation(item, null, null);
        }
        //public void Update(T item)
        //{
        //    UpdateOperation update = new UpdateOperation();
        //    update.UpdateObjectInDb(item, null, null);
        //}
        //public void Delete(T item)
        //{
        //    DeleteOperation<T> delete = new DeleteOperation<T>(_t);
        //    delete.DeleteObject(item, null, null);
        //}
        //public void Delete(G id)
        //{
        //    DeleteOperation<T> delete = new DeleteOperation<T>(_t);
        //    //delete.DeleteObjectById(id, null);
        //}
        //public void GetById(G id)
        //{
        //    GetOperation<G, T> get = new GetOperation<G, T>();
        //    T item = new T();
        //    List<Dictionary<string, object>> result = get.ExtractData(_t, id, null);
        //    get.PrintData(result);
        //}
        //public void GetAll()
        //{
        //    GetOperation<G, T> get = new GetOperation<G, T>();
        //    T item = new T();
        //    List<Dictionary<string, object>> result = get.ExtractAllData(item);
        //    get.PrintData(result);

        //}





    }
}
