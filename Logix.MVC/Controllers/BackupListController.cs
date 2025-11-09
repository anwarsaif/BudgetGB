using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class BackupListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
