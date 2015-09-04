using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public interface IExpenseRepository
    {
        int Add(Expense expense);
        void Remove(Expense expense);
        Expense GetExpenseById(int id);
        Dictionary<string, double> GetExpenseSummaryByCategory(DateTime from, DateTime to);
        List<Expense> GetExpenses(DateTime from, DateTime to);
    }
}
