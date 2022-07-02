namespace Web.Api._6._0._01.Services
{
    public interface ISenderService
    {
        void SendEmail(string email);
    }

    public class SenderService : ISenderService
    {
        public void SendEmail(string email)
        {
            Console.WriteLine(email);
        }
    }

    public class SenderService2 : ISenderService
    {
        public void SendEmail(string email)
        {
            Console.WriteLine("SIŠI KURAC");
        }
    }
}
