using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Utils
{
    /// <summary>
    /// Provides manages access to the HttpContext part of a client request received by the server.
    /// This class is not useful for Pages, as the HttpContext is made available to them
    /// by the Context object.
    /// </summary>
    public class HttpContextHelper
    {
        static IHttpContextAccessor _IHttpContextAccessor = null;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _IHttpContextAccessor =  httpContextAccessor;
        }

        public static HttpContext HttpContext
        {
            get { return _IHttpContextAccessor.HttpContext; }
        }
    }
}
