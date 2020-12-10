using BankAppEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Utils
{
    public class SessionFacade
    {
        const string userInfoKey = "userInfoKey";

        public static UserInfo USERINFO
        {
            get
            {
                return HttpContextHelper.HttpContext.Session.Get<UserInfo>(userInfoKey);
            }
            set
            {
                HttpContextHelper.HttpContext.Session.Set<UserInfo>(userInfoKey, value);
            }
        }
    }
}
