using System;
using Xunit;
using MyApp;

namespace MyApp.Tests
{
    public class MockOrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Saved Order to Database {order}");
        }
    }

    public class MockEmailSender : IEmailSender
    {
        public void Send(string email, string message)
        {
            Console.WriteLine($"Email sent to {email}: {message}");
        }
    }

    public class OrderServiceTests : IDisposable
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailSender _emailSender;
        private readonly OrderService _sut;

        public OrderServiceTests()
        {
            _repository = new MockOrderRepository();
            _emailSender = new MockEmailSender();
            _sut = new OrderService(_repository, _emailSender);
        }

        public void Dispose()
        {
            // Cleanup if needed
        }

        [Fact]
        public void OrderService_PlaceOrder_SavesOrderAndSendsEmail()
        {
            var expectedResult = 10;

            var actualResult = _sut.PlaceOrder(
                new Order { Email = "john.doe@orderscompany.com" });

            Assert.Equal(expectedResult, actualResult);
        }
    }
}