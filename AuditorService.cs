using System;
using System.Collections.Generic;
using System.Text;
using BudgetAutomator.Models;
using IronXL;

namespace BudgetAutomator
{
    public class AuditorService
    {
        private string _budgetFile;
        private string _bankFile;
        private EnumHelper _enumHelper;

        public AuditorService(string budgetFile, string bankFile)
        {
            _budgetFile = budgetFile;
            _bankFile = bankFile;
            _enumHelper = new EnumHelper();
        }

        public List<Receipt> auditMoneyOutSheet(int days)
        {
            var result = new List<Receipt>();
            
            WorkBook budgetWorkBook = WorkBook.Load(_budgetFile);
            WorkSheet moneyOutSheet = budgetWorkBook.WorkSheets[1];

            WorkBook transactionWorkBook = WorkBook.LoadCSV(_bankFile);
            WorkSheet transactionsSheet = transactionWorkBook.WorkSheets[0];
            
            int currentRow = moneyOutSheet.RowCount;
            var cutoffDate = DateTime.Now.DayOfYear - days;
            Boolean toggle = true;
            
            while (toggle)
            {
                var currentReceipt = GetReceiptFromRow(moneyOutSheet, currentRow);
                var transactions = GetTransactionsFromSheet(transactionsSheet);

                if (!ReceiptExistsInTransactions(currentReceipt, transactions))
                {
                    result.Add(currentReceipt);
                }

                currentRow--;

                //If the current receipt's date is less than the cutoff date
                if (DateTime.Parse(currentReceipt.date).DayOfYear < cutoffDate)
                {
                    toggle = false;
                }
            }

            return result;
        }

        private Receipt GetReceiptFromRow(WorkSheet sheet, int row)
        {
            if (sheet.RowCount < row)
            {
                throw new ArgumentException();
            }

            return new Receipt()
            {
                rowNumber = row,
                vendor = sheet["A" + row.ToString()].StringValue,
                description = sheet["B" + row.ToString()].StringValue,
                amount = sheet["C" + row.ToString()].DoubleValue,
                date = sheet["D" + row.ToString()].StringValue,
                category = _enumHelper.CategoryFromStr(sheet["E" + row.ToString()].StringValue),
                method = _enumHelper.MethodFromStr(sheet["F" + row.ToString()].StringValue),
                amountSeen = 0
            };
        }
    
        private List<Transaction> GetTransactionsFromSheet(WorkSheet sheet)
        {
            var result = new List<Transaction>();

            for (int i = 2; i <= sheet.RowCount; i++)
            {
                result.Add(new Transaction()
                {
                    Date = sheet["A" + i.ToString()].StringValue,
                    Type = sheet["B" + i.ToString()].StringValue,
                    Description = sheet["C" + i.ToString()].StringValue,
                    Category = String.Empty,
                    Amount = sheet["E" + i.ToString()].DoubleValue,
                    Balance = sheet["F" + i.ToString()].DoubleValue
                });
            }

            return result;
        }
    
        private Boolean ReceiptExistsInTransactions(Receipt receipt, List<Transaction> transactions)
        {
            var result = false;

            transactions.ForEach(x =>
            {
                if (Math.Abs(x.Amount) == receipt.amount && areDatesCloseEnough(x.Date, receipt.date))
                {
                    result = true;
                }
                else if (Math.Abs(x.Amount) == receipt.amount && x.Description.Contains(receipt.vendor))
                {
                    result = true;
                }
            });

            return result;
        }

        private Boolean areDatesCloseEnough(string smallDate, string largeDate)
        {
            var sDate = DateTime.Parse(smallDate);
            var lDate = DateTime.Parse(largeDate);

            if (Math.Abs(sDate.Date.DayOfYear - lDate.DayOfYear) < 4)
            {
                return true;
            }

            return false;
        }
    }
}
