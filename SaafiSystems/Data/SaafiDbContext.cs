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
        public SaafiDbContext(DbContextOptions<SaafiDbContext> options)
            : base(options)
        { }

    }
}