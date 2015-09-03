using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace RAMRepository
{
    class CategoryEqualityComparer : EqualityComparer<Category>
    {
        public override bool Equals(Category x, Category y)
        {
            return x.Name.Equals(y.Name);
        }

        public override int GetHashCode(Category obj)
        {
            return obj.Name.GetHashCode();
        }
    }

    public class CategoryRepository : Repositories.ICategoryRepository
    {
        public CategoryRepository()
        {
        }

        public void Add(Category category) 
        {
            if (false == Categories.Add(category))
            {
                throw new Repositories.Exceptions.CategoryAlreadyExistsException();
            }

        }

        public void Remove(Category category)
        {
            throw new NotImplementedException();
        }
        public bool Exists(Category category)
        {
            return Categories.Contains(category);
        }

        public bool Exists(string name)
        {
            return Exists(new Category { Name = name });
        }

        private static HashSet<Category> Categories { get { return RAMRepository.SharedInstance.Categories; } }
    }
}
