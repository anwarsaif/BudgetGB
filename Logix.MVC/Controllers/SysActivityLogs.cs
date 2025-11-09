using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysActivityLogs : Controller
    {
        private readonly IFilesHelper filesHelper;

        public SysActivityLogs(IFilesHelper filesHelper)
        {
            this.filesHelper = filesHelper;
        }
        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        //تحركات المستخدمين
        public IActionResult Index()
        {
            return View();
        }
         //العمليات التي تمت على النظام
        public IActionResult ActivityByTransaction()
        {
            
            return View();
        }
    }
}
