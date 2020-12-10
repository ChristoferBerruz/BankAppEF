using BankAppEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.DataLayer
{
    public interface IRepositoryAuthentication
    {
        UserInfo GetUserInfo(string username);
    }
}
