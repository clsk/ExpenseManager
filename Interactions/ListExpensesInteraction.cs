using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Entities;

namespace Interactions
{
    public class ListExpensesInteraction<RepositoryType> :
        AbstractInteraction<RequestModels.ListExpenses, ResponseModels.ExpenseListResponse, RepositoryType> where RepositoryType : IExpenseRepository
    {
        public ListExpensesInteraction(RequestModels.ListExpenses requestModel, RepositoryType repository) : base(requestModel, repository)
        {
        }

        public override RepositoryType Repository { get; set; }

        public override void performAction()
        {
            if (RequestModel.From > RequestModel.To)
            {
                ResponseModel = new ResponseModels.ExpenseListResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.DATE_RANGE_INVALID, Message = "Dates are Inverted" });
            }
            else
            {
                var expenseResponseList = Repository.GetExpenses(RequestModel.From, RequestModel.To)
                    .Select(x => new  ResponseModels.ExpenseResponse(x)).ToList();
                ResponseModel = new ResponseModels.ExpenseListResponse(expenseResponseList);
            }
        }
    }

    public class ListExpensesInteraction : ListExpensesInteraction<EFRepositories.ExpenseRepository> 
    { 
        public ListExpensesInteraction(RequestModels.ListExpenses requestModel) : 
            base(requestModel, new EFRepositories.ExpenseRepository())
        {
        }
    }

}
