﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace EFRepositories
{
    public class ExpenseRepository : Repositories.IExpenseRepository 
    {
        public int Add(Expense expense)
        {
            using (var db = new DBContext())
            {
                db.Categories.Attach(expense.Category);
                db.Expenses.Add(expense);
                db.SaveChanges();

                return expense.ExpenseId;
            }
        }

        public void Remove(Expense expense)
        {
            throw new NotImplementedException();
        }

        public Expense GetExpenseById(int id)
        {
            using (var db = new DBContext())
            {
                return db.Expenses.Single(x => x.ExpenseId == id);
            }
        }

        public Dictionary<string, double> GetExpenseSummaryByCategory(DateTime from, DateTime to)
        {
            Dictionary<string, double> expensesByCategory = new Dictionary<string,double>();
            using (var db = new DBContext())
            {
                foreach (var category in db.Categories)
                {
                    double summary = db.Expenses
                        .Where(expense => expense.Date.Date >= from && expense.Date.Date <= to)
                        .Select(expense => expense.Amount)
                        .Sum();
                    expensesByCategory.Add(category.Name, summary);
                }
            }

            return expensesByCategory;
        }


        public List<Expense> GetExpenses(DateTime from, DateTime to)
        {
            using (var db = new DBContext())
            {
                return db.Expenses
                    .Where(expense => expense.Date.Date >= from && expense.Date.Date <= to)
                    .ToList();
            }
        }
    }
}
