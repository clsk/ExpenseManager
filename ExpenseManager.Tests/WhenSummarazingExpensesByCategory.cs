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
* Given: That the user has registered expenses
* When: Summarizing expense by category
* Then: Get All expenses sorted by category
*/

namespace ExpenseManager.Tests
{
    public class WhenSummarazingExpensesByCategory
    {
        [TestMethod]
        public void shouldGetAllExpensesSortedByCategory()
        {
            // Create categories manually
            Category gasCategory  = new Category { Name = "Gas" };       
            Category foodCategory = new Category { Name = "Food"};
            Category gymCategory = new Category { Name = "Gym" };
            CategoryRepository categoryRepository = new CategoryRepository();
            categoryRepository.Add(gasCategory);
            categoryRepository.Add(foodCategory);
            categoryRepository.Add(gymCategory);

            // Create expenses manually
            ExpenseRepository expenseRepository = new ExpenseRepository();
            var gas0 = new Expense { Amount = 55.43, Category = gasCategory};
            gas0.ExpenseId = expenseRepository.Add(gas0);
            var gas1 = new Expense { Amount = 23.43, Category = gasCategory};
            gas1.ExpenseId = expenseRepository.Add(gas1);

            var food0 = new Expense { Amount = 88.11, Category = foodCategory};
            food0.ExpenseId = expenseRepository.Add(food0);
            var food1 = new Expense { Amount = 55.11, Category = foodCategory};
            food0.ExpenseId = expenseRepository.Add(food1);

            var gym0 = new Expense { Amount = 44.93, Category = gymCategory};
            gym0.ExpenseId = expenseRepository.Add(gym0);
            var gym1 = new Expense { Amount = 12.93, Category = gymCategory};
            gym1.ExpenseId = expenseRepository.Add(gym1);


            var interaction = new SummarizeExpensesInteraction<RAMRepository.ExpenseRepository>(repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);

            Dictionary<CategoryResponse, <int, ExpenseResponse>> expenses = response.Expenses;

            Assert.AreEqual(shouldGetAllExpensesSortedByCategory.Count, 3);
            Assert.IsTrue(expenses[gasCategory].ContainsKey(gas0.ExpenseId));
            Assert.AreEqual(expenses[gasCategory][gas0.ExpenseId].Amount, gas0.Amount);
            Assert.IsTrue(expenses[gasCategory].ContainsKey(gas1.ExpenseId));
            Assert.AreEqual(expenses[gasCategory][gas1.ExpenseId].Amount, gas1.Amount);

            Assert.IsTrue(expenses[foodCategory].ContainsKey(food0.ExpenseId));
            Assert.AreEqual(expenses[foodCategory][food0.ExpenseId].Amount, food0.Amount);
            Assert.IsTrue(expenses[foodCategory].ContainsKey(food1.ExpenseId));
            Assert.AreEqual(expenses[foodCategory][food1.ExpenseId].Amount, food1.Amount);

            Assert.IsTrue(expenses[foodCategory].ContainsKey(gym0.ExpenseId));
            Assert.AreEqual(expenses[foodCategory][gym0.ExpenseId].Amount, gym0.Amount);
            Assert.IsTrue(expenses[foodCategory].ContainsKey(gym1.ExpenseId));
            Assert.AreEqual(expenses[foodCategory][gym1.ExpenseId].Amount, gym1.Amount);
        }
    }
}
