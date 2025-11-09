using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysLookupCategoryController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IWebHostEnvironment env;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        public SysLookupCategoryController(
           IMainServiceManager mainServiceManager,
           IPermissionHelper permission,
           IWebHostEnvironment env,
           IDDListHelper listHelper,
           ISessionHelper session,
           ILocalizationService localization)
        {
            this.mainServiceManager = mainServiceManager;
            this.permission = permission;
            this.env = env;
            this.listHelper = listHelper;
            this.session = session;
            this.localization = localization;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SysLookupCategoryDto filter)
        {
            var chk = await permission.HasPermission(121, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");
            await GetDDl(true, "all");
            var model = new SearchVM<SysLookupCategoryDto, SysLookupCategoryDto>(filter, new List<SysLookupCategoryDto>());

            try
            {
                var items = await mainServiceManager.SysLookupCategoryService.GetAll(c => c.Isdel == false);
                if (items.Succeeded)
                {
                    var res = items.Data.OrderBy(c => c.CatagoriesId).AsQueryable();

                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }

                    if (Convert.ToInt32(filter.SystemId) > 0)
                        res = res.Where(c => c.SystemId != null && c.SystemId.Equals(filter.SystemId));

                    if (!string.IsNullOrEmpty(filter.CatagoriesName))
                        res = res.Where(c => (c.CatagoriesName != null && c.CatagoriesName.Contains(filter.CatagoriesName) || (c.CatagoriesName2 != null && c.CatagoriesName2.ToLower().Contains(filter.CatagoriesName.ToLower()))));

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
            var chk = await permission.HasPermission(121, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            setErrors();
            await GetDDl(true, "Tselect");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysLookupCategoryDto entity)
        {
            setErrors();
            if (!ModelState.IsValid)
                return View(entity);

            try
            {
                var add=await mainServiceManager.SysLookupCategoryService.Add(entity);
                if (add.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.AddErrorMessage($"{add.Status.message}");
                    return View(entity);
                }
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(entity);
            }
        }

        [HttpGet]
        public async Task<ActionResult> Edit(string encId)
        {
            setErrors();
            var chk = await permission.HasPermission(121, PermissionType.Edit);
            if (!chk)
                return View("AccessDenied");
            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction(nameof(Index));
            }
            int Id = EncryptionHelper.Decrypt<int>(encId);
            try
            {
                var getCategory = await mainServiceManager.SysLookupCategoryService.GetForUpdate<SysLookupCategoryEditDto>(Id);
                if (getCategory.Succeeded)
                {
                    if (getCategory.Data.IsEditable == true)
                        return View(getCategory.Data);
                    else
                    {
                        TempData.AddErrorMessage("لا يمكنك تعديل هذا العنصر");
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    TempData.AddErrorMessage($"{getCategory.Status.message}");
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
        public async Task<ActionResult> Edit(SysLookupCategoryEditDto entity)
        {
            setErrors();
            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                var update = await mainServiceManager.SysLookupCategoryService.Update(entity);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return View(entity);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(entity);
            }
        }

        public async Task<ActionResult> Delete(int Id)
        {
            try
            {
                var chk = await permission.HasPermission(121, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");
                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysLookupCategoryService.Remove(Id);
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
            var ddlvm = new DDLViewModel();

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (session.Language == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
