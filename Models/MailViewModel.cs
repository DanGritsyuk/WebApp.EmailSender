using WebAppEmailSender.Data;

namespace WebAppEmailSender.Models
{
    /// <summary>
    /// Модель представления
    /// </summary>
    public class MailViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string[] Recipients { get; set; }
        public string Status { get; set; }
        public string FailMessage { get; set; }
    }
}