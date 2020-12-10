using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppEF.Cache;
using BankAppEF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAppEF.Pages
{
    public class TestCacheModel : PageModel
    {
        CacheAbstraction _cabs = null;
        public string Message = "";
        public TestCacheModel(CacheAbstraction cabs)
        {
            _cabs = cabs;
        }
        public void OnGet()
        {
            UserInfo u1 = new UserInfo
            {
                UserName = "bill",
                CheckingAccountNumber = 12345
            };
            _cabs.Insert("USER1", u1);
            Message = "user inserted in cache";
        }
        public void OnPost()
        {
            // read data from cache
            UserInfo u1 = _cabs.Retrieve<UserInfo>("USER1");
            if (u1 != null)
                Message = "Data Read from Cache = " + u1.UserName + " " + u1.CheckingAccountNumber;
            else
                Message = "No Data found in Cache";
        }

    }
}
