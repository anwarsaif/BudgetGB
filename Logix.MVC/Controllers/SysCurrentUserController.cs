using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysCurrentUserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
