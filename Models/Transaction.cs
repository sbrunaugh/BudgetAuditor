using System;
using System.Collections.Generic;
using System.Text;

namespace BudgetAutomator.Models
{
    public class Transaction
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
    }
}
