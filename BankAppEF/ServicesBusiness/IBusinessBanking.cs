using BankAppEF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.ServiceBusiness
{
    public interface IBusinessBanking
    {
        decimal GetCheckingBalance(long checkingAccountNumber);
        decimal GetSavingBalance(long savingAccountNumber);
        long GetCheckingAccountNumberForUser(string username);
        long GetSavingAccountNumberForUser(string username);
        bool TransferCheckingToSaving(long checkingAccountNumber, long savingAccountNumber, decimal amount);
        bool TransferSavingToChecking(long checkingAccountNumber, long savingAccountNumber, decimal amount);
        List<TransactionHistoryVM> GetTransactionHistory(long checkingAccountNumber);
    }
}
