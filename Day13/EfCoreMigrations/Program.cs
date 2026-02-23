using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrmApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmDbContext())
            {
                var customers = context.Customers.ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Customer ID: {customer.CustomerId}, Name: {customer.Name}, Email: {customer.Email}");
                }
            }
        }
    }

    class CrmDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
    "Server=localhost\\SQLEXPRESS;Database=CrmDb;Trusted_Connection=True;TrustServerCertificate=True");

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerType>().HasData(
                new CustomerType { Id = 1, TypeName = "Regular" },
                new CustomerType { Id = 2, TypeName = "Premium" }
            );

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany()
                .HasForeignKey(o => o.CustomerId);
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class CustomerType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
