using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysFormsController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly IFilesHelper filesHelper;

        public SysFormsController(IMainServiceManager mainServiceManager,
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
        public async Task<IActionResult> Index(SysFormDto filter)
        {
            var chk = await permission.HasPermission(329, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "all");
            var model = new SearchVM<SysFormDto, SysFormsVw>(filter, new List<SysFormsVw>());
            try
            {
                var items = await mainServiceManager.SysFormService.GetAllVW(t => t.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();

                    if (!string.IsNullOrEmpty(filter.Name1))
                        res = res.Where(t => (t.Name1 != null && t.Name1.Contains(filter.Name1)) || (t.Name2 != null && t.Name2.ToLower().Contains(filter.Name1.ToLower())));

                    if (filter.SystemId > 0)
                        res = res.Where(t => t.SystemId != null && t.SystemId.Equals(filter.SystemId));

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
                var chk = await permission.HasPermission(329, PermissionType.Add);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                await GetDDl(true, "Tselect");

                return View();
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysFormDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, "Tselect");
                if (!ModelState.IsValid)
                    return View(obj);

                //if (obj.FileUploaded != null && obj.FileUploaded.Length > 0)
                //{
                //    if (obj.FileUploaded.ContentType == "application/pdf")
                //    {
                //        var addFile = await filesHelper.SaveFile(obj.FileUploaded, "AllFiles/Forms");
                //        obj.Url = addFile;
                //    }
                //    else
                //    {
                //        TempData.AddErrorMessage("خطأ الملف ليس بصيغة PDF");
                //        return View(obj);
                //    }
                //}

                var add = await mainServiceManager.SysFormService.Add(obj);
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
                var chk = await permission.HasPermission(329, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction("Index");
                }
                long id = EncryptionHelper.Decrypt<long>(encId);

                var getItem = await mainServiceManager.SysFormService.GetForUpdate<SysFormEditDto>(id);
                if (getItem.Succeeded)
                {
                    var obj = getItem.Data;
                    SetErrors();
                    await GetDDl(false);
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
        public async Task<ActionResult> Edit(SysFormEditDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(false);
                if (!ModelState.IsValid)
                    return View(obj);

                //if (obj.FileUploaded != null && obj.FileUploaded.Length > 0)
                //{
                //    if (obj.FileUploaded.ContentType == "application/pdf")
                //    {
                //        var addFile = await filesHelper.SaveFile(obj.FileUploaded, "AllFiles/Forms");
                //        obj.Url = addFile;
                //    }
                //    else
                //    {
                //        TempData.AddErrorMessage("خطأ الملف ليس بصيغة PDF");
                //        return View(obj);
                //    }
                //}

                var update = await mainServiceManager.SysFormService.Update(obj);
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
            try
            {
                var chk = await permission.HasPermission(329, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysFormService.Remove(Id);
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
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            int language = session.Language;
            var ddlvm = new DDLViewModel();

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (language == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);

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
