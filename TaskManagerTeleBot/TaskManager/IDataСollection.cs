using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager
{
    internal interface IDataСollection<T>
    {        
        public static bool Add(T t)
        {
            return true;
        }
        public static T Search(int id)
        {
            return default(T);
        }
        public static bool Update(T t, Dictionary<string, string> parametrs)
        {
            return true;
        }
        public static bool Delete(T t) 
        {
            return true;
        }
    }
}
