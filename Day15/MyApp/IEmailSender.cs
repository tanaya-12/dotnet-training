namespace MyApp
{
    public interface IEmailSender
    {
        void Send(string email, string message);
    }
}