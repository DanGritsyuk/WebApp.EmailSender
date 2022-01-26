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
    public class EmailSendController : Controller
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
        public ViewResult Index(string subject, string body, string[] recipients)
        {
            var message = new Message(subject, body, recipients);
            var mailStatus = _emailSender.SendEmailAsync(message);
            var thisEmailSender = GetThisEmSender();

            string result;
            string failedMessage = null;

            if (mailStatus.Result == "OK")
            {
                result = "OK";
            }
            else
            {
                result = "Failed";
                failedMessage = mailStatus.Result;
            }

            var mail = new DbMailInfo(subject, body, string.Join(", ", recipients), result, failedMessage, thisEmailSender);

            _context.Add(mail);
            _context.SaveChanges();

            return View(new StatusViewModel(result, failedMessage));
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
