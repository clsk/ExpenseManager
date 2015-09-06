using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Entities;

namespace Interactions
{
    public class AddExpenseInteraction<RepositoryType> :
        AbstractInteraction<RequestModels.AddExpense, ResponseModels.IDResponse, RepositoryType> where RepositoryType : IExpenseRepository
    {
        public AddExpenseInteraction(RequestModels.AddExpense requestModel, RepositoryType repository) : base(requestModel, repository)
        {
        }

        public override RepositoryType Repository { get; set; }

        public override void performAction()
        {
            if (RequestModel.Amount <= 0)
            {
                ResponseModel = new ResponseModels.IDResponse(new ResponseModels.DefaultResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.EXPENSE_AMOUNT_MUST_BE_POSITIVE, Message = "Expense Must Be a Positive Number"}));
            }
            else
            {
                try
                {
                    Expense expense = new Expense { Amount = RequestModel.Amount, Category = new Category { Name = RequestModel.Category }, Date = DateTime.Today.Date};
                    ResponseModel = new ResponseModels.IDResponse(Repository.Add(expense));
                }
                catch (Repositories.Exceptions.CategoryDoesNotExistException)
                {
                    ResponseModel = new ResponseModels.IDResponse (new ResponseModels.DefaultResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.CATEGORY_DOES_NOT_EXIST, Message = "Category Does Not Exist" }));
                }
            }
        }
    }

    public class AddExpenseInteraction : AddExpenseInteraction<EFRepositories.ExpenseRepository> 
    { 
        public AddExpenseInteraction(RequestModels.AddExpense requestModel) : 
            base(requestModel, new EFRepositories.ExpenseRepository())
        {
        }
    }

}
