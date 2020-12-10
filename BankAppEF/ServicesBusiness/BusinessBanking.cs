using BankAppEF.DataLayer;
using BankAppEF.Models;
using BankAppEF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.ServicesBusiness
{
    public class BusinessBanking:IBusinessBanking
    {
        IRepositoryBanking _bankRepo = null;
        public BusinessBanking(IRepositoryBanking bankRepo)
        {
            _bankRepo = bankRepo;
        }

        public BusinessBanking(MYBANKContext context) : this(new RepositoryEF(context)) { }
        public long GetCheckingAccountNumberForUser(string username)
        {
            return _bankRepo.GetCheckingAccountNumberForUser(username);
        }

        public decimal GetCheckingBalance(long checkingAccountNumber)
        {
            return _bankRepo.GetCheckingBalance(checkingAccountNumber);
        }

        public long GetSavingAccountNumberForUser(string username)
        {
            return _bankRepo.GetSavingAccountNumberForUser(username);
        }

        public decimal GetSavingBalance(long savingAccountNumber)
        {
            return _bankRepo.GetSavingBalance(savingAccountNumber);
        }

        public List<TransactionHistoryVM> GetTransactionHistory(long checkingAccountNumber)
        {
            return _bankRepo.GetTransactionHistory(checkingAccountNumber);
        }

        public bool TransferCheckingToSaving(long checkingAccountNumber, long savingAccountNumber, decimal amount)
        {
            return _bankRepo.TransferCheckingToSaving(checkingAccountNumber, savingAccountNumber, amount, 0);
        }

        public bool TransferSavingToChecking(long checkingAccountNumber, long savingAccountNumber, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}
