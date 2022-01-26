using Microsoft.AspNetCore.Mvc;

namespace WebAppEmailSender.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
