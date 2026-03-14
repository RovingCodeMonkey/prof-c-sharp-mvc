using Microsoft.EntityFrameworkCore;
using Web.Models;

namespace Web.Data
{
public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SalesPerson> SalesPersons { get; set; }
    }
}

