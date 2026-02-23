namespace MyApp
{
    public class Order
    {
        public string Email { get; set; } = string.Empty;

        public override string ToString()
        {
            return $"Order for {Email}";
        }
    }
}