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
 * UC1 - As a user, in order to review my expenses I want to view a list with
 * my expenses in a given period of time.
 *
 * Given: A valid time range
 * When: Listing expenses
 * Then: Get list of expenses within given time range (inclusive)
 */

namespace ExpenseManager.Tests
{
    [TestClass]
    public class WhenListingExpenses
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
        public static void initialize(TestContext context)
        {
            RAMRepository.RAMRepository.SharedInstance.Expenses.Clear();
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
        public void shouldCheckForInvalidRange()
        {
            ExpenseRepository repository = new ExpenseRepository();
            ListExpenses request = new ListExpenses { From = DateTime.Today.Date, To = DateTime.Today.Date.AddDays(-1) };
            
            var interaction = new ListExpensesInteraction<RAMRepository.ExpenseRepository>(request, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual(Error.Codes.DATE_RANGE_INVALID, response.Error.Value.Code);
        }

        [TestMethod]
        public void shouldGetAllExpenses()
        {
            ExpenseRepository repository = new ExpenseRepository();
            ListExpenses request = new ListExpenses { From = DateTime.Today.Date, To = DateTime.Today.Date.AddDays(6) };
            
            var interaction = new ListExpensesInteraction<RAMRepository.ExpenseRepository>(request, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);

            var expenses = response.Expenses.Select(x => new Expense() { Amount = x.Amount, Date = x.Date, Category = new Category() { Name = x.Category}, ExpenseId = x.ExpenseId }).ToList();

            Assert.AreEqual(expenses.Count, 6);
            Assert.IsTrue(expenses.Contains(gas0));
            Assert.IsTrue(expenses.Contains(gas1));
            Assert.IsTrue(expenses.Contains(food0));
            Assert.IsTrue(expenses.Contains(food1));
            Assert.IsTrue(expenses.Contains(gym0));
            Assert.IsTrue(expenses.Contains(gym1));
        }

        [TestMethod]
        public void shouldOnlyGetExpensesWithInDateRange()
        {
            ExpenseRepository repository = new ExpenseRepository();
            ListExpenses request = new ListExpenses { From = DateTime.Today.Date.AddDays(1), To = DateTime.Today.Date.AddDays(5) };
            
            var interaction = new ListExpensesInteraction<RAMRepository.ExpenseRepository>(request, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);

            var expenses = response.Expenses.Select(x => new Expense() { Amount = x.Amount, Date = x.Date, Category = new Category() { Name = x.Category}, ExpenseId = x.ExpenseId }).ToList();

            Assert.AreEqual(expenses.Count, 4);
            Assert.IsTrue(expenses.Contains(gas1));
            Assert.IsTrue(expenses.Contains(food0));
            Assert.IsTrue(expenses.Contains(food1));
            Assert.IsTrue(expenses.Contains(gym0));
        }
    }
}
