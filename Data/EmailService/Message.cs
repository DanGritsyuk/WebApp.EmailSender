using MimeKit;
using System.Collections.Generic;
using System.Linq;

namespace WebAppEmailSender.EmailService
{
    /// <summary>
    /// Письмо
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Конструктор создающий новый экземпляр объекта
        /// </summary>
        /// <param name="subject">Тема письма</param>
        /// <param name="content">Содержание письма</param>
        /// <param name="recipients">Адреса отправления</param>
        public Message(string subject, string content, IEnumerable<string> recipients)
        {
            To = new List<MailboxAddress>();
            To.AddRange(recipients.Select(x => new MailboxAddress(x)));
            Subject = subject;
            Content = content;
        }

        /// <summary>
        /// Адреса отправления
        /// </summary>
        public List<MailboxAddress> To { get; set; }
        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// Содержание письма
        /// </summary>
        public string Content { get; set; }
    }
}
