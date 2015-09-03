using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        void Remove(Category category);
        bool Exists(Category category);
        bool Exists(string name);
    }
}
