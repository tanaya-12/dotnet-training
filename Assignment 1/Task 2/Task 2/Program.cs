using System;
using System.Diagnostics;
using System.Text;
class Program
{
    static void Main()
    {
        const int iterations = 100_000;
        // String concatenation using +
        Stopwatch sw1 = Stopwatch.StartNew();

        string result1 = "";
        for (int i = 0; i < iterations; i++)
        {
            result1 += i;
        }

        sw1.Stop();
        Console.WriteLine($"+ concatenation time: {sw1.ElapsedMilliseconds} ms");

        // StringBuilder

        Stopwatch sw2 = Stopwatch.StartNew();

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < iterations; i++)
        {
            sb.Append(i);
        }

        string result2 = sb.ToString();

        sw2.Stop();
        Console.WriteLine($"StringBuilder time: {sw2.ElapsedMilliseconds} ms");
    }
}
