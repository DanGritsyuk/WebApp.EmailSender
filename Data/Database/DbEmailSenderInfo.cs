using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace WebAppEmailSender.Data
{
    [DataContract]    
    public class DbEmailSenderInfo
    {
        public DbEmailSenderInfo() { }
        public DbEmailSenderInfo(Guid senderId)
        {
            this.SenderId = senderId;
        }

        [Key]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        [Required]
        public Guid SenderId { get; set; }
    }
}
