using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");

            modelBuilder.Entity<Customer>().HasKey(c => c.Id);

            modelBuilder.Entity<Customer>()
                .Property(c => c.Id)
                .HasColumnName("Id");

            modelBuilder.Entity<Customer>()
                .Property(c => c.Name)
                .HasColumnName("Name");

            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasColumnName("Email");
        }
    }
}