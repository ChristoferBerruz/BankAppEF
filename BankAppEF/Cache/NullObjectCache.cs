using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Cache
{
    public class NullObjectCache : IWebCache
    {
        public void Remove(string key)
        {
            
        }

        public T Retrieve<T>(string key)
        {
            return default(T);
        }

        public void Store(string key, object obj)
        {
            
        }
    }
}
