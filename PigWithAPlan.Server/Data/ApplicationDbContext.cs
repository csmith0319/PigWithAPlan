using Microsoft.EntityFrameworkCore;
using PigWithAPlan.Server.Models;

namespace PigWithAPlan.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Budget> Budgets { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Payee> Payees { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<CategoryGroup> CategoryGroups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new BudgetConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new PayeeConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryGroupConfiguration());
        }
    }
}
