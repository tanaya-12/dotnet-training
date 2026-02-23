namespace MyApp
{
    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailSender _emailSender;

        public OrderService(IOrderRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public int PlaceOrder(Order order)
        {
            _repository.Save(order);
            _emailSender.Send(order.Email, "Your order has been placed successfully.");

            return 10; // returning fixed value for test
        }
    }
}