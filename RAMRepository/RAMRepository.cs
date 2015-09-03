using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RAMRepository
{
    internal class RAMRepository
    {
        private RAMRepository()
        {
            Categories = new HashSet<Category>(new CategoryEqualityComparer());
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

        public HashSet<Category> Categories;
    }
}
