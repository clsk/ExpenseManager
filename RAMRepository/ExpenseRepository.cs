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

        private static Dictionary<Category, Dictionary<int, Expense>> Expenses { get { return RAMRepository.SharedInstance.Expenses; } }
    }
}
