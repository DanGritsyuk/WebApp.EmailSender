using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using WebAppEmailSender.Data;
using WebAppEmailSender.EmailService;
using WebAppEmailSender.Models;

namespace WebAppEmailSender.Controllers
{
    [Route("api")]
    public class EmailSendController :Controller
    {
        public EmailSendController(EmsDbContext context, IEmailSender sender)
        {
            _context = context;
            _emailSender = sender;
        }

        private readonly EmsDbContext _context;
        private readonly IEmailSender _emailSender;

        // GET: api/emails/
        [HttpGet]
        [Route("mails")]
        public FileStreamResult Index()
        {
            var mails = _context.Mails.Where(mail => mail.EmailSender == GetThisEmSender()).OrderBy(mail => mail.CreationDate).ToList();

            string jsonString = JsonSerializer.Serialize(mails);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(jsonString);
            var content = new System.IO.MemoryStream(bytes);

            return File(content, "application/json", "export.json");
        }

        // POST: api/emails/
        [HttpPost]
        [Route("mails")]
        public ViewResult Index([FromBody] MailViewModel email)
        {
            var message = new Message(email.Subject, email.Body, email.Recipients);
            var mailStatus = _emailSender.SendEmailAsync(message);
            var thisEmailSender = GetThisEmSender();

            if (mailStatus.Result == "OK")
            {
                email.Status = "OK";
            }
            else
            {
                email.Status = "Failed";
                email.FailMessage = mailStatus.Result;
            }

            var mailInfo = new DbMailInfo(email.Subject, email.Body, string.Join(", ", email.Recipients), email.Status, email.FailMessage, thisEmailSender);

            _context.Add(mailInfo);
            _context.SaveChanges();

            return View(email);
        }

        private DbEmailSenderInfo GetThisEmSender()
        {
            var ThisSenderId = Guid.Empty;

            try
            {
                ThisSenderId = Guid.Parse(System.IO.File.ReadAllText("App_Data/Config/ThisSender.config"));
            }
            catch
            {
                ThisSenderId = Guid.NewGuid();
                System.IO.File.WriteAllText("App_Data/Config/ThisSender.config", ThisSenderId.ToString());
                _context.Add(new DbEmailSenderInfo(ThisSenderId));
                _context.SaveChanges();
            }

            return _context.EmailSenderInfo.Where(sender => sender.SenderId == ThisSenderId).First();
        }
    }
}
