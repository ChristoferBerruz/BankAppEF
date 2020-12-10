using Couchbase;
using Couchbase.Core;
using Couchbase.Configuration.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Cache
{
    public class CouchBaseCacheAdapter : IWebCache
    {
        static ClientConfiguration config = null;
        private readonly Cluster cluster = null;
        IBucket _ibucket = null;

        public CouchBaseCacheAdapter()
        {
            config = new ClientConfiguration
            {
                Servers = new List<Uri>
                {
                    new Uri("http://127.0.0.1:8091")
                }
            };
            cluster = new Cluster(config);
            cluster.Authenticate("Administrator", "123456");
            _ibucket = cluster.OpenBucket("mybucket");
        }
        #region IWebCache Members
        public void Remove(string key)
        {
            try
            {
                var upsert = _ibucket.Remove(key);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public void Store(string key, object obj)
        {
            try
            {
                var upsert = _ibucket.Upsert(key, obj);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        public T Retrieve<T>(string key)
        {
            T cachedData = default(T);
            var data = _ibucket.GetDocument<T>(key);
            cachedData = data.Document.Content;
            return cachedData;
        }
        #endregion

    }
}
