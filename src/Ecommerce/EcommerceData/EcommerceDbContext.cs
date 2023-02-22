using Ecommerce.EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.EntityFramework
{
    public class EcommerceDbContext : DbContext
    {
        public DbSet<ProductEfModel> Products { get; set; }
        public DbSet<CustomeEfModel> Customers { get; set; }

        public EcommerceDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEfModel>()
                .HasKey(p => p.Sku);

            modelBuilder.Entity<CustomeEfModel>()
                .HasKey(c => c.Id);

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EcommerceDb");
        }
    }
}