﻿using System;
using System.Collections.Generic;
using System.Linq;

var unitOfWork = new UnitOfWork();

// Add Customers
unitOfWork.Customers.Add(new Customer { Id = 1, Name = "Tanaya" });
unitOfWork.Customers.Add(new Customer { Id = 2, Name = "Rahul" });
unitOfWork.Customers.Add(new Customer { Id = 3, Name = "Amit" });

// Get Customer by Id
var customer = unitOfWork.Customers.GetById(1);

if (customer != null)
{
    customer.Name = "Aditya";
    Console.WriteLine($"Customer 1 Name: {customer.Name}");
}

// Remove Customer
unitOfWork.Customers.Remove(2);

// Print all customers
Console.WriteLine("\nAll Customers:");
foreach (var c in unitOfWork.Customers.GetAll())
{
    Console.WriteLine($"{c.Id} - {c.Name}");
}

// Commit Changes
unitOfWork.Commit();



/* =========================
   ENTITY
========================= */

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}


/* =========================
   DATABASE CONTEXT
========================= */

public class DatabaseContext
{
    public List<Customer> Customers { get; set; } = new();
}


/* =========================
   REPOSITORY
========================= */

public class CustomerRepository
{
    private readonly DatabaseContext _context;

    public CustomerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Add(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public IEnumerable<Customer> GetAll()
    {
        return _context.Customers;
    }

    public Customer? GetById(int id)
    {
        return _context.Customers.FirstOrDefault(c => c.Id == id);
    }

    public void Remove(int id)
    {
        var customer = _context.Customers.FirstOrDefault(c => c.Id == id);

        if (customer != null)
        {
            _context.Customers.Remove(customer);
        }
    }
}


/* =========================
   UNIT OF WORK
========================= */

public class UnitOfWork
{
    private readonly DatabaseContext _context;

    public CustomerRepository Customers { get; }

    public UnitOfWork()
    {
        _context = new DatabaseContext();
        Customers = new CustomerRepository(_context);
    }

    public void Commit()
    {
        Console.WriteLine("\nChanges committed to database.");
    }
}