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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //If switching database types these need to be updated!!!!!
            modelBuilder.Entity<Product>()
                .Property(e => e.Name)
                .HasColumnType("TEXT COLLATE NOCASE"); //
            modelBuilder.Entity<Customer>()
                .Property(e => e.FirstName)
                .HasColumnType("TEXT COLLATE NOCASE"); //
            modelBuilder.Entity<Customer>()
                .Property(e => e.LastName)
                .HasColumnType("TEXT COLLATE NOCASE"); //
            modelBuilder.Entity<SalesPerson>()
                .Property(e => e.FirstName)
                .HasColumnType("TEXT COLLATE NOCASE"); //
            modelBuilder.Entity<SalesPerson>()
              .Property(e => e.LastName)
              .HasColumnType("TEXT COLLATE NOCASE"); //
            modelBuilder.Entity<SalesPerson>()
              .Property(e => e.Manager)
              .HasColumnType("TEXT COLLATE NOCASE"); //
        }

    }
}

