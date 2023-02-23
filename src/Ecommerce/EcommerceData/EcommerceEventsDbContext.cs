using Microsoft.EntityFrameworkCore;

namespace Ecommerce.EntityFramework
{
    public class EcommerceEventsDbContext : DbContext
    {
        public DbSet<EventRecord> Events { get; set; }

        public DbSet<ProjectionEventRecord> ProjectionsEvents { get; set; }

        public EcommerceEventsDbContext() : base() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventRecord>().HasKey(x => x.Id);
            modelBuilder.Entity<EventRecord>().HasIndex(r => r.EventId);

            modelBuilder.Entity<ProjectionEventRecord>().HasKey(x => x.Id);
            modelBuilder.Entity<ProjectionEventRecord>().HasIndex(r => r.EventId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "EcommerceDb");
        }
    }
}