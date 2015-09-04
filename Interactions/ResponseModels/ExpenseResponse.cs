using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class ExpenseResponse : DefaultResponse
    {
        public ExpenseResponse(DefaultResponse baseResponse)
            : base(baseResponse.Error)
        {
            ExpenseId = 0;
            Category = null;
            Amount = 0.0;
            Date = DateTime.MinValue;
        }

        public ExpenseResponse(int expenseId, string category, double amount, DateTime date) : base(null)
        {
            ExpenseId = expenseId;
            Category = category;
            Amount = amount;
            date = Date;
        }

        public ExpenseResponse(Entities.Expense expense) : base(null)
        {
            ExpenseId = expense.ExpenseId;
            Category = expense.Category.Name;
            Amount = expense.Amount;
            Date = expense.Date;
        }

        public int ExpenseId { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
