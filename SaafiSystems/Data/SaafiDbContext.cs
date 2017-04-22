using SaafiSystems.Models;
using Microsoft.EntityFrameworkCore;
namespace SaafiSystems.Data
{
    public class SaafiDbContext : DbContext
    {
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<RevenueCategory> RevenueCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DbSet<Load> Loads { get; set; }
        public DbSet<LoadCategory> LoadCategories { get; set; }

        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverName> DriverNames { get; set; }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<OwnerLoad> OwnerLoads { get; set; }

        public DbSet<OwnerExpense> OwnerExpenses { get; set; }

        
        public SaafiDbContext(DbContextOptions<SaafiDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OwnerLoad>()
                .HasKey(c => new { c.LoadID, c.OwnerID });
            modelBuilder.Entity<OwnerExpense>()
                .HasKey(c => new { c.ExpenseID, c.OwnerID });
        }

    }
}
