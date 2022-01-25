using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading.Tasks;

namespace WebAppEmailSender.EmailService
{
    public class EmailSender : IEmailSender
    {
        /// <summary>
        /// Конструктор отправителя принимает настройки SMPT-сервера
        /// </summary>
        /// <param name="emailConfig">настройки SMPT-сервера</param>
        public EmailSender(SmptConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        private readonly SmptConfiguration _emailConfig;

        /// <summary>
        /// Асинхронная отправка письма на указанные адреса
        /// </summary>
        /// <param name="message">Письмо, содержащее необходимые параметры</param>
        /// <returns>Возвращает отчет об отправки. В случае успеха - "OK", иначе - текст ошибки</returns>
        public async Task<string> SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);
            return await SendAsync(emailMessage);
        }

        private MimeMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.From));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:red;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private async Task<string> SendAsync(MimeMessage mailMessage)
        {
            var answer = string.Empty;

            using (var client = new SmtpClient())
            {
                try
                {
                    await client.ConnectAsync(_emailConfig.SmtpServer, _emailConfig.Port, true);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    await client.AuthenticateAsync(_emailConfig.UserName, _emailConfig.Password);
                    await client.SendAsync(mailMessage);

                    answer = "OK";
                }
                catch(Exception ex)
                {
                    answer = ex.Message;
                }
                finally
                {
                    await client.DisconnectAsync(true);
                    client.Dispose();
                }
            }

            return answer;
        }
    }
}
