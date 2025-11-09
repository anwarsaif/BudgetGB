using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Logix.MVC.Controllers
{
    public class SysLicenseController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IAccServiceManager accServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly IFilesHelper filesHelper;
        private readonly ILocalizationService localization;
        private readonly ISessionHelper session;

        public SysLicenseController(IMainServiceManager mainServiceManager,
            IAccServiceManager accServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            IFilesHelper filesHelper,
            ILocalizationService localization,
            ISessionHelper session)
        {
            this.mainServiceManager = mainServiceManager;
            this.accServiceManager = accServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.filesHelper = filesHelper;
            this.localization = localization;
            this.session = session;
        }

        // GET: SysLicenseController
        [HttpGet]
        public async Task<ActionResult> Index(SysLicenseDto filter)
        {
            var model = new SearchVM<SysLicenseDto, SysLicensesVw>(filter, new List<SysLicensesVw>());
            try
            {
                var chk = await permission.HasPermission(497, PermissionType.Show);
                if (!chk)
                    return View("AccessDenied");

                var ddl = await GetDDlWithDefault(filter, true, "all");
                var items = await mainServiceManager.SysLicenseService.GetAllVW();
                if (items.Succeeded)
                {
                    var res = items.Data.Where(s => s.IsDeleted == false).OrderBy(s => s.Id).AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }
                    if (filter.FacilityId != null && filter.FacilityId > 0)
                    {
                        res = res.Where(r => r.FacilityId.Equals(filter.FacilityId));
                    }
                    if (filter.LicenseType != null && filter.LicenseType > 0)
                    {
                        res = res.Where(r => r.LicenseType.Equals(filter.LicenseType));
                    }
                    if (filter.BranchId != null && filter.BranchId > 0)
                    {
                        res = res.Where(r => r.BranchId.Equals(filter.BranchId));
                    }
                    if (filter.LicenseNo != null)
                    {
                        res = res.Where(r => r.LicenseNo.Equals(filter.LicenseNo));
                    }
                    if (filter.IssuedDate != null)
                    {
                        //IssuedDate I used it for "Expiry from"
                        res = res.Where(r => DateTime.ParseExact(r.ExpiryDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) >= DateTime.ParseExact(filter.IssuedDate, "yyyy/MM/dd", CultureInfo.InvariantCulture));
                    }
                    if (filter.ExpiryDate != null)
                    {
                        //IssuedDate I used it for "Expiry to"
                        res = res.Where(r => DateTime.ParseExact(r.ExpiryDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) <= DateTime.ParseExact(filter.ExpiryDate, "yyyy/MM/dd", CultureInfo.InvariantCulture));
                    }
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
        public async Task<ActionResult> Add()
        {
            setErrors();
            var chk = await permission.HasPermission(497, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "Tselect");
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Add(SysLicenseDto entity, IFormFile? file)
        {
            setErrors();
            await GetDDl(true, "Tselect");

            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                if (file != null && file.Length > 0)
                {
                    if (!ChkExtension(file))
                        return View(entity);
                    var addFile = await filesHelper.SaveFile(file, "AllFiles");
                    if (!string.IsNullOrEmpty(addFile))
                    {
                        entity.FileUrl = addFile;
                    }
                }
                var addLicense = await mainServiceManager.SysLicenseService.Add(entity);
                if (addLicense.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Add");
                }
                else
                {
                    TempData.AddErrorMessage($"{addLicense.Status.message}");
                    return View(entity);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(entity);
            }
        }


        // GET: SysLicenseController/Edit/5
        public async Task<ActionResult> Edit(string encId)
        {
            try
            {
                setErrors();
                var chk = await permission.HasPermission(497, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");
                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction(nameof(Index));
                }
                long Id = EncryptionHelper.Decrypt<long>(encId);
                await GetDDl(false);
                var obj = new SysLicenseDto();
                var getLicense = await mainServiceManager.SysLicenseService.GetById(Id);

                if (getLicense.Succeeded)
                    obj = getLicense.Data;
                else
                {
                    TempData.AddErrorMessage($"{getLicense.Status.message}");
                    return RedirectToAction("Index");
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        // POST: SysLicenseController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SysLicenseDto entity, IFormFile? file)
        {
            try
            {
                setErrors();
                var ddl = await GetDDl(false);
                if (!ModelState.IsValid)
                    return View(entity);

                if (file != null && file.Length > 0)
                {
                    if (!ChkExtension(file))
                        return View(entity);
                    var addFile = await filesHelper.SaveFile(file, "AllFiles");
                    if (!string.IsNullOrEmpty(addFile))
                    {
                        entity.FileUrl = addFile;
                    }
                }

                var update = await mainServiceManager.SysLicenseService.Update(entity);
                if (update.Succeeded)
                    TempData.AddSuccessMessage("success");
                else
                    TempData.AddErrorMessage($"{update.Status.message}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(entity);
            }
        }

        // GET: SysLicenseController/Delete/
        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                var chk = await permission.HasPermission(497, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");
                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }
                //long id = EncryptionHelper.Decrypt<long>(Id);
                var delLicense = await mainServiceManager.SysLicenseService.Remove(Id);
                if (delLicense.Succeeded)
                    TempData.AddSuccessMessage("success");
                else
                    TempData.AddErrorMessage($"{delLicense.Status.message}");

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
            var ddlvm = new DDLViewModel();

            var facilities = await accServiceManager.AccFacilityService.GetAll(f => f.IsDeleted == false);
            var ddFacilitiesList = listHelper.GetFromList<long>(facilities.Data.Select(s => new DDListItem<long> { Name = s.FacilityName, Value = s.FacilityId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddFacilitiesList), ddFacilitiesList);

            var ddLicenseTypeList = await listHelper.GetList(257, hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddLicenseTypeList), ddLicenseTypeList);

            var ddActivitiesList = await listHelper.GetList(258, hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddActivitiesList), ddActivitiesList);

            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = (session.Language == 1) ? s.BraName : s.BraName2, Value = s.BranchId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysLicenseDto model, bool hasDefault, string text = "")
        {
            if (model == null)
            {
                return await GetDDl(hasDefault, text);
            }

            var ddlvm = new DDLViewModel();

            var facilities = await accServiceManager.AccFacilityService.GetAll(f => f.IsDeleted == false);
            var ddFacilitiesList = listHelper.GetFromList<long>(facilities.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.FacilityName : s.FacilityName2, Value = s.FacilityId }), selectedValue: (int)(model.FacilityId ?? 0));
            ddlvm.AddList(nameof(ddFacilitiesList), ddFacilitiesList);

            var ddLicenseTypeList = await listHelper.GetList(257, selectedValue: model.LicenseType ?? 0);
            ddlvm.AddList(nameof(ddLicenseTypeList), ddLicenseTypeList);

            var ddActivitiesList = await listHelper.GetList(258, selectedValue: model.JobCat ?? 0);
            ddlvm.AddList(nameof(ddActivitiesList), ddActivitiesList);

            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = (session.Language == 1) ? s.BraName : s.BraName2, Value = s.BranchId }), selectedValue: (int)(model.BranchId ?? 0));
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        private bool ChkExtension(IFormFile file)
        {
            string[] acceptedExtension = { ".gif", ".png", ".jepg", ".jpg", ".pdf" };
            string extension = "." + file.FileName.Split('.').Last().ToLower();

            if (acceptedExtension.Contains(extension))
            {
                return true;
            }
            else
            {
                TempData.AddErrorMessage($"{$"{localization.GetResource1("ImageFormat")}"}");
                return false;
            }
        }
    }
}
