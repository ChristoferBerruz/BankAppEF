using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankAppEF.Models;
using BankAppEF.Models.ViewModels;
using BankAppEF.ServicesBusiness;
using BankAppEF.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BankAppEF.Pages
{
    public class TransactionHistoryModel : PageModel
    {
        IBusinessBanking _IBusinessBanking = null;

        public TransactionHistoryModel(IBusinessBanking businessBanking)
        {
            _IBusinessBanking = businessBanking;
        }

        public List<TransactionHistoryVM> Transactions { get; set; }
        public IActionResult OnGet()
        {
            if(SessionFacade.USERINFO == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }
            else
            {
                UserInfo userInfo = SessionFacade.USERINFO;
                Transactions = _IBusinessBanking.GetTransactionHistory(userInfo.CheckingAccountNumber);
            }
            return Page();
        }
    }
}
