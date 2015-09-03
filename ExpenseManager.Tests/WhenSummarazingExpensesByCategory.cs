using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interactions;
using Interactions.RequestModels;
using Interactions.ResponseModels;
using RAMRepository;
using Entities;

/*
* UC3 - As a user, in order to know what things I spent my money in, I
* want to see a summary especifying how much I spent in each expense
* category in a period of time.
*
* Given: A time range
* When: Summarizing expense by category
* Then: Get expenses within date range
*/

namespace ExpenseManager.Tests
{
    public class WhenSummarazingExpensesByCategory
    {
        static Category gasCategory  = new Category { Name = "Gas" };       
        static Category foodCategory = new Category { Name = "Food"};
        static Category gymCategory = new Category { Name = "Gym" };
        static Expense gas0 = new Expense { Amount = 55.43, Category = gasCategory, Date = DateTime.Today};
        static Expense gas1 = new Expense { Amount = 23.43, Category = gasCategory, Date = DateTime.Today.AddDays(2)};
        static Expense food0 = new Expense { Amount = 88.11, Category = foodCategory, Date = DateTime.Today.AddDays(3)};
        static Expense food1 = new Expense { Amount = 55.11, Category = foodCategory, Date = DateTime.Today.AddDays(4)};
        static Expense gym0 = new Expense { Amount = 44.93, Category = gymCategory, Date = DateTime.Today.AddDays(5)};
        static Expense gym1 = new Expense { Amount = 12.93, Category = gymCategory, Date = DateTime.Today.AddDays(6)};

        [ClassInitialize]
        public static void initialize()
        {
            // Create categories manually
            CategoryRepository categoryRepository = new CategoryRepository();
            categoryRepository.Add(gasCategory);
            categoryRepository.Add(foodCategory);
            categoryRepository.Add(gymCategory);

            // Create expenses manually
            ExpenseRepository expenseRepository = new ExpenseRepository();
            gas0.ExpenseId = expenseRepository.Add(gas0);
            gas1.ExpenseId = expenseRepository.Add(gas1);
            food0.ExpenseId = expenseRepository.Add(food0);
            food0.ExpenseId = expenseRepository.Add(food1);
            gym0.ExpenseId = expenseRepository.Add(gym0);
            gym1.ExpenseId = expenseRepository.Add(gym1);
        }

        [ClassCleanup]
        public static void cleanup()
        {
            RAMRepository.RAMRepository.SharedInstance.Expenses.Clear();
        }


        [TestMethod]
        public void shouldGetAllExpensesSortedByCategory()
        {
            ExpenseRepository expenseRepository = new ExpenseRepository();
            SummarizeExpenses request = new SummarizeExpenses { From = DateTime.Today.Date, To = DateTime.Today.Date.AddDays(6) };
            
            var interaction = new SummarizeExpensesInteraction<RAMRepository.ExpenseRepository>(request, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);

            Dictionary<string, int> summary = response.Summary;

            Assert.AreEqual(summary.Count, 3);
            Assert.AreEqual(summary[gasCategory.Name], gas0.Amount + gas1.Amount);
            Assert.AreEqual(summary[gymCategory.Name], gym0.Amount + gym1.Amount);
            Assert.AreEqual(summary[foodCategory.Name], food0.Amount + food1.Amount);
        }

        [TestMethod]
        public void shouldOnlyGetExpensesWithDateRange()
        {
            ExpenseRepository expenseRepository = new ExpenseRepository();
            SummarizeExpenses request = new SummarizeExpenses { From = DateTime.Today.Date.AddDays(1), To = DateTime.Today.Date.AddDays(4) };
            
            var interaction = new SummarizeExpensesInteraction<RAMRepository.ExpenseRepository>(request, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);

            Dictionary<string, int> summary = response.Summary;

            Assert.AreEqual(summary.Count, 3);
            Assert.AreEqual(summary[gasCategory.Name], gas1.Amount);
            Assert.AreEqual(summary[foodCategory.Name], food0.Amount + food1.Amount);
            Assert.AreEqual(summary[gymCategory.Name], gym0.Amount);

        }

    }
}
