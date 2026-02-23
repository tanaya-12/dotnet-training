using Microsoft.EntityFrameworkCore;

public class CustomerManagementDB : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Segment> Segments { get; set; }
    public DbSet<ContactPerson> ContactPersons { get; set; }
    public DbSet<CustomerAddress> CustomerAddress { get; set; }
    public DbSet<CustomerInteraction> CustomerInteraction { get; set; }
    public DbSet<CustomerAudit> CustomerAudit { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost;Database=CustomerManagementDB;User Id=sa;Password=p@ssw0rd;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Soft delete filter
        modelBuilder.Entity<Customer>()
            .HasQueryFilter(c => c.IsDeleted == false);

        // Customer → Segment
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Segment)
            .WithMany(s => s.Customers)
            .HasForeignKey(c => c.SegmentId);

        // ContactPerson → Customer
        modelBuilder.Entity<ContactPerson>()
            .HasOne(cp => cp.Customer)
            .WithMany(c => c.ContactPersons)
            .HasForeignKey(cp => cp.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // CustomerAddress → Customer
        modelBuilder.Entity<CustomerAddress>()
            .HasOne(a => a.Customer)
            .WithMany(c => c.Addresses)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        // CustomerInteraction → Customer
        modelBuilder.Entity<CustomerInteraction>()
            .HasOne(i => i.Customer)
            .WithMany(c => c.Interactions)
            .HasForeignKey(i => i.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
