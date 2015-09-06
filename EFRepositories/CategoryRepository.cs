using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;


namespace EFRepositories
{
    public class CategoryRepository : Repositories.ICategoryRepository
    {
        public void Add(Category category)
        {
            using (var db = new DBContext())
            {
                try
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateException)
                {
                    throw new Repositories.Exceptions.CategoryAlreadyExistsException();
                }
            }
        }

        public void Remove(Category category)
        {
            throw new NotImplementedException();
        }

        public bool Exists(Category category)
        {
            using (var db = new DBContext())
            {
                return db.Categories.Where(x => x.Name == category.Name).Count() > 0;
            }
        }

        public bool Exists(string name)
        {
            return Exists(new Category { Name = name });
        }

        public List<Category> GetCategories()
        {
            using (var db = new DBContext())
            {
                return db.Categories.ToList();
            }
        }
    }
}
