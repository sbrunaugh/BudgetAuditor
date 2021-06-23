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
            string route = "D:\\sourcecode\\repos\\BudgetAutomator\\data\\";

            string budgetFile = args[0];
            if (budgetFile == null || budgetFile == String.Empty)
            {
                throw new ArgumentException();
            }
            budgetFile = route + budgetFile;

            string bankFile = args[1];
            if (bankFile == null || bankFile == string.Empty)
            {
                throw new ArgumentException();
            }
            bankFile = route + bankFile;

            AuditorService auditorService = new AuditorService(budgetFile, bankFile);
            int days = 14;
            var problemReceipts = auditorService.auditMoneyOutSheet(days);

            Console.WriteLine($"See potentially missed receipts below (from the last {days} days):\n");

            foreach(var reciept in problemReceipts)
            {
                var msg = $"{reciept.rowNumber.ToString()} | {reciept.vendor} | ${reciept.amount} | {reciept.date} | {reciept.category}";
                Console.WriteLine(msg);
            }

            Console.WriteLine("\nDone auditing");
        }
    }
}
