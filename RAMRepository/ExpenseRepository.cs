using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using Repositories.Exceptions;

namespace RAMRepository
{
    public class ExpenseRepository : Repositories.IExpenseRepository 
    {
        public int Add(Expense expense)
        {
            if (!Expenses.ContainsKey(expense.Category))
            {
                throw new CategoryDoesNotExistException();
            }

            Random rnd = new Random();
            int id = 0;
            do 
            {
                id = (int) rnd.Next(1, 10000);
            } while (Expenses[expense.Category].ContainsKey(id) == true);

            Expenses[expense.Category].Add(id, expense);

            return id;
        }

        public void Remove(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Expense GetExpenseById(int id)
        {
            foreach (var categoryExpenses in Expenses)
            {
                foreach (var idExpense in categoryExpenses.Value)
                {
                    if (idExpense.Key == id)
                    {
                        return idExpense.Value;
                    }
                }
            }

            return null;
        }

        public Dictionary<string, double> GetExpenseSummaryByCategory(DateTime from, DateTime to)
        {
            Dictionary<string, double> expensesByCategory = new Dictionary<string,double>();
            foreach (var categoryExpenses in Expenses)
            {
                double summary = Expenses[categoryExpenses.Key]
                    .Where(kvp => kvp.Value.Date.Date >= from && kvp.Value.Date.Date <= to)
                    .Select(idExpense => idExpense.Value.Amount)
                    .Sum();

                expensesByCategory.Add(categoryExpenses.Key.Name, summary);
            }

            return expensesByCategory;
        }


        public List<Expense> GetExpenses(DateTime from, DateTime to)
        {
            return Expenses
                .SelectMany(kvp => kvp.Value.Values)
                .Where(expense => expense.Date.Date >= from && expense.Date.Date <= to).ToList();
        }

        private static Dictionary<Category, Dictionary<int, Expense>> Expenses { get { return RAMRepository.SharedInstance.Expenses; } }
    }
}
