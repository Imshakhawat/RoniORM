using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4
{
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
