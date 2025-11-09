using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.DTOs.WF;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Services;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Logix.MVC.Controllers
{
    public class SysGroupController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IWFServiceManager wfServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysGroupController(IMainServiceManager mainServiceManager,
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

        #region ===================== Basic Functions (Add, Edit, Delete) =================

        public async Task<IActionResult> Index(SysGroupDto filter)
        {
            var model = new SearchVM<SysGroupDto, SysGroupVw>(filter, new List<SysGroupVw>());
            try
            {
                var chk = await permission.HasPermission(116, PermissionType.Show);
                if (!chk)
                    return View("AccessDenied");
                await GetDDl(true, filter.SystemId ?? 0, "all");

                var items = await mainServiceManager.SysGroupService.GetAllVW(g => g.FacilityId == session.FacilityId && g.Isdel == false);
                if (items.Succeeded)
                {
                    var res = items.Data.OrderBy(g => g.GroupId).AsQueryable();

                    if (filter.SystemId > 0)
                    {
                        res = res.Where(g => g.SystemId != null && g.SystemId.Equals(filter.SystemId));
                    }

                    if (session.GroupId != 1)
                    {
                        var userGroup = await mainServiceManager.SysGroupService.GetById(session.GroupId);
                        if (userGroup.Succeeded && userGroup.Data != null)
                        {
                            res = res.Where(g => g.SystemId > 0 && g.SystemId == userGroup.Data.SystemId);
                        }
                        else
                        {
                            TempData.AddErrorMessage($"{userGroup.Status.message}");
                            return View("Index", model);
                        }
                    }

                    if (!string.IsNullOrEmpty(filter.GroupName))
                    {
                        res = res.Where(g => g.GroupName != null && g.GroupName.Contains(filter.GroupName));
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

        public async Task<IActionResult> Add()
        {
            var chk = await permission.HasPermission(116, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            SetErrors();
            await GetDDl(true, 0, "choose");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysGroupDto obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, 0, "choose");
                if (!ModelState.IsValid)
                    return View(obj);

                var update = await mainServiceManager.SysGroupService.Add(obj);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(update.Data.GroupId) });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
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
                var chk = await permission.HasPermission(116, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");
                SetErrors();
                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction("Index");
                }

                int GroupId = EncryptionHelper.Decrypt<int>(encId);

                var obj = new SysScreenGrouopPermissionWM();

                var groupData = await mainServiceManager.SysGroupService.GetForUpdate<SysGroupEditDto>(GroupId);
                if (groupData.Succeeded)
                {
                    await GetDDl(false, groupData.Data.SystemId ?? 0);
                    obj.SysGroupDto = groupData.Data;
                    obj.SystemIdDdl = groupData.Data.SystemId;

                    var chkHasAutoSys = await mainServiceManager.SysSystemService.GetOne(s => s.SystemId == 43 && s.Isdel == false);
                    if (chkHasAutoSys.Succeeded && chkHasAutoSys.Data != null)
                        obj.HasAutomationSys = true;
                    return View(obj);
                }
                else
                {
                    TempData.AddErrorMessage($"{groupData.Status.message}");
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
        public async Task<IActionResult> EditBasicData(SysScreenGrouopPermissionWM obj)
        {
            try
            {
                var update = await mainServiceManager.SysGroupService.Update(obj.SysGroupDto);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.SysGroupDto.GroupId) });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.SysGroupDto.GroupId) });
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.SysGroupDto.GroupId) });
            }
        }

        public async Task<ActionResult> Delete(int Id = 0)
        {
            try
            {
                var chk = await permission.HasPermission(116, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysGroupService.Remove(Id);
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

        #endregion


        #region =========== Re-fill Tables in Partial Views (screens & forms data) ========

        [HttpPost]
        public async Task<IActionResult> RefillTable(string[] screensId, int groupId, int systemId)
        {
            try
            {
                var ScreensArray = screensId;
                List<SysScreenPermissionDtoVM> vm = new List<SysScreenPermissionDtoVM>();
                // نجلب الشاشات 
                var screenInstalled = await mainServiceManager.SysScreenInstalledService.GetAllVW(s => s.SystemId == systemId && ScreensArray.Contains(s.ParentId.ToString()) && s.IsDeleted == false);
                if (screenInstalled.Succeeded)
                {
                    foreach (var item in screenInstalled.Data)
                    {
                        var screenPermission = await mainServiceManager.SysScreenPermissionService.GetOne(p => p.ScreenId == item.ScreenId && p.GroupId == groupId);
                        if (screenPermission.Data != null)
                        {
                            var permission = screenPermission.Data;
                            //نعبي الصلاحيات كما هي،، حسب ما هو بالجدول
                            var SysScreenPermissionDto = new SysScreenPermissionDtoVM
                            {
                                ScreenId = item.ScreenId,
                                GroupId = groupId,
                                PriveId = permission.PriveId,
                                ScreenName = (session.Language == 1) ? item.ScreenName : item.ScreenName2,
                                ScreenShow = permission.ScreenShow ?? false,
                                ScreenAdd = permission.ScreenAdd ?? false,
                                ScreenEdit = permission.ScreenEdit ?? false,
                                ScreenDelete = permission.ScreenDelete ?? false,
                                ScreenPrint = permission.ScreenPrint ?? false
                            };
                            vm.Add(SysScreenPermissionDto);
                        }
                        else
                        {
                            //اذا الشاشة لم تحفظ في جدول الصلاحيات بعد
                            var SysScreenPermissionDto = new SysScreenPermissionDtoVM
                            {
                                ScreenId = item.ScreenId,
                                GroupId = groupId,
                                ScreenName = item.ScreenName,
                                ScreenShow = false,
                                ScreenAdd = false,
                                ScreenEdit = false,
                                ScreenDelete = false,
                                ScreenPrint = false
                            };
                            vm.Add(SysScreenPermissionDto);
                        }
                    }

                    return PartialView("_ScreensPermissions", vm);
                }
                return PartialView("_ScreensPermissions", new List<SysScreenPermissionDtoVM>());
            }
            catch (Exception ex)
            {
                return PartialView("_ScreensPermissions", new List<SysScreenPermissionDtoVM>());
            }
        }

        [HttpPost]
        public async Task<IActionResult> RefillTableWithForms(string[] groupsId, int groupId, int systemId)
        {
            try
            {
                var groupsArray = groupsId;
                List<FormsPermissionDtoVM> vm = new List<FormsPermissionDtoVM>();
                int lang = session.Language;
                // نجلب النماذج
                var forms = await wfServiceManager.WfAppTypeService.GetAllVW(f => f.IsDeleted == false && groupsArray.Contains(f.GroupId.ToString()) && f.SystemId == systemId);
                if (forms.Succeeded)
                {
                    foreach (var item in forms.Data)
                    {
                        var obj = new FormsPermissionDtoVM()
                        {
                            GroupId = groupId,
                            FormId = item.Id,
                            FormName = (lang == 1) ? item.Name : item.Name2,
                            FormQery = (!string.IsNullOrEmpty(item.SysGroupQuery)) && item.SysGroupQuery.Split(',').Contains(groupId.ToString()),
                            FormSend = (!string.IsNullOrEmpty(item.SysGroupId)) && item.SysGroupId.Split(',').Contains(groupId.ToString())
                        };

                        vm.Add(obj);
                    }
                    return PartialView("_FormsPermissions", vm);
                }
                else
                {
                    return PartialView("_FormsPermissions", new List<FormsPermissionDtoVM>());
                }
            }
            catch
            {
                return PartialView("_FormsPermissions", new List<FormsPermissionDtoVM>());
            }
        }

        #endregion


        #region =========== Edit Permissions on Sceens & on Automation Forms ==============

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPermissions(List<SysScreenPermissionDtoVM> obj)
        {
            //تعديل الصلاحيات على الشاشات
            int groupId = obj.First().GroupId ?? 0;
            SetErrors();

            if (!ModelState.IsValid)
                return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });

            try
            {
                var update = await mainServiceManager.SysScreenPermissionService.Update(obj);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditFormsPermissions(List<FormsPermissionDtoVM> obj)
        {
            int groupId = obj.First().GroupId ?? 0;
            SetErrors();

            if (!ModelState.IsValid)
                return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
            try
            {
                //update wfAppType
                var update = await wfServiceManager.WfAppTypeService.Update(obj);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(groupId) });
            }
        }

        #endregion


        #region =========== Re-fill dropdown lists when change value ======================

        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, int systemId = 0, string text = "")
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
                var allScreens = await mainServiceManager.SysScreenService.GetAll(s => s.SystemId == systemId && s.ParentId == s.ScreenId && s.Isdel == false);
                var ddScreensList = listHelper.GetFromList<long>(allScreens.Data.Select(s => new DDListItem<long> { Name = (language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: false);
                ddlvm.AddList(nameof(ddScreensList), ddScreensList);

                var allFormsGroup = await wfServiceManager.WfAppGroupService.GetAll(g => g.SystemId == Convert.ToInt64(systemId) && g.IsDeleted == false);
                var ddFormsGroupList = listHelper.GetFromList<long>(allFormsGroup.Data.Select(g => new DDListItem<long> { Name = (session.Language == 1) ? g.Name : g.Name2 ?? g.Name, Value = g.Id ?? 0 }), hasDefault: false);
                ddlvm.AddList(nameof(ddFormsGroupList), ddFormsGroupList);

            }
            else
            {
                //in index view (filter) systemId may be null "all", this code to avoid null screen data for selectList
                var ddScreensList = new SelectList(new List<DDListItem<SysScreen>>());
                ddlvm.AddList(nameof(ddScreensList), ddScreensList);

                var ddFormsGroupList = new SelectList(new List<DDListItem<WfAppGroupDto>>());
                ddlvm.AddList(nameof(ddFormsGroupList), ddFormsGroupList);
            }

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [HttpGet]
        public async Task<IActionResult> GetScreens(string systemId)
        {
            if (systemId == "0")
            {
                var screens = new SelectList(new List<DDListItem<SysScreen>>());
                return Json(new { screens });
            }
            else
            {
                var allScreens = await mainServiceManager.SysScreenService.GetAll(s => s.SystemId == Convert.ToInt32(systemId) && s.ParentId == s.ScreenId && s.Isdel == false);
                var screens = listHelper.GetFromList<long>(allScreens.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.ScreenName : s.ScreenName2 ?? s.ScreenName, Value = s.ScreenId ?? 0 }), hasDefault: false);
                return Json(new { screens });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFormsGroup(string systemId)
        {
            if (systemId == "0")
            {
                var groups = new SelectList(new List<DDListItem<WfAppGroupDto>>());
                return Json(new { groups });
            }
            else
            {
                var allFormsGroup = await wfServiceManager.WfAppGroupService.GetAll(g => g.SystemId == Convert.ToInt64(systemId) && g.IsDeleted == false);
                var groups = listHelper.GetFromList<long>(allFormsGroup.Data.Select(g => new DDListItem<long> { Name = (session.Language == 1) ? g.Name : g.Name2 ?? g.Name, Value = g.Id ?? 0 }), hasDefault: false);
                return Json(new { groups });
            }
        }

        #endregion


        #region ============== Copying permission from another group ======================
        [HttpGet]
        public async Task<IActionResult> Copy(string encId)
        {
            try
            {
                var chk = await permission.HasPermission(116, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                if (string.IsNullOrEmpty(encId))
                {
                    return RedirectToAction("Index");
                }
                int GroupId = EncryptionHelper.Decrypt<int>(encId);
                await GetDDlForCopy(GroupId);
                CopyGroupVM obj = new() { GroupId = GroupId };
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Copy(CopyGroupVM obj)
        {
            try
            {
                SetErrors();
                await GetDDlForCopy(obj.GroupId ?? 0);
                if (!ModelState.IsValid)
                    return View(obj);

                var copy = await mainServiceManager.SysGroupService.Copy(obj);
                if (copy.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return View(obj);
                }
                else
                {
                    TempData.AddErrorMessage($"{copy.Status.message}");
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(obj);
            }
        }

        public async Task<DDLViewModel> GetDDlForCopy(int groupId1 = 0)
        {
            var ddlvm = new DDLViewModel();

            var allSysGroups = await mainServiceManager.SysGroupService.GetAll(g => g.FacilityId == session.FacilityId && g.IsDeleted == false);

            var groups1 = allSysGroups.Data.Where(g => g.GroupId == groupId1);
            var ddGroupsList1 = listHelper.GetFromList<int>(groups1.Select(s => new DDListItem<int> { Name = s.GroupName ?? "", Value = s.GroupId ?? 0 }), hasDefault: false);
            ddlvm.AddList(nameof(ddGroupsList1), ddGroupsList1);

            var groups2 = allSysGroups.Data.Where(g => g.GroupId != groupId1);
            var ddGroupsList2 = listHelper.GetFromList<int>(groups2.Select(s => new DDListItem<int> { Name = s.GroupName ?? "", Value = s.GroupId ?? 0 }), hasDefault: true, defaultText: "choose");
            ddlvm.AddList(nameof(ddGroupsList2), ddGroupsList2);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
        #endregion


        [NonAction]
        private void SetErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
    }
}
