﻿using System;
using System.Collections.Generic;

namespace BankAppEF.Models
{
    public partial class SavingAccounts
    {
        public string Username { get; set; }
        public long SavingAccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
}
