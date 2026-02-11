using System;

public class Calculator
{
    public int CalculateTotal(int[] numbers)
    {
        int total = 0;

        // Local function
        void Sum()
        {
            foreach (var n in numbers)
            {
                total += n;   // Captured variables: numbers and total
            }
        }

        Sum();
        return total;
    }
}

public class Program
{
    public static void Main()
    {
        Calculator calc = new Calculator();

        int[] nums = { 10, 20, 30, 40 };

        int result = calc.CalculateTotal(nums);

        Console.WriteLine("Total Sum: " + result);
    }
}
