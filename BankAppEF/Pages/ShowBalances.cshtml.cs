using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppEF.Models;
using BankAppEF.ServicesBusiness;
using BankAppEF.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAppEF.Pages
{
    public class ShowBalancesModel : PageModel
    {
        IBusinessBanking _IBusinessBanking = null;
        public ShowBalancesModel(IBusinessBanking businessBanking)
        {
            _IBusinessBanking = businessBanking;
        }

        public decimal CheckingBalance { get; set; }
        public decimal SavingBalance { get; set; }
        public IActionResult OnGet()
        {
            if(SessionFacade.USERINFO == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                UserInfo userInfo = SessionFacade.USERINFO;
                CheckingBalance = _IBusinessBanking.GetCheckingBalance(userInfo.CheckingAccountNumber);
                SavingBalance = _IBusinessBanking.GetSavingBalance(userInfo.SavingAccountNumber);
            }
            return Page();
        }
    }
}
