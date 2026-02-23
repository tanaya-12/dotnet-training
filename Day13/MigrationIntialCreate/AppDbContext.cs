using Microsoft.EntityFrameworkCore;

namespace MigrationsDemo
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
