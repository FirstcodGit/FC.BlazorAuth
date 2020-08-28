using Microsoft.EntityFrameworkCore;
using FC.Provider.Models;

namespace FC.Provider
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Customers
            builder.Entity<Customer>().ToTable($"App{nameof(this.Customers)}");
            builder.Entity<Customer>(entity => { entity.HasIndex(t => t.CustomerEmailAddress).IsUnique(); });
            builder.Entity<Customer>(entity => { entity.HasIndex(t => t.CustomerPhoneNumber).IsUnique(); });

        }
    }
}
