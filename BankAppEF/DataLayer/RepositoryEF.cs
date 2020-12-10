using BankAppEF.Models;
using BankAppEF.Models.ViewModels;
using BankAppEF.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.DataLayer
{
    public class RepositoryEF : IRepositoryAuthentication, IRepositoryBanking
    {
        MYBANKContext _dbContext = null;

        public RepositoryEF(DbContext dbContext)
        {
            _dbContext = dbContext as MYBANKContext;
        }
        public long GetCheckingAccountNumberForUser(string username)
        {
            try
            {
                var checkingAccountNumber = (from row in _dbContext.CheckingAccounts
                                             where row.Username == username
                                             select row.CheckingAccountNumber).FirstOrDefault<long>();
                return checkingAccountNumber;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public decimal GetCheckingBalance(long checkingAccountNumber)
        {
            try
            {
                var balance = (from row in _dbContext.CheckingAccounts
                               where row.CheckingAccountNumber == checkingAccountNumber
                               select row.Balance).FirstOrDefault<decimal>();
                return balance;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public long GetSavingAccountNumberForUser(string username)
        {
            try
            {
                var savingAccountNumber = (from row in _dbContext.SavingAccounts
                                           where row.Username == username
                                           select row.SavingAccountNumber).FirstOrDefault<long>();
                return savingAccountNumber;
            }
            catch
            {
                throw;
            }
        }

        public decimal GetSavingBalance(long savingAccountNumber)
        {
            try
            {
                var balance = (from row in _dbContext.SavingAccounts
                                            where row.SavingAccountNumber == savingAccountNumber
                                            select row.Balance).FirstOrDefault<decimal>();
                return balance;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<TransactionHistoryVM> GetTransactionHistory(long checkingAccountNumber)
        {
            try
            {
                List<TransactionHistoryVM> transHistories = null;
                string checkAcctNum = SessionFacade.USERINFO.CheckingAccountNumber.ToString();
                transHistories = CacheAbstractionHelper.CABS.Retrieve<List<TransactionHistoryVM>>("TRHISTORY_"+checkAcctNum);
                if(transHistories != null)
                {
                    return transHistories;
                }
                var res = (from transactionHistoryRecord in _dbContext.TransactionHistories
                           join transactionTypeRecord in _dbContext.TransactionTypes on
                           transactionHistoryRecord.TransactionTypeId equals transactionTypeRecord.TransactionTypeId
                           where transactionHistoryRecord.CheckingAccountNumber == checkingAccountNumber
                           select new TransactionHistoryVM
                           {
                               CheckingAccountNumber = transactionHistoryRecord.CheckingAccountNumber,
                               SavingAccountNumber = transactionHistoryRecord.SavingAccountNumber,
                               Amount = transactionHistoryRecord.Amount,
                               TransactionFee = transactionHistoryRecord.TransactionFee,
                               TransactionTypeName = transactionTypeRecord.TransactionTypeName,
                               TransactionDate = transactionHistoryRecord.TransactionDate,
                           }).ToList<TransactionHistoryVM>();

                CacheAbstractionHelper.CABS.Insert("TRHISTORY_" + checkAcctNum, res);
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public UserInfo GetUserInfo(string username)
        {
            UserInfo userInfo = null;
            try
            {
                long checkingAccountNumber = 0;
                checkingAccountNumber = (from row in _dbContext.CheckingAccounts
                                         where row.Username == username
                                         select row.CheckingAccountNumber).FirstOrDefault<long>();
                if(checkingAccountNumber > 0)
                {
                    long savingAccountNumber = (from row in _dbContext.SavingAccounts
                                                where row.Username == username
                                                select row.SavingAccountNumber).FirstOrDefault<long>();
                    userInfo = new UserInfo();
                    userInfo.UserName = username;
                    userInfo.CheckingAccountNumber = checkingAccountNumber;
                    userInfo.SavingAccountNumber = savingAccountNumber;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return userInfo;
        }

        public bool TransferCheckingToSaving(long checkingAccountNumber, long savingAccountNumber, decimal amount, decimal transactionFee)
        {
            bool success = false;
            using (var dbContextTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Taking out money from checking account
                    var checkingAccountRow = (from row in _dbContext.CheckingAccounts
                                           where row.CheckingAccountNumber == checkingAccountNumber
                                           select row).FirstOrDefault<CheckingAccounts>();
                    if (checkingAccountRow != null)
                    {
                        checkingAccountRow.Balance -= amount;
                    }
                    _dbContext.SaveChanges();

                    // Checking if after taking account money, our checking balance is good

                    var checkingBalance = (from row in _dbContext.CheckingAccounts
                                           where row.CheckingAccountNumber == checkingAccountNumber
                                           select row.Balance).FirstOrDefault<decimal>();
                    if(checkingBalance < 0)
                    {
                        throw new Exception("Insufficient funds in checking account");
                    }

                    var savingAccountRow = (from row in _dbContext.SavingAccounts
                                              where row.SavingAccountNumber == savingAccountNumber
                                              select row).FirstOrDefault<SavingAccounts>();
                    if (savingAccountRow != null)
                    {
                        savingAccountRow.Balance += amount; 
                    }

                    _dbContext.SaveChanges();

                    TransactionHistories transactions = new TransactionHistories();
                    transactions.CheckingAccountNumber = checkingAccountNumber;
                    transactions.SavingAccountNumber = savingAccountNumber;
                    transactions.Amount = amount;
                    transactions.TransactionTypeId = 100;
                    transactions.TransactionFee = transactionFee;
                    transactions.TransactionDate = DateTime.Now;
                    _dbContext.TransactionHistories.Add(transactions);
                    _dbContext.SaveChanges();

                    dbContextTransaction.Commit();
                    success = true;
                }
                catch (Exception)
                {
                    dbContextTransaction.Rollback();
                    throw;
                }
            }
            return success;
        }

        public bool TransferSavingToChecking(long savingAccountNumber, long checkingAccountNumber, decimal amount, decimal transactionFee)
        {
            throw new NotImplementedException();
        }
    }
}
