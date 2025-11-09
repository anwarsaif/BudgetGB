using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Linq;

namespace Logix.MVC.Controllers
{
    public class SysUserLogTimeController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysUserLogTimeController(IMainServiceManager mainServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            ILocalizationService localization,
            ISessionHelper session,
            IFilesHelper filesHelper)
        {
            this.mainServiceManager = mainServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.session = session;
            this.localization = localization;
        }

        //عداد الدخول
        public async Task<IActionResult> Index(SysUserLogTimeSearchVm filter)
        {
            
            var model = new SearchVM<SysUserLogTimeSearchVm, SysUserLogTimeVm>(filter, new List<SysUserLogTimeVm>());
            try
            {
                var chk = await permission.HasPermission(101, PermissionType.Show);
                if (!chk)
                    return View("AccessDenied");

                await GetDDl(true, "all");
                var items = await mainServiceManager.SysUserLogTimeService.GetAllVW();
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();

                    if (filter.UserTypeId > 0)
                        res = res.Where(u => u.UserTypeId > 0 && u.UserTypeId == filter.UserTypeId);

                    if (filter.LoginTime != null)
                        res = res.Where(u => u.LoginTime != null && u.LoginTime >= DateTime.ParseExact(filter.LoginTime, "yyyy/MM/dd", CultureInfo.InvariantCulture));

                    if (filter.LogoutTime != null)
                        res = res.Where(u => u.LoginTime != null && u.LoginTime <= DateTime.ParseExact(filter.LogoutTime, "yyyy/MM/dd", CultureInfo.InvariantCulture));

                    var x = res.GroupBy(l => l.UserFullname).Select(g => new
                    {
                        USER_FULLNAME = g.Key,
                        Num_of_entries = g.Count()
                    }).ToList();

                    List<SysUserLogTimeVm> results = new List<SysUserLogTimeVm>();
                    foreach (var item in x)
                    {
                        SysUserLogTimeVm result = new SysUserLogTimeVm()
                        {
                            UserFullName = item.USER_FULLNAME,
                            NumOfEntries = item.Num_of_entries
                        };
                        results.Add(result);
                    }
                    model.ListModel = results;
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

        //المتواجدون حاليا
        public async Task<IActionResult> CurrentUsers(SysUserLogTimeVw filter)
        {
            var model = new SearchVM<SysUserLogTimeVw, SysUserLogTimeVw>(filter, new List<SysUserLogTimeVw>());
            try
            {
                var chk = await permission.HasPermission(92, PermissionType.Show);
                if (!chk)
                    return View("AccessDenied");

                await GetDDl(true, "all");

                var items = await mainServiceManager.SysUserLogTimeService.GetAllVW(u => u.Offline == false);
                if (items.Succeeded)
                {
                    var res = items.Data.DistinctBy(u => u.UserFullname).AsQueryable();

                    if (filter.UserTypeId > 0)
                        res = res.Where(u => u.UserTypeId == filter.UserTypeId);

                    model.ListModel = res.ToList();
                    if (!model.ListModel.Any())
                        TempData.AddSuccessMessage($"{localization.GetResource1("NosearchResult")}");
                    return View("CurrentUsers", model);
                }

                TempData.AddErrorMessage($"{items.Status.message}");
                return View("CurrentUsers", model);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View("CurrentUsers", model);
            }
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            var ddlvm = new DDLViewModel();

            var userTypes = await mainServiceManager.SysUserLogTimeService.GetAllUserTypes();
            var ddUserTypeList = listHelper.GetFromList<int>(userTypes.Data.Select(c => new DDListItem<int> { Name = c.UserTypeName ?? "", Value = c.UserTypeId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddUserTypeList), ddUserTypeList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

    }
}
