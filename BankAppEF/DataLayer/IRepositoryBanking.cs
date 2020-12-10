using BankAppEF.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.DataLayer
{
    public interface IRepositoryBanking
    {
        decimal GetCheckingBalance(long checkingAccountNumber);
        decimal GetSavingBalance(long savingAccountNumber);
        long GetCheckingAccountNumberForUser(string username);
        long GetSavingAccountNumberForUser(string username);
        bool TransferCheckingToSaving(long checkingAccountNumber, long savingAccountNumber, decimal amount, decimal transactionFee);
        bool TransferSavingToChecking(long savingAccountNumber, long checkingAccountNumber, decimal amount, decimal transactionFee);
        List<TransactionHistoryVM> GetTransactionHistory(long checkingAccountNumber);
    }
}
