using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SendMailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
