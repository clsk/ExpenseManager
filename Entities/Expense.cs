using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Expense
    {
        public Expense() { }
        public int ExpenseId { get; set; }
        public virtual Category Category { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Expense) 
            {
                return ExpenseId == ((Expense)obj).ExpenseId
                    && Category.Name == ((Expense)obj).Category.Name
                    && Amount == ((Expense)obj).Amount
                    && Date == ((Expense)obj).Date;
            }
            return base.Equals(obj);
        }
    }
}
