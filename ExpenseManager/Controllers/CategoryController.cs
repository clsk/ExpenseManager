using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Interactions;


namespace ExpenseManager.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            var interaction = new AddCategoryInteraction(new Interactions.RequestModels.AddCategory { Name = Request.Form["Name"] });
            interaction.performAction();
            var response = interaction.ResponseModel;

            return RedirectToAction("Index");
        }

    }
}
