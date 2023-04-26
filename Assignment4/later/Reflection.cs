using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment4.later
{
    public static class Reflection
    {
        public static void Convert(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            Console.WriteLine("Table:" + obj.GetType().Name);

            foreach (var property in properties)
            {
                var value = property.GetValue(obj);
                if (value is null) continue;
                else if (value is string || value.GetType().IsValueType)
                {
                    Console.WriteLine("Column:" + property.Name);
                    Console.WriteLine("Value: " + value);
                }
                else if (value is IList list)
                {
                    foreach (var item in list)
                        Convert(item);
                }
                else
                {
                    Convert(value);
                }
            }
        }
    }

}


