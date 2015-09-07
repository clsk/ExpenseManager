using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repositories;
using Entities;
namespace Interactions
{
    public class SummarizeExpensesInteraction<RepositoryType> :
        AbstractInteraction<RequestModels.SummarizeExpenses, ResponseModels.ExpensesSummaryResponse, RepositoryType> where RepositoryType : IExpenseRepository
    {
        public SummarizeExpensesInteraction(RequestModels.SummarizeExpenses requestModel, RepositoryType repository) : base(requestModel, repository)
        {
        }

        public override RepositoryType Repository { get; set; }

        public override void performAction()
        {
            if (RequestModel.From > RequestModel.To)
            {
                ResponseModel = new ResponseModels.ExpensesSummaryResponse(new ResponseModels.DefaultResponse(new ResponseModels.Error { Code = ResponseModels.Error.Codes.DATE_RANGE_INVALID, Message = "Dates are Inverted" }));
            }
            else
            {
                ResponseModel = new ResponseModels.ExpensesSummaryResponse(Repository.GetExpenseSummaryByCategory(RequestModel.From, RequestModel.To));
            }
        }
    }

    public class SummarizeExpensesInteraction : SummarizeExpensesInteraction<EFRepositories.ExpenseRepository> 
    { 
        public SummarizeExpensesInteraction(RequestModels.SummarizeExpenses requestModel) : 
            base(requestModel, new EFRepositories.ExpenseRepository())
        {
        }
    }
}
