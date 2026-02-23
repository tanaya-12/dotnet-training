using Xunit;
using MyApp;

namespace MyApp.Tests
{
    public class CalculatorTests
    {
        [Fact]
        public void Add_Test()
        {
            var calc = new Calculator();
            Assert.Equal(15, calc.Add(5, 10));
        }

        [Fact]
        public void Subtract_Test()
        {
            var calc = new Calculator();
            Assert.Equal(5, calc.Subtract(10, 5));
        }

        [Fact]
        public void Multiply_Test()
        {
            var calc = new Calculator();
            Assert.Equal(20, calc.Multiply(4, 5));
        }

        [Fact]
        public void Divide_Test()
        {
            var calc = new Calculator();
            Assert.Equal(2, calc.Divide(12, 6));
        }

        [Fact]
        public void Divide_ByZero_ThrowsException()
        {
            var calc = new Calculator();
            Assert.Throws<DivideByZeroException>(() => calc.Divide(10, 0));
        }
    }
}