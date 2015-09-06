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

/*
 * UC1 - As a user in order to keep track of my money, I want to register
 * all my expenses associating them with an expense category.
 *
 * Given: That the expense category already exists
 * When: The user registers an expense 
 * Then: Get List of available categories
 *       Check that the expense amount is a positive number
 *       and Add the Expense to the system with today's date  
 */


namespace ExpenseManager.Tests
{
    [TestClass]
    public class WhenRegisteringExpense
    {
        static string ExpenseCategory = "Food";
        static double ExpenseAmount = 150.55;

        [TestCleanup]
        public void cleanup()
        {
            RAMRepository.RAMRepository.SharedInstance.Expenses.Clear();
        }

        [TestMethod]
        public void shouldGetListOfAvailableCategories()
        {
            CategoryRepository repository = new CategoryRepository();
            repository.Add(new Entities.Category { Name = "Gas" });
            repository.Add(new Entities.Category { Name = "Food" });
            repository.Add(new Entities.Category { Name = "Gym" });

            var interaction = new ListCategoriesInteraction<RAMRepository.CategoryRepository>(repository);
            interaction.performAction();
            var categories = interaction.ResponseModel.Categories;

            Assert.IsTrue(categories.Contains("Gas"));
            Assert.IsTrue(categories.Contains("Food"));
            Assert.IsTrue(categories.Contains("Gym"));
        }

        [TestMethod]
        public void shouldCheckExpenseCategoryExists()
        {
            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = ExpenseAmount, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_DOES_NOT_EXIST);
        }

        [TestMethod]
        public void shouldCheckExpenseAmountIsPositiveNumber()
        {
            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = -44.56, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.EXPENSE_AMOUNT_MUST_BE_POSITIVE);
        }

        [TestMethod]
        public void shouldAddExpenseToSystemWithCurrentDate()
        { 
            // Add category manully
            CategoryRepository categoryRepository = new CategoryRepository();
            categoryRepository.Add(new Entities.Category { Name = ExpenseCategory });


            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = ExpenseAmount, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);
            Entities.Expense expense = repository.GetExpenseById(response.Id);
            Assert.IsNotNull(expense);
            Assert.AreEqual<DateTime>(expense.Date, DateTime.Today.Date);
        }
    }
}
