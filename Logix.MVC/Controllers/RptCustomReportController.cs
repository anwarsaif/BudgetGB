using Logix.Application.Common;
using Logix.Application.DTOs.RPT;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.RPT;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class RptCustomReportController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IRptServiceManager rptServiceManager;
        private readonly IPermissionHelper permission;
        private readonly ILocalizationService localization;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly IFilesHelper filesHelper;
        private readonly IWebHostEnvironment env;

        public RptCustomReportController(IRptServiceManager rptServiceManager,
            IMainServiceManager mainServiceManager,
            IPermissionHelper permission,
            ILocalizationService localization,
            IDDListHelper listHelper,
            ISessionHelper session,
            IFilesHelper filesHelper,
            IWebHostEnvironment env)
        {
            this.mainServiceManager = mainServiceManager;
            this.rptServiceManager = rptServiceManager;
            this.permission = permission;
            this.localization = localization;
            this.listHelper = listHelper;
            this.session = session;
            this.filesHelper = filesHelper;
            this.env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index(RptCustomReportDto filter)
        {
            var chk = await permission.HasPermission(1042, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            var model = new SearchVM<RptCustomReportDto, RptCustomReportDto>(filter, new List<RptCustomReportDto>());
            try
            {
                var items = await rptServiceManager.RptCustomReportService.GetAll(r => r.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }

                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        res = res.Where(r => r.Name != null && r.Name.Contains(filter.Name));
                    }
                    if (!string.IsNullOrEmpty(filter.Name2))
                    {
                        res = res.Where(r => r.Name2 != null && r.Name2.ToLower().Contains(filter.Name2.ToLower()));
                    }
                    model.ListModel = res.ToList();
                    if (!model.ListModel.Any())
                        TempData.AddSuccessMessage($"{localization.GetResource1("NosearchResult")}");

                    foreach (var item in model.ListModel)
                    {
                        var screenData = await mainServiceManager.SysScreenService.GetById(item.ScreenId ?? 0);
                        if (screenData.Succeeded && screenData != null)
                            item.ScreenName = screenData.Data.ScreenName;
                    }
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

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var chk = await permission.HasPermission(1042, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            SetErrors();
            await GetDDl(true, "Tselect");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RptCustomReportDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, "Tselect");
                if (!ModelState.IsValid)
                    return View(obj);

                var add = await rptServiceManager.RptCustomReportService.Add(obj);
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

        [HttpGet]
        public async Task<ActionResult> Edit(string encId)
        {
            try
            {
                var chk = await permission.HasPermission(1042, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                await GetDDl(false);
                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction(nameof(Index));
                }

                long Id = EncryptionHelper.Decrypt<long>(encId);
                var getReport = await rptServiceManager.RptCustomReportService.GetForUpdate<RptCustomReportEditDto>(Id);
                if (getReport.Succeeded)
                {
                    var obj = new RptCustomReportEditDto();
                    obj = getReport.Data;

                    //get systemId & parentId to be selected in select lists
                    var screen = await mainServiceManager.SysScreenService.GetById(obj.ScreenId);
                    if (screen != null)
                    {
                        obj.SystemId = screen.Data.SystemId;
                        obj.ParentId = screen.Data.ParentId;
                    }
                    return View(obj);
                }
                else
                {
                    TempData.AddErrorMessage($"{getReport.Status.message}");
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(RptCustomReportEditDto obj)
        {
            SetErrors();
            await GetDDl(false);
            if (!ModelState.IsValid)
                return View(obj);
            try
            {
                var update = await rptServiceManager.RptCustomReportService.Update(obj);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.Id) });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return View(obj);
                }
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(obj);
            }
        }

        public async Task<ActionResult> Delete(long Id = 0)
        {
            if (Id == 0)
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var chk = await permission.HasPermission(1042, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                var delete = await rptServiceManager.RptCustomReportService.Remove(Id);
                if (delete.Succeeded)
                    TempData.AddSuccessMessage("success");
                else
                    TempData.AddErrorMessage($"{delete.Status.message}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetParentScreens(string systemId)
        {
            var parentScreensData = await mainServiceManager.SysScreenService.GetAll(s => s.SystemId == Convert.ToInt32(systemId) && s.ParentId == s.ScreenId && s.Isdel == false);
            var parentScreens = listHelper.GetFromList<long>(parentScreensData.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId??0 }), hasDefault: true, defaultText: "Tselect");
            return Json(new { parentScreens });
        }

        [HttpGet]
        public async Task<IActionResult> GetChildScreens(string ParentId)
        {
            var childScreensData = await mainServiceManager.SysScreenService.GetAll(s => s.ParentId != s.ScreenId && s.ParentId == Convert.ToInt32(ParentId) && s.Isdel == false);
            var childScreens = listHelper.GetFromList<long>(childScreensData.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId??0 }), hasDefault: true, defaultText: "Tselect");
            return Json(new { childScreens });
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            int language = session.Language;
            var ddlvm = new DDLViewModel();

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (language == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);

            var parentScreensData = await mainServiceManager.SysScreenService.GetAll(s => s.ParentId == s.ScreenId && s.Isdel == false);
            var ddParentScreensList = listHelper.GetFromList<long>(parentScreensData.Data.Select(s => new DDListItem<long> { Name = (language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddParentScreensList), ddParentScreensList);

            var childScreensData = await mainServiceManager.SysScreenService.GetAll(s => s.ParentId != s.ScreenId && s.Isdel == false);
            var ddChildScreensList = listHelper.GetFromList<long>(childScreensData.Data.Select(s => new DDListItem<long> { Name = (language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddChildScreensList), ddChildScreensList);

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
