using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Exceptions
{
    public class CategoryDoesNotExistException : Exception
    {
        public CategoryDoesNotExistException() : base("Category Does Not Exists") { }
    }
}
