using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysVatGroupController : Controller
    {
        private readonly IPermissionHelper permission;

        public SysVatGroupController(IPermissionHelper permission)
        {
            this.permission = permission;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Add()
        {
            setErrors();
            var chk = await permission.HasPermission(765, PermissionType.Add);
            if (!chk)
            {
                return View("AccessDenied");
            }


            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
