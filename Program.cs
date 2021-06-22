using System;
using IronXL;
using System.Linq;
using BudgetAutomator.Models;

namespace BudgetAutomator
{
    class Program
    {
        static void Main(string[] args)
        {
            string budgetFile = args[0];
            if (budgetFile == null || budgetFile == String.Empty)
            {
                throw new ArgumentException();
            }

            AuditorService auditorService = new AuditorService(budgetFile);
            auditorService.auditMoneyOutSheet();

            Console.WriteLine("Done auditing");
        }
    }
}
