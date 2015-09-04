using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EFRepositories
{
    internal class DBContext : DbContext
    {
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Expense> Expenses { get; set; }
    }
}
