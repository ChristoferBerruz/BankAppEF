using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAppEF.Models.ViewModels
{
    public class TransactionHistoryVM:TransactionHistories
    {
        public string TransactionTypeName { get; set; }

    }
}
