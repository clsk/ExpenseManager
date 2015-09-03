using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }
        public Category Category { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
