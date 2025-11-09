using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysDynamicAttributeController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IWFServiceManager wfServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysDynamicAttributeController(IMainServiceManager mainServiceManager,
            IWFServiceManager wfServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            ILocalizationService localization,
            ISessionHelper session)
        {
            this.mainServiceManager = mainServiceManager;
            this.wfServiceManager = wfServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.session = session;
            this.localization = localization;
        }


        //Screens
        [HttpGet]
        public async Task<IActionResult> Index(SysScreenDto filter)
        {
            var chk = await permission.HasPermission(1004, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "all");
            var model = new SearchVM<SysScreenDto, SysScreenVw>(filter, new List<SysScreenVw>());
            try
            {
                if (filter == null)
                {
                    return View(model);
                }

                var items = await mainServiceManager.SysScreenService.GetAllVW(s => s.Isdel == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();

                    if (filter.SystemId > 0)
                        res = res.Where(s => s.SystemId != null && s.SystemId.Equals(filter.SystemId));

                    if (!string.IsNullOrEmpty(filter.ScreenName))
                        res = res.Where(s => s.ScreenName != null && s.ScreenName.Contains(filter.ScreenName));

                    if (!string.IsNullOrEmpty(filter.ScreenName2))
                        res = res.Where(s => s.ScreenName2 != null && s.ScreenName2.ToLower().Contains(filter.ScreenName2.ToLower()));

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
        public async Task<IActionResult> AddAttribute(string encId)
        {
            var chk = await permission.HasPermission(1004, PermissionType.Edit);
            if (!chk)
                return View("AccessDenied");

            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction("Index");
            }
            long id = EncryptionHelper.Decrypt<long>(encId);
            //get basic data
            var items = await mainServiceManager.SysScreenService.GetOne(s => s.ScreenId == id && s.Isdel == false);
            if (items.Succeeded)
            {
                SysDynamicAttributeVM obj = new SysDynamicAttributeVM()
                {
                    ScreenId = items.Data.ScreenId ?? 0,
                    ScreenName = items.Data.ScreenName??"",
                    ScreenName2 = items.Data.ScreenName2??"",
                    SystemId = items.Data.SystemId ?? 0
                };

                await GetDDl(true, "choose");
                return View(obj);
            }
            TempData.AddErrorMessage($"{items.Status.message}");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddAttribute(SysDynamicAttributeVM obj)
        {
            var test = obj.SysDynamicAttributeDto;
            return View();
        }


        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            var ddlvm = new DDLViewModel();
            int lang = session.Language;

            var allSystems = await mainServiceManager.SysSystemService.GetAll();
            var systems = allSystems.Data.Where(s => s.Isdel == false);
            var ddSystemsList = listHelper.GetFromList<int>(systems.Select(s => new DDListItem<int> { Name = (lang == 1) ? s.SystemName : s.SystemName2, Value = s.SystemId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddSystemsList), ddSystemsList);

            var allTypes = await mainServiceManager.SysDynamicAttributeService.GetAttributeTypes();
            var types = allTypes.Data;
            var ddTypesList = listHelper.GetFromList<int>(types.Select(s => new DDListItem<int> { Name = (lang == 1) ? s.DataTypeCaption : s.DataTypeName, Value = s.Id }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddTypesList), ddTypesList);

            var allWfLookup = await wfServiceManager.WfLookUpCatagoryService.GetAll(l => l.Isdel == false);
            var lookups = allWfLookup.Data;
            var ddLookupsList = listHelper.GetFromList<int>(lookups.Select(s => new DDListItem<int> { Name = (lang == 1) ? s.CatagoriesName : s.CatagoriesName2, Value = s.CatagoriesId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddLookupsList), ddLookupsList);

            var allWfTypeTbl = await wfServiceManager.WfAppTypeTableService.GetAll(t => t.IsDeleted == false);
            var typesTbl = allWfTypeTbl.Data;
            var ddTypesTableList = listHelper.GetFromList<long>(typesTbl.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), hasDefault: hasDefault, defaultText: text);

            ddlvm.AddList(nameof(ddTypesTableList), ddTypesTableList);
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
    }
}
