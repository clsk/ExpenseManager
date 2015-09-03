using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Exceptions
{
    public class CategoryAlreadyExistsException : Exception
    {
        public CategoryAlreadyExistsException() : base("Category Already Exists") { }
    }
}
