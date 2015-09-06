using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interactions;

namespace ExpenseManager.Controllers
{
    public class ExpenseController : Controller
    {
        //
        // GET: /Expense/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Expense/List
        public ActionResult List()
        {

            return View();
        }


        //
        // GET: /Expense/Create
        public ActionResult Create()
        {
            var listCategoriesInteraction = new ListCategoriesInteraction();
            listCategoriesInteraction.performAction();

            return View(listCategoriesInteraction.ResponseModel);
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            double amount = 0;
            bool amountIsNumber = double.TryParse(Request.Form["Amount"], out amount);
            if (amountIsNumber == false) 
            {
                return RedirectToAction("Index", "Home").Error("Amount is not a number");
            }

            var interaction = new AddExpenseInteraction(new Interactions.RequestModels.AddExpense { Amount = amount, Category = Request.Form["Category"]});
            interaction.performAction();
            var response = interaction.ResponseModel;

            if (response.Error.HasValue)
            {
                return RedirectToAction("Index", "Home").Error(response.Error.Value.Message);
            }

            return RedirectToAction("Index", "Home").Success("Expenses Added Successfully!");
        }
    }
}
