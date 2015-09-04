using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.ResponseModels
{
    public class ExpensesSummaryResponse : DefaultResponse
    {
        public ExpensesSummaryResponse(DefaultResponse baseResponse)
            : base(baseResponse.Error)
        {
            Expenses = null;
        }

        public ExpensesSummaryResponse(Dictionary<string, double> expenses) : base(null)
        {
            Expenses = expenses;
        }

        public Dictionary<string, double> Expenses;
    }
}
