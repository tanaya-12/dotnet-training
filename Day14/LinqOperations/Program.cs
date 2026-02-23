using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations.Schema;

using (var _context = new CrmDbContext())
{
    // 1️⃣ Customers who have placed at least one order
    var customersWithOrders = _context.Orders
                                      .Select(o => o.Customer)
                                      .Distinct()
                                      .ToList();

    Console.WriteLine("Customers With Orders:");
    foreach (var c in customersWithOrders)
    {
        Console.WriteLine(c.Name);
    }

    // 2️⃣ Total Revenue
    var totalRevenue = _context.Orders.Sum(o => o.TotalAmount);
    Console.WriteLine($"\nTotal Revenue: {totalRevenue}");

    // 3️⃣ Customer with Maximum Total Purchase
    var topCustomer = _context.Orders
                              .GroupBy(o => o.Customer)
                              .Select(g => new
                              {
                                  Customer = g.Key,
                                  TotalSpent = g.Sum(o => o.TotalAmount)
                              })
                              .OrderByDescending(x => x.TotalSpent)
                              .FirstOrDefault();

    if (topCustomer != null)
    {
        Console.WriteLine($"\nTop Customer: {topCustomer.Customer.Name}, Amount: {topCustomer.TotalSpent}");
    }

    // 4️⃣ Orders placed in last 30 days
    var recentOrders = _context.Orders
                               .Where(o => o.OrderDate >= DateTime.Now.AddDays(-30))
                               .ToList();

    Console.WriteLine("\nRecent Orders (Last 30 Days):");
    foreach (var order in recentOrders)
    {
        Console.WriteLine($"Order ID: {order.OrderId}, Amount: {order.TotalAmount}");
    }

    // 5️⃣ Customers who have NO orders
    var customersWithoutOrders = _context.Customers
                                         .Where(c => !_context.Orders
                                             .Any(o => o.CustomerId == c.CustomerId))
                                         .ToList();

    Console.WriteLine("\nCustomers Without Orders:");
    foreach (var c in customersWithoutOrders)
    {
        Console.WriteLine(c.Name);
    }
}
