using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysUserController : Controller
    {
        private readonly IFilesHelper filesHelper;

        public SysUserController(IFilesHelper filesHelper)
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
        public IActionResult Add()
        {
            setErrors();
            return View();
        }
        public IActionResult Edit()
        {
            setErrors();
            return View();
        }
        //الأوقات المسموح به للدخول للنظام
        public IActionResult LoginTime()
        {
            setErrors();
            return View();
        }
        //الصلاحيات على خصائص الشاشات

        public IActionResult ScreenProperty()
        {
            return View();
        }
        //اعدادات التحقق بخطوتين
        public IActionResult TwofactorControl()
        {
           
            return View();
        }
    }
}
