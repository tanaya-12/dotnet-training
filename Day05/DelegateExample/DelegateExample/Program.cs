
using System;

namespace DelegateExamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Demo demo = new Demo();

            demo.LambdaExpressionDemo();
            demo.AnonymousMethodDemo();
        }
    }

    // Custom delegate
    delegate int MathOperation(int a, int b);

    class Demo
    {
        // Lambda Expression Example
        public void LambdaExpressionDemo()
        {
            Func<int, int> f;

            f = (int x) => x * x;

            var result = f(5);

            Console.WriteLine($"result: {result}");
        }

        // Anonymous Method Example
        public void AnonymousMethodDemo()
        {
            // Using an anonymous method with a delegate
            MathOperation operation = delegate (int a, int b)
            {
                Console.WriteLine($"The sum of {a} and {b} is: {a + b}");
                return a + b;
            };

            operation(5, 3);
        }
    }
}
