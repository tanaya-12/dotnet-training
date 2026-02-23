using System;
using Microsoft.EntityFrameworkCore;

namespace MigrationsDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Application Started...");

            try
            {
                using (var context = new AppDbContext())
                {
                    // Apply pending migrations and create database
                    context.Database.Migrate();
                }

                Console.WriteLine("Database created/updated successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
