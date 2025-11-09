using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysUserTrackingController : Controller
    {
        private readonly IFilesHelper filesHelper;

        public SysUserTrackingController(IFilesHelper filesHelper)
        {
            this.filesHelper = filesHelper;
        }
        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
