using System;

namespace DomainModelingExample
{
    // Sentence 1: The customer places an order.

    public class Customer
    {
        public string CustomerName { get; set; }
        public string OrderName { get; set; }

        public void PlaceOrder()
        {
            Console.WriteLine($"{CustomerName} placed an order: {OrderName}");
        }
    }

    // Sentence 2: The system processes the payment.

    public class PaymentSystem
    {
        public decimal PaymentAmount { get; set; }

        public void ProcessPayment()
        {
            Console.WriteLine($"Payment of {PaymentAmount} is processed successfully.");
        }
    }

    // Sentence 3: The admin updates the inventory.

    public class Admin
    {
        public string InventoryItem { get; set; }

        public void UpdateInventory()
        {
            Console.WriteLine($"Inventory updated for item: {InventoryItem}");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Sentence 1 execution
            Customer customer = new Customer();
            customer.CustomerName = "Tanaya";
            customer.OrderName = "Laptop";
            customer.PlaceOrder();

            // Sentence 2 execution
            PaymentSystem payment = new PaymentSystem();
            payment.PaymentAmount = 50000;
            payment.ProcessPayment();

            // Sentence 3 execution
            Admin admin = new Admin();
            admin.InventoryItem = "Laptop Stock";
            admin.UpdateInventory();
        }
    }
}
