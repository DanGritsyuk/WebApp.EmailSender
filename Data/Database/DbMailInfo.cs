using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace WebAppEmailSender.Data
{
    [DataContract]
    public class DbMailInfo
    {
        public DbMailInfo() { }

        public DbMailInfo(string subject, string body, string recipients, string result, string failedMessage, DbEmailSenderInfo emailSender)
        {
            this.Subject = subject;
            this.Body = body;
            this.Recipients = recipients;
            this.CreationDate = DateTime.Now;
            this.Result = result;
            this.FailedMessage = failedMessage;
            this.EmailSender = emailSender;
        }

        [Key]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public string Recipients { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public string Result { get; set; }

        public string FailedMessage { get; set; }

        [Required]
        public DbEmailSenderInfo EmailSender { get; set; }
    }
}
