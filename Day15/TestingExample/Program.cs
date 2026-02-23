using System;

namespace SimpleAdditionTest
{
    class Program
    {
        // Method to add two numbers
        static int Add(int a, int b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            // Test cases
            RunTests();
        }

        static void RunTests()
        {
            // Test case 1: 5 + 10 = 15 (Pass scenario)
            int result1 = Add(5, 10);
            if (result1 == 15)
                Console.WriteLine("Test 1 Passed");
            else
                Console.WriteLine("Test 1 Failed");

            // Test case 2: -5 + 10 = 5 (Pass scenario)
            int result2 = Add(-5, 10);
            if (result2 == 5)
                Console.WriteLine("Test 2 Passed");
            else
                Console.WriteLine("Test 2 Failed");

            // Test case 3: 2 + 2 = 5 (Fail scenario)
            int result3 = Add(2, 2);
            if (result3 == 5)
                Console.WriteLine("Test 3 Passed");
            else
                Console.WriteLine("Test 3 Failed");
        }
    }
}