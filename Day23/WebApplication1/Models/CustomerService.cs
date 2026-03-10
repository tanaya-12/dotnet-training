using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

// DbContext
public class CrmDbContext : DbContext
{
    public CrmDbContext(DbContextOptions<CrmDbContext> options)
        : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}

// Model
public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
}



// Interface
public interface ICustomerService
{
    IEnumerable<CustomerDto> GetAllCustomers();
}

// Service
public class CustomerService : ICustomerService
{
    private readonly CrmDbContext dbContext;
    private readonly IMapper mapper;

    public CustomerService(CrmDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public IEnumerable<CustomerDto> GetAllCustomers()
    {
        var customers = dbContext.Customers.ToList();

        return mapper.Map<List<CustomerDto>>(customers);
    }
}
