using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

var _context = new CustomerManagementDB();

Console.WriteLine("----- ALL CUSTOMERS -----");

var customers = _context.Customers.ToList();

foreach (var customer in customers)
{
    Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.CustomerName}, Email: {customer.Email}");
}

Console.WriteLine("\n----- HIGH VALUE CUSTOMERS (> 100000) -----");

var highValueCustomers = _context.Customers
    .Where(c => c.AccountValue > 100000)
    .OrderBy(c => c.CustomerName)
    .ToList();

foreach (var customer in highValueCustomers)
{
    Console.WriteLine($"{customer.CustomerName} - {customer.AccountValue}");
}

Console.WriteLine("\n----- CUSTOMERS FROM PUNE -----");

var puneCustomers = _context.CustomerAddress
    .Where(a => a.City == "Pune")
    .Select(a => a.Customer.CustomerName)
    .Distinct()
    .ToList();

foreach (var name in puneCustomers)
{
    Console.WriteLine(name);
}

Console.WriteLine("\n----- ENTERPRISE SEGMENT CUSTOMERS -----");

var enterpriseCustomers = _context.Customers
    .Where(c => c.Segment.SegmentName == "Enterprise")
    .ToList();

foreach (var customer in enterpriseCustomers)
{
    Console.WriteLine(customer.CustomerName);
}

Console.WriteLine("\n----- PRIMARY CONTACT PERSONS -----");

var primaryContacts = _context.ContactPersons
    .Where(cp => cp.IsPrimary == true)
    .Select(cp => new
    {
        Customer = cp.Customer.CustomerName,
        ContactName = cp.Name
    })
    .ToList();

foreach (var item in primaryContacts)
{
    Console.WriteLine($"Customer: {item.Customer}, Contact: {item.ContactName}");
}

Console.WriteLine("\n----- CUSTOMERS WITH MEETING INTERACTIONS -----");

var meetingCustomers = _context.CustomerInteraction
    .Where(i => i.InteractionType == "Meeting")
    .Select(i => i.Customer.CustomerName)
    .Distinct()
    .ToList();

foreach (var name in meetingCustomers)
{
    Console.WriteLine(name);
}

Console.WriteLine("\n----- SEGMENT SUMMARY -----");

var segmentSummary = _context.Customers
    .GroupBy(c => c.Segment.SegmentName)
    .Select(g => new
    {
        Segment = g.Key,
        TotalCustomers = g.Count(),
        TotalRevenue = g.Sum(c => c.AccountValue)
    })
    .ToList();

foreach (var item in segmentSummary)
{
    Console.WriteLine($"Segment: {item.Segment}, Customers: {item.TotalCustomers}, Revenue: {item.TotalRevenue}");
}

Console.WriteLine("\nDone!");
