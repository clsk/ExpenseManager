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
            try 
            {
                Expenses.Add(category, new Dictionary<int, Expense>());
            } 
            catch(ArgumentException) 
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
            return Expenses.ContainsKey(category);
        }

        public bool Exists(string name)
        {
            return Exists(new Category { Name = name });
        }

        public List<Category> GetCategories()
        {
            return Expenses.Keys.ToList();
        }

        private static Dictionary<Category, Dictionary<int, Expense>> Expenses { get { return RAMRepository.SharedInstance.Expenses; } }
    }
}
