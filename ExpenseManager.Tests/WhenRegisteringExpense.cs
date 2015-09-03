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
 * Then: Check that the expense amount is a positive number
 *       and Add the Expense to the system with today's date  
 */


namespace ExpenseManager.Tests
{
    [TestClass]
    class WhenRegisteringExpense
    {
        string ExpenseCategory = "Gas";
        double ExpenseAmount = 150.55;

        [TestMethod]
        void shouldCheckExpenseCategoryExists()
        {
            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = ExpenseAmount, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_DOES_NOT_EXIST);
        }


        [TestMethod]
        void shouldCheckExpenseAmountIsPositiveNumber()
        {
            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = -44.56, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.EXPENSE_AMOUNT_IS_NEGATIVE);

        }

        [TestMethod]
        void shouldAddExpenseToSystemWithCurrentDate()
        {
            ExpenseRepository repository = new ExpenseRepository();
            var interaction = new AddExpenseInteraction<RAMRepository.ExpenseRepository>(new Interactions.RequestModels.AddExpense { Amount = -44.56, Category = ExpenseCategory}, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);
            Entities.Expense expense = repository.GetExpenseById(response.ExpenseId);
            Assert.IsNotNull(expense);
            Assert.AreEqual<DateTime>(expense.Date, DateTime.Today.Date);
        }
    }
}
