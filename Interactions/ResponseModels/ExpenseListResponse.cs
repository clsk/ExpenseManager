using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class ExpenseListResponse : DefaultResponse
    {
        public ExpenseListResponse(Error error) : base(error)
        {
            Expenses = null;
        }

        public ExpenseListResponse(DefaultResponse baseResponse)
            : base(baseResponse.Error)
        {
            Expenses = null;
        }

        public ExpenseListResponse(List<ExpenseResponse> expenses) : base(null)
        {
            Expenses = expenses;
        }

        public List<ExpenseResponse> Expenses;
    }
}
