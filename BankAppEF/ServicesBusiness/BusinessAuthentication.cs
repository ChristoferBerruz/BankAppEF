using BankAppEF.DataLayer;
using BankAppEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.ServicesBusiness
{
    public class BusinessAuthentication : IBusinessAuthentication
    {
        IRepositoryAuthentication _authRepository = null;

        public BusinessAuthentication(IRepositoryAuthentication authRepo)
        {
            _authRepository = authRepo;
        }

        public BusinessAuthentication(MYBANKContext context) : this(new RepositoryEF(context)) { }
        public UserInfo GetUserInfo(string username)
        {
            return _authRepository.GetUserInfo(username);
        }
    }
}
