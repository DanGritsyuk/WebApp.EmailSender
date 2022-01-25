using System.Collections.Generic;
using WebAppEmailSender.Data;

namespace WebAppEmailSender.Models
{
    /// <summary>
    /// Модель представления
    /// </summary>
    public class MailsViewModel
    {
        public MailsViewModel(List<DbMailInfo> mails = null)
        {
            if (mails == null) this.IsSending = true;
            else this.Mails = mails;
        }

        public List<DbMailInfo> Mails { get; set; }
        public bool IsSending { get; set; }
    }
}
