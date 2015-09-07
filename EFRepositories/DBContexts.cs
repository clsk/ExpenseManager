using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;

namespace EFRepositories
{
    internal class DBContext : DbContext
    {
        public DbSet<Entities.Category> Categories { get; set; }
        public DbSet<Entities.Expense> Expenses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasKey(c => c.Name);
            modelBuilder.Entity<Expense>().HasRequired(expense => expense.Category).WithOptional();
        }
    }
}
