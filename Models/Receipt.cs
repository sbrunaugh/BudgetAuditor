using System;
using System.Collections.Generic;
using System.Text;
using BudgetAutomator.Enums;

namespace BudgetAutomator.Models
{
    public class Receipt
    {
        public string vendor { get; set; }
        public string description { get; set; }
        public double amount { get; set; }
        public string date { get; set; }
        public Category category { get; set; }
        public Method method { get; set; }
        public double amountSeen { get; set; }

    }
}
