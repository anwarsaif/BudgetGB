using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysSettingExportController : Controller
    {
        private readonly IFilesHelper filesHelper;

        public SysSettingExportController(IFilesHelper filesHelper)
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
            setErrors();
            return View();
        }
    }
}
