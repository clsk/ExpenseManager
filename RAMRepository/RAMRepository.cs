using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RAMRepository
{
    public class RAMRepository
    {
        private RAMRepository()
        {
            Expenses = new Dictionary<Category, Dictionary<int, Expense>>(new CategoryEqualityComparer());
        }

        private static RAMRepository sharedInstance;
        public static RAMRepository SharedInstance {
            get
            {
                if (sharedInstance == null)
                {
                    sharedInstance = new RAMRepository();
                }

                return sharedInstance;
            }
        }

        public Dictionary<Category, Dictionary<int, Expense>> Expenses;
    }
}
