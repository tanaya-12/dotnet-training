using System;

namespace Day5
{
    public class Resource
    {
        public string Name { get; set; }

        public Resource(string name)
        {
            Name = name;
            Console.WriteLine($"{Name} created");
        }

        ~Resource()
        {
            Console.WriteLine($"{Name} destroyed by GC");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Resource r = new Resource("File Resource");

            Console.WriteLine("Program Running...");

            r = null;
            GC.Collect();
            GC.WaitForPendingFinalizers(); // important

            Console.ReadLine();
        }
    }
}
