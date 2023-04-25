using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
    public interface IMyORM<G, T>
    {
        public void Insert(T item);
        public void Update(T item);
        public void Delete(T item);
        public void Delete(G id);
        public void GetById(G id);
        public void GetAll();
    }
}
