using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interactions.RequestModels
{
    public struct AddExpense
    {
        public double Amount { get; set; }
        public string Category { get; set; }
    }
}
