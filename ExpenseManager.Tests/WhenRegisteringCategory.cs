using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpenseManager.Interactions;
using ExpenseManager.Interactions.RequestModels;
using ExpenseManager.Interactions.ResponseModels;
/*
 * UC2 - As a user, in order to be able to categorize my expenses I want to
 * be able to register an expense category
 *
 * Given: That the category name doesn't already exist
 * When: The user registers an expense category
 * Then: Check for Bogus Name (only word characters allowed)
 *       and Add the category to the system   
 */

namespace ExpenseManager.Tests
{
    [TestClass]
    public class WhenRegisteringCategory
    {
        private RAMDBGateway DB { get; set; }
        private static readonly string CategoryName = "Gas";
        [TestInitialize]
        public void initialize()
        {
            DB = new RAMDBGateway();
        }

        [TestMethod]
        public void shouldCheckForBogusName()
        {
            var interaction = new AddCategory(Interactions.RequestModels.AddCategory(CategoryName));
            var response = interaction.performInteraction();
            Assert.IsNotNull(response.error);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.error.Code, Interactions.ResponseModels.Error.Codes.BOGUS_CATEGORY_NAME);
        }

        [TestMethod]
        public void shouldAddCategoryToSystem()
        {
            var interaction = new AddCategory(Interactions.RequestModels.AddCategory(CategoryName));
            var response = interaction.performInteraction();
            Assert.IsNull(response.error);
            Assert.IsNotNull(DB.GetCategoryByName(CategoryName));
        }

        [TestMethod]
        public void shouldNotAlreadyExist()
        {
            string categoryName = "Gas";
            var interaction = new AddCategory(Interactions.RequestModels.AddCategory(categoryName));
            var response = interaction.performInteraction();
            Assert.IsNull(response.error);
            Assert.AreEqual<Interactions.ResponseModels.Error.Codes>(response.error.Code, Interactions.ResponseModels.Error.Codes.CATEGORY_ALREADY_EXISTS);
        }
    }
}
