using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Logix.MVC.Controllers
{
    public class SysTemplateController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly IFilesHelper filesHelper;

        public SysTemplateController(IMainServiceManager mainServiceManager,
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
            this.filesHelper = filesHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SysTemplateDto filter)
        {
            var chk = await permission.HasPermission(399, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");
            await GetDDl(true, filter.SystemId ?? 0, "all");
            var model = new SearchVM<SysTemplateDto, SysTemplateVw>(filter, new List<SysTemplateVw>());
            try
            {
                var items = await mainServiceManager.SysTemplateService.GetAllVW(t => t.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();

                    if (!string.IsNullOrEmpty(filter.Name))
                        res = res.Where(t => (t.Name != null && t.Name.Contains(filter.Name)));

                    if (filter.SystemId > 0)
                        res = res.Where(t => t.SystemId != null && t.SystemId.Equals(filter.SystemId));

                    if (filter.ScreenId > 0)
                        res = res.Where(t => t.ScreenId != null && t.ScreenId.Equals(filter.ScreenId));

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


        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var chk = await permission.HasPermission(399, PermissionType.Add);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                await GetDDl(true, 0, "Tselect");

                return View();
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysTemplateDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, obj.SystemId ?? 0, "Tselect");
                if (!ModelState.IsValid)
                    return View(obj);

                //if type is text, so the details field is required
                if (obj.TypeId == 1 && string.IsNullOrEmpty(obj.Detailes))
                {
                    TempData.AddErrorMessage($"{localization.GetSALResource("Details")}");
                    return View(obj);
                }

                ////if typeId = 2, save upload file
                //if (obj.TypeId == 2 && obj.FileUploaded != null && obj.FileUploaded.Length > 0)
                //{
                //    var addFile = await filesHelper.SaveFile(obj.FileUploaded, "AllFiles");
                //    obj.Url = addFile;
                //}

                var add = await mainServiceManager.SysTemplateService.Add(obj);
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
        public async Task<IActionResult> Edit(string encId)
        {
            try
            {
                var chk = await permission.HasPermission(399, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction("Index");
                }
                int id = EncryptionHelper.Decrypt<int>(encId);

                var getItem = await mainServiceManager.SysTemplateService.GetForUpdate<SysTemplateEditDto>(id);
                if (getItem.Succeeded)
                {
                    var obj = getItem.Data;
                    SetErrors();
                    await GetDDl(false, obj.SystemId ?? 0);
                    return View(obj);
                }
                else
                {
                    TempData.AddErrorMessage($"{getItem.Status.message}");
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
        public async Task<ActionResult> Edit(SysTemplateEditDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(false, obj.SystemId ?? 0);
                if (!ModelState.IsValid)
                    return View(obj);

                if (obj.TypeId == 1 && string.IsNullOrEmpty(obj.Detailes))
                {
                    TempData.AddErrorMessage($"{localization.GetSALResource("Details")}");
                    return View(obj);
                }

                ////if typeId = 2, save upload file
                //if (obj.TypeId == 2 && obj.FileUploaded != null && obj.FileUploaded.Length > 0)
                //{
                //    var addFile = await filesHelper.SaveFile(obj.FileUploaded, "AllFiles");
                //    obj.Url = addFile;
                //}

                var update = await mainServiceManager.SysTemplateService.Update(obj);
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

        public async Task<ActionResult> Delete(int Id = 0)
        {
            try
            {
                var chk = await permission.HasPermission(399, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");
                
                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysTemplateService.Remove(Id);
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


        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, int systemId, string text = "")
        {
            int language = session.Language;
            var ddlvm = new DDLViewModel();

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (language == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);

            if (systemId > 0)
            {
                //because ddScreensList based on systemId, and when the page return after refresh we must keep the data of ddScreensList, so we get the again
                var allScreens = await mainServiceManager.SysScreenService.GetAll(s => s.SystemId == systemId && s.ParentId != s.ScreenId && s.Isdel == false);
                var ddScreensList = listHelper.GetFromList<long>(allScreens.Data.Select(s => new DDListItem<long> { Name = (language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: false);
                ddlvm.AddList(nameof(ddScreensList), ddScreensList);
            }
            else
            {
                //in index view (filter) systemId may be null "all", this code to avoid null screen data for selectList
                var ddScreensList = new SelectList(new List<DDListItem<SysScreen>>());
                ddlvm.AddList(nameof(ddScreensList), ddScreensList);
            }
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreens(string systemId)
        {
            if(systemId == "0")
            {
                var screens = new SelectList(new List<DDListItem<SysScreen>>());
                return Json(new { screens });
            }
            else
            {
                var allScreens = await mainServiceManager.SysScreenService.GetAll(s => s.SystemId == Convert.ToInt32(systemId) && s.ParentId != s.ScreenId && s.Isdel == false);
                var screens = listHelper.GetFromList<long>(allScreens.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: false);
                return Json(new { screens });
            }
        }

        [NonAction]
        private void SetErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
