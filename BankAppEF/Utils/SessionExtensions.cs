using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Utils
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T val)
        {
            session.SetString(key, JsonConvert.SerializeObject(val));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var stringRepresentation = session.GetString(key);
            if(stringRepresentation == null)
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(stringRepresentation);
            }
        }
    }
}
