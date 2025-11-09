
using Microsoft.AspNetCore.Mvc;
using Logix.MVC.Helpers;
using Logix.Application.DTOs.HR;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Common;
using Logix.Domain.HR;
using Logix.MVC.ViewModels;
using Logix.Domain.Main;
using Logix.Application.DTOs.Main;

namespace Logix.MVC.Controllers
{
    public class HrEmployeeController : Controller
    {
        private readonly IHrServiceManager hrServiceManager;
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly IFilesHelper filesHelper;

        public HrEmployeeController(IHrServiceManager hrServiceManager,
            IMainServiceManager mainServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            ILocalizationService localization,
            ISessionHelper session,
            IFilesHelper filesHelper)
        {
            this.hrServiceManager = hrServiceManager;
            this.mainServiceManager = mainServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.session = session;
            this.localization = localization;
            this.filesHelper = filesHelper;
        }

        public async Task<IActionResult> Index(HrEmployeeDto filter)
        {
            var chk = await permission.HasPermission(934, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "all");
            var model = new SearchVM<HrEmployeeDto, HrEmployeeVw>(filter, new List<HrEmployeeVw>());
            try
            {
                var items = await hrServiceManager.HrEmployeeService.GetAllVW(e => e.IsDeleted == false && e.IsSub == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }

                    if (filter.BranchId > 0)
                        res = res.Where(r => r.BranchId != null && r.BranchId.Equals(filter.BranchId));
                    else
                    {
                        //get branchsId from session,, this field in sys_use save like that "1,2,5,..."
                        var curUser = session.GetData<SysUser>("user");
                        var arr = curUser.BranchsId.Split(',');
                        res = res.Where(r => r.BranchId != null && arr.Any(x => x == r.BranchId.ToString()));
                    }

                    if (!string.IsNullOrEmpty(filter.EmpName))
                    {
                        res = res.Where(c => (c.EmpName != null && c.EmpName.Contains(filter.EmpName)) || (c.EmpName2 != null && c.EmpName2.ToLower().Contains(filter.EmpName.ToLower())));
                    }

                    if (!string.IsNullOrEmpty(filter.EmpId))
                    {
                        res = res.Where(r => r.EmpId.Equals(filter.EmpId));
                    }

                    res = res.OrderBy(e => e.EmpId);
                    model.ListModel = res.ToList();
                    if (!model.ListModel.Any())
                        TempData.AddSuccessMessage($"{localization.GetResource1("NosearchResult")}");
                    return View("Index", model);
                }
                TempData.AddErrorMessage($"{items.Status.message}");
                return View("Index", model);
            }

            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View("Index", model);
            }
        }


        public async Task<IActionResult> Add()
        {
            var chk = await permission.HasPermission(934, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            SetErrors();
            await GetDDl(true, "Tselect");
            //return view with instance of HrEmployeeDto, to take initial value of chkNumbering variable (make it true => checked)
            return View(new InvestEmployeeDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(InvestEmployeeDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, "Tselect");
                if (!ModelState.IsValid)
                    return View(obj);

                //chek if not auto numbering
                if (obj.AutoNumbering == false && string.IsNullOrEmpty(obj.EmpId))
                {
                    TempData.AddErrorMessage("ادخل رقم الموظف");
                    return View(obj);
                }

                var add = await mainServiceManager.InvestEmployeeService.Add(obj);
                if (add.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Add");
                }
                else
                {
                    TempData.AddErrorMessage($"{add.Status.message}");
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(obj);
            }
        }


        public IActionResult Edit()
        {
            SetErrors();
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            var ddlvm = new DDLViewModel();

            //get branchsId from session,, this field in sys_use save like that "1,2,5,..."
            var cucurUser = session.GetData<SysUser>("user");
           // var arr = cucurUser.BranchsId.Split(',');

            var branches = await mainServiceManager.InvestBranchService.GetAll(b => b.Isdel == false);
           // var branches = await mainServiceManager.InvestBranchService.GetAll(b => arr.Any(x => x == b.BranchId.ToString()) && b.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<long>(branches.Data.Select(b => new DDListItem<long> { Name = (session.Language == 1) ? b.BraName : b.BraName2, Value = b.BranchId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        private void SetErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
