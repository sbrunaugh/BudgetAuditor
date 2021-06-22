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
        private EnumHelper _enumHelper;

        public AuditorService(string budgetFile)
        {
            _budgetFile = budgetFile;
            _enumHelper = new EnumHelper();
        }

        public void auditMoneyOutSheet()
        {
            WorkBook workBook = WorkBook.Load(_budgetFile);
            WorkSheet sheet = workBook.WorkSheets[1];
            int row = sheet.RowCount - 1;

            Receipt testReceipt = GetReceiptFromRow(sheet, row);

            var asdf = 1;
        }

        private Receipt GetReceiptFromRow(WorkSheet sheet, int row)
        {
            if (sheet.RowCount < row)
            {
                throw new ArgumentException();
            }

            return new Receipt()
            {
                vendor = sheet["A" + row.ToString()].StringValue,
                description = sheet["B" + row.ToString()].StringValue,
                amount = sheet["C" + row.ToString()].FloatValue,
                date = sheet["D" + row.ToString()].StringValue,
                category = _enumHelper.CategoryFromStr(sheet["E" + row.ToString()].StringValue),
                method = _enumHelper.MethodFromStr(sheet["F" + row.ToString()].StringValue),
                amountSeen = 0
            };
        }
    }
}
