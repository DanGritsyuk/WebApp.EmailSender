using System.Threading.Tasks;

namespace WebAppEmailSender.EmailService
{
    public interface IEmailSender
    {
        Task<string> SendEmailAsync(Message message);
    }
}
