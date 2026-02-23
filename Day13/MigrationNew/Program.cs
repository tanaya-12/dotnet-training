using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmDbContext())
            {
                Console.WriteLine("===== ALL ACTIVE CUSTOMERS =====\n");

                var customers = context.Customers
                                       .Where(c => c.IsDeleted == false)
                                       .Include(c => c.Segment)
                                       .ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Id: {customer.CustomerId}");
                    Console.WriteLine($"Name: {customer.CustomerName}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Phone: {customer.Phone}");
                    Console.WriteLine($"Segment: {customer.Segment?.SegmentName}");
                    Console.WriteLine("----------------------------");
                }

                Console.ReadLine();
            }
        }
    }

    // ================= DB CONTEXT =================
    public class CrmDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Segment> Segments { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<CustomerAddress> CustomerAddresses { get; set; }
        public DbSet<CustomerInteraction> CustomerInteractions { get; set; }
        public DbSet<CustomerAudit> CustomerAudits { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;" +
                "Database=CustomerManagementDB;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary Keys
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
            modelBuilder.Entity<Segment>().HasKey(s => s.SegmentId);
            modelBuilder.Entity<ContactPerson>().HasKey(cp => cp.ContactPersonId);
            modelBuilder.Entity<CustomerAddress>().HasKey(ca => ca.AddressId);
            modelBuilder.Entity<CustomerInteraction>().HasKey(ci => ci.InteractionId);
            modelBuilder.Entity<CustomerAudit>().HasKey(ca => ca.AuditId);

            // Table Names
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Segment>().ToTable("Segment");
            modelBuilder.Entity<ContactPerson>().ToTable("ContactPerson");
            modelBuilder.Entity<CustomerAddress>().ToTable("CustomerAddress");
            modelBuilder.Entity<CustomerInteraction>().ToTable("CustomerInteraction");
            modelBuilder.Entity<CustomerAudit>().ToTable("CustomerAudit");

            // Relationships

            // Customer → Segment
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Segment)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SegmentId);

            // ContactPerson → Customer
            modelBuilder.Entity<ContactPerson>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.ContactPersons)
                .HasForeignKey(cp => cp.CustomerId);

            // CustomerAddress → Customer
            modelBuilder.Entity<CustomerAddress>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Addresses)
                .HasForeignKey(ca => ca.CustomerId);

            // CustomerInteraction → Customer
            modelBuilder.Entity<CustomerInteraction>()
                .HasOne(ci => ci.Customer)
                .WithMany(c => c.Interactions)
                .HasForeignKey(ci => ci.CustomerId);

            // CustomerAudit → Customer
            modelBuilder.Entity<CustomerAudit>()
                .HasOne(ca => ca.Customer)
                .WithMany(c => c.Audits)
                .HasForeignKey(ca => ca.CustomerId);
        }
    }

    // ================= CUSTOMER =================
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? SegmentId { get; set; }
        public bool IsDeleted { get; set; }

        public Segment? Segment { get; set; }

        public ICollection<ContactPerson>? ContactPersons { get; set; }
        public ICollection<CustomerAddress>? Addresses { get; set; }
        public ICollection<CustomerInteraction>? Interactions { get; set; }
        public ICollection<CustomerAudit>? Audits { get; set; }
    }

    // ================= SEGMENT =================
    public class Segment
    {
        public int SegmentId { get; set; }
        public string? SegmentName { get; set; }
        public string? Description { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }

    // ================= CONTACT PERSON =================
    public class ContactPerson
    {
        public int ContactPersonId { get; set; }
        public int CustomerId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public bool IsPrimary { get; set; }

        public Customer? Customer { get; set; }
    }

    // ================= CUSTOMER ADDRESS =================
    public class CustomerAddress
    {
        public int AddressId { get; set; }
        public int CustomerId { get; set; }
        public string? AddressType { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public bool IsDeleted { get; set; }

        public Customer? Customer { get; set; }
    }

    // ================= CUSTOMER INTERACTION =================
    public class CustomerInteraction
    {
        public int InteractionId { get; set; }
        public int CustomerId { get; set; }
        public string? InteractionType { get; set; }
        public string? Subject { get; set; }
        public string? Details { get; set; }
        public DateTime InteractionDate { get; set; }

        public Customer? Customer { get; set; }
    }

    // ================= CUSTOMER AUDIT =================
    public class CustomerAudit
    {
        public int AuditId { get; set; }
        public int CustomerId { get; set; }
        public string? ChangedField { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangedDate { get; set; }

        public Customer? Customer { get; set; }
    }
}
