using Microsoft.EntityFrameworkCore;

namespace WebAppEmailSender.Data
{
    public class EmsDbContext : DbContext
    {
        public EmsDbContext(DbContextOptions<EmsDbContext> options) : base(options) { }

        public DbSet<DbEmailSenderInfo> EmailSenderInfo { get; set; }
        public DbSet<DbMailInfo> Mails { get; set; }
    }
}
