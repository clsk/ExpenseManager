using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;


namespace EFRepositories
{
    class CategoryRepository : Repositories.ICategoryRepository
    {
        void Add(Category category)
        {
            using (var db = new DBContext())
            {
                db.Categories.Add(category);
                db.SaveChanges();
            }
        }

        void Remove(Category category)
        {
            throw new NotImplementedException();
        }

        bool Exists(Category category)
        {
            using (var db = new DBContext())
            {
                return db.Categories.Where(x => x.Name == category.Name).Count() > 0;
            }
        }

        bool Exists(string name)
        {
            return Exists(new Category { Name = name });
        }

        }

    }
}
