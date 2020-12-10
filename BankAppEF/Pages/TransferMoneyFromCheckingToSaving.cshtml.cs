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
    public class TransferMoneyFromSavingsToCheckingModel : PageModel
    {
        IBusinessBanking _IBusinessBanking = null;

        public TransferMoneyFromSavingsToCheckingModel(IBusinessBanking businessBanking)
        {
            _IBusinessBanking = businessBanking;
        }

        public decimal CheckingBalance { get; set; }
        public decimal SavingBalance { get; set; }

        [BindProperty]
        public decimal TransferAmount { get; set; }
        public string Message { get; set; }

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
                Message = "";
                TransferAmount = 0;
            }
            return Page();
            
        }


        /// <summary>
        /// A user will not be able to post on this page unless authorized.
        /// </summary>
        public IActionResult OnPost()
        {

            if (SessionFacade.USERINFO == null)
            {
                return RedirectToPage("/Account/Login", new { area = "Identity" });
            }


            UserInfo userInfo = SessionFacade.USERINFO;
            bool success = _IBusinessBanking.TransferCheckingToSaving(userInfo.CheckingAccountNumber, 
                userInfo.SavingAccountNumber, TransferAmount);

            if (success)
            {
                CheckingBalance = _IBusinessBanking.GetCheckingBalance(userInfo.CheckingAccountNumber);
                SavingBalance = _IBusinessBanking.GetSavingBalance(userInfo.SavingAccountNumber);
                Message = "Transfer succeeded...";
                string key = string.Format("TRHISTORY_{0}", userInfo.CheckingAccountNumber);
                CacheAbstractionHelper.CABS.Remove(key);
            }

            return Page();
        }
    }
}
