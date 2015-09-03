using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Interactions;
using Interactions.RequestModels;
using Interactions.ResponseModels;
using RAMRepository;
/*
 * UC2 - As a user, in order to be able to categorize my expenses I want to
 * be able to register an expense category
 *
 * Given: That the category name doesn't already exist
 * When: The user registers an expense category
 * Then: Check for empty name
 *       Check for Bogus Name (only word characters allowed)
 *       and Add the category to the system   
 */

namespace ExpenseManager.Tests
{
    [TestClass]
    public class WhenRegisteringCategory
    {
        private static readonly string CategoryName = "Gas";

        [TestMethod]
        public void shouldCheckForEmptyName()
        {
            CategoryRepository repository = new CategoryRepository();
            var interaction = new AddCategoryInteraction<RAMRepository.CategoryRepository>(new Interactions.RequestModels.AddCategory { Name = "" }, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_NAME_IS_EMPTY);
        }

        [TestMethod]
        public void shouldCheckForBogusName()
        {
            CategoryRepository repository = new CategoryRepository();
            var interaction = new AddCategoryInteraction<RAMRepository.CategoryRepository>(new Interactions.RequestModels.AddCategory { Name = "Ga$" }, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_BOGUS_NAME);
        }

        [TestMethod]
        public void shouldAddCategoryToSystem()
        {
            CategoryRepository repository = new CategoryRepository();
            var interaction = new AddCategoryInteraction<RAMRepository.CategoryRepository>(new Interactions.RequestModels.AddCategory { Name = CategoryName }, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsFalse(response.Error.HasValue);
            Assert.IsTrue(repository.Exists(CategoryName));
        }

        [TestMethod]
        public void shouldNotAlreadyExist()
        {
            CategoryRepository repository = new CategoryRepository();
            var interaction = new AddCategoryInteraction<RAMRepository.CategoryRepository>(new Interactions.RequestModels.AddCategory { Name = CategoryName }, repository);
            interaction.performAction();
            var response = interaction.ResponseModel;
            Assert.IsTrue(response.Error.HasValue);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.Error.Value.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_ALREADY_EXISTS);
        }
    }
}
