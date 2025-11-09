using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysLookupDataController : Controller
    {
        
        private readonly IMainServiceManager mainServiceManager;
        private readonly IAccServiceManager accServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IWebHostEnvironment env;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysLookupDataController(
           IMainServiceManager mainServiceManager,
           IAccServiceManager accServiceManager,
           IPermissionHelper permission,
           IWebHostEnvironment env,
           IDDListHelper listHelper,
           ILocalizationService localization,
           ISessionHelper session)
        {
            this.mainServiceManager = mainServiceManager;
            this.accServiceManager = accServiceManager;
            this.permission = permission;
            this.env = env;
            this.listHelper = listHelper;
            this.localization = localization;
            this.session = session;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SysLookupDataVwsDto filter)
        {
            var chk = await permission.HasPermission(122, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDlWithDefault(filter, true, "all");
            var model = new SearchVM<SysLookupDataVwsDto, SysLookupDataVw>(filter, new List<SysLookupDataVw>());

            try
            {
                var items = await mainServiceManager.SysLookupDataService.GetAllVW();
                if (items.Succeeded)
                {
                    var res = items.Data.Where(s => s.Isdel == false).OrderBy(s => s.SortNo).AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }
                    if (Convert.ToInt32(filter.SystemId) > 0)
                        res = res.Where(l => l.SystemId != null && l.SystemId.Equals(filter.SystemId));

                    if (filter.CatagoriesId > 0)
                        res = res.Where(l => l.CatagoriesId.Equals(filter.CatagoriesId));

                    if (!string.IsNullOrEmpty(filter.Name))
                        res = res.Where(l => (l.Name != null && l.Name.Contains(filter.Name)) || (l.Name2 != null && l.Name2.Contains(filter.Name)));

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
            var chk = await permission.HasPermission(122, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            setErrors();
            await GetDDl(true, "Tselect");
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Add(SysLookupDataDto obj )
        {
            var dto = new SysLookupDataVwsDto
            {
                SystemId = obj.SystemId.ToString(),
                CatagoriesId = obj.CatagoriesId,
                ColorId= obj.ColorId
            };
            setErrors();
            await GetDDlWithDefault(dto, true, "Tselect");

            if (!ModelState.IsValid)
                return View(obj);

            try
            {
                var add = await mainServiceManager.SysLookupDataService.Add(obj);
                if (add.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return View(obj);
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
            setErrors();
            var chk = await permission.HasPermission(122, PermissionType.Edit);
            if (!chk)
                return View("AccessDenied");
            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction(nameof(Index));
            }
            long Id = EncryptionHelper.Decrypt<long>(encId);
            try
            {
                await GetDDl(false);

                var getLookup = await mainServiceManager.SysLookupDataService.GetForUpdate<SysLookupDataDto>(Id);
                if (getLookup.Succeeded)
                {
                    var costCenter = await accServiceManager.AccCostCenterService.GetOne(c => c.CcId == getLookup.Data.CcId && c.IsDeleted == false);
                    if (costCenter.Succeeded && costCenter != null)
                    {
                        getLookup.Data.CostCenterCode = costCenter.Data.CostCenterCode;
                        getLookup.Data.CostCenterName = costCenter.Data.CostCenterName;
                    }
                    var account = await accServiceManager.AccAccountService.GetOne(a => a.AccAccountId == getLookup.Data.AccAccountId && a.IsDeleted == false);
                    if (account.Succeeded && account != null)
                    {
                        getLookup.Data.AccountCode = account.Data.AccAccountCode;
                        getLookup.Data.AccountName = account.Data.AccAccountName;
                    }
                    return View(getLookup.Data);
                }
                else
                {
                    TempData.AddErrorMessage($"{getLookup.Status.message}");
                    return RedirectToAction("Index");
                }
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(SysLookupDataDto entity)
        {
            setErrors();
            var dto = new SysLookupDataVwsDto
            {
                CatagoriesId = entity.CatagoriesId,
                ColorId = entity.ColorId
            };
            await GetDDlWithDefault(dto, false);
            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                var update=await mainServiceManager.SysLookupDataService.Update(entity);
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

        public async Task<ActionResult> Delete(long Id)
        {
            try
            {
                var chk = await permission.HasPermission(122, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");
                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysLookupDataService.Remove(Id);
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

            var allCategories = await mainServiceManager.SysLookupCategoryService.GetAll();
            var categories = allCategories.Data.Where(c => c.Isdel == false);
            var ddCategoriesList = listHelper.GetFromList<long>(categories.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.CatagoriesName : s.CatagoriesName2 ?? s.CatagoriesName, Value = s.CatagoriesId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddCategoriesList), ddCategoriesList);
            
            var ddColorsList = await listHelper.GetList(287);
            ddlvm.AddList(nameof(ddColorsList), ddColorsList);


            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysLookupDataVwsDto model, bool hasDefault, string text = "")
        {
            if (model == null)
            {
                return await GetDDl(hasDefault, text);
            }

            var ddlvm = new DDLViewModel();

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (session.Language == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), selectedValue: Convert.ToInt32((model.SystemId)));
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);


            var allCategories = await mainServiceManager.SysLookupCategoryService.GetAll();
            var categories = allCategories.Data.Where(c => c.Isdel == false);
            var ddCategoriesList = listHelper.GetFromList<long>(categories.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.CatagoriesName : s.CatagoriesName2 ?? s.CatagoriesName, Value = s.CatagoriesId }), selectedValue: Convert.ToInt32((model.CatagoriesId)));
            ddlvm.AddList(nameof(ddCategoriesList), ddCategoriesList);

            var ddColorsList = await listHelper.GetList(287, selectedValue: Convert.ToInt32(model.ColorId));
            ddlvm.AddList(nameof(ddColorsList), ddColorsList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories(string systemId)
        {
            var categoriesData = await mainServiceManager.SysLookupCategoryService.GetAll(c => c.Isdel == false && c.SystemId == systemId);
            var categories = listHelper.GetFromList<long>(categoriesData.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.CatagoriesName : s.CatagoriesName2 ?? s.CatagoriesName, Value = s.CatagoriesId }));
            return Json(new { categories });
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
