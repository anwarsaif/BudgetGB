using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    //this Controller already exist in HR area
    public class SysDepartmentController : Controller
    {
        public SysDepartmentController()
        {

        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
