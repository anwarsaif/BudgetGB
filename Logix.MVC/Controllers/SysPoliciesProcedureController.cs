using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysPoliciesProcedureController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly IFilesHelper filesHelper;

        public SysPoliciesProcedureController(IMainServiceManager mainServiceManager,
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
        public async Task<IActionResult> Index(SysPoliciesProcedureDto filter)
        {
            var chk = await permission.HasPermission(934, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "all");
            var model = new SearchVM<SysPoliciesProcedureDto, SysPoliciesProceduresVw>(filter, new List<SysPoliciesProceduresVw>());
            try
            {
                var items = await mainServiceManager.SysPoliciesProcedureService.GetAllVW(c => c.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }

                    if (filter.TypeId > 0)
                        res = res.Where(r => r.TypeId != null && r.TypeId.Equals(filter.TypeId));


                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        res = res.Where(c => (c.Name != null && c.Name.Contains(filter.Name)) || (c.Name2 != null && c.Name2.ToLower().Contains(filter.Name.ToLower())));
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
        public async Task<IActionResult> Add()
        {
            try
            {
                var chk = await permission.HasPermission(934, PermissionType.Add);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                await GetDDl(true, "Tselect");
                var obj = new SysPoliciesProcedureVM();
                var sysGroups = await mainServiceManager.SysGroupService.GetAll(g => g.IsDeleted == false);
                foreach (var item in sysGroups.Data)
                {
                    var sysgroupVM = new SysGroupVM { GroupId = item.GroupId ?? 0, GroupName = item.GroupName };
                    obj.SysGroupsList.Add(sysgroupVM);
                }

                return View(obj);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysPoliciesProcedureVM obj)
        {
            try
            {
                SetErrors();
                await GetDDl(true, "Tselect");
                if (!ModelState.IsValid)
                    return View(obj);

                //save groups id
                string grpId = "";
                foreach (var sysGroup in obj.SysGroupsList)
                {
                    if (sysGroup.IsSelected)
                        grpId += sysGroup.GroupId + ",";
                }
                //remove the last comma "," from groupId
                if (!string.IsNullOrEmpty(grpId))
                    grpId = grpId.Remove(grpId.Length - 1);

                obj.SysPoliciesProcedureDto.GroupsId = grpId;

                //save file and take its url
                //if (obj.SysPoliciesProcedureDto.FileUpload != null)
                //{
                //    var saveFile = await filesHelper.SaveFile(obj.SysPoliciesProcedureDto.FileUpload, "AllFiles");
                //    obj.SysPoliciesProcedureDto.FileUrl = saveFile;
                //}

                //save obj
                var add = await mainServiceManager.SysPoliciesProcedureService.Add(obj.SysPoliciesProcedureDto);
                if (add.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index");
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
                var chk = await permission.HasPermission(934, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                SetErrors();
                await GetDDl(false);
                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction("Index");
                }
                long id = EncryptionHelper.Decrypt<long>(encId);

                //get all sysGroups.. for grant premission checkboxes in the view
                var sysGroups = await mainServiceManager.SysGroupService.GetAll(g => g.IsDeleted == false);
                if (!sysGroups.Succeeded)
                {
                    TempData.AddErrorMessage($"{sysGroups.Status.message}");
                    return RedirectToAction("Index");
                }

                var getItem = await mainServiceManager.SysPoliciesProcedureService.GetForUpdate<SysPoliciesProcedureEditDto>(id);
                if (!getItem.Succeeded)
                {
                    TempData.AddErrorMessage($"{getItem.Status.message}");
                    return RedirectToAction("Index");
                }

                var groupsId = getItem.Data.GroupsId;

                //this array will save the split of 'groupsId'; beacuse this field in DB save data as '1,2,5,...'
                string[] groupsIdArray = Array.Empty<string>();

                if (groupsId != null)
                {
                    groupsIdArray = groupsId.Split(',');
                }

                var obj = new SysPoliciesProcedureEditVM();
                foreach (var item in sysGroups.Data)
                {
                    //if this "sysPolicyProcedure" contains groupId of current item,we must display 'checked' checkbox for this group
                    var sysgroupVM = new SysGroupVM { GroupId = item.GroupId ?? 0, GroupName = item.GroupName, IsSelected = (groupsIdArray.Contains(item.GroupId.ToString())) };
                    obj.SysGroupsList.Add(sysgroupVM);
                }
                obj.SysPoliciesProcedureEditDto = getItem.Data;
                return View(obj);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SysPoliciesProcedureEditVM obj)
        {
            SetErrors();
            await GetDDl(false);
            if (!ModelState.IsValid)
                return View(obj);
            try
            {
                //save groups id
                string grpId = "";
                foreach (var sysGroup in obj.SysGroupsList)
                {
                    if (sysGroup.IsSelected)
                        grpId += sysGroup.GroupId + ",";
                }
                //remove the last comma "," from groupId
                if (!string.IsNullOrEmpty(grpId))
                    grpId = grpId.Remove(grpId.Length - 1);

                obj.SysPoliciesProcedureEditDto.GroupsId = grpId;

                ////save file and take its url
                //if (obj.SysPoliciesProcedureEditDto.FileUpload != null)
                //{
                //    var saveFile = await filesHelper.SaveFile(obj.SysPoliciesProcedureEditDto.FileUpload, "AllFiles");
                //    obj.SysPoliciesProcedureEditDto.FileUrl = saveFile;
                //}

                //update object
                var update = await mainServiceManager.SysPoliciesProcedureService.Update(obj.SysPoliciesProcedureEditDto);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.SysPoliciesProcedureEditDto.Id) });
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
                var chk = await permission.HasPermission(934, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                var delete = await mainServiceManager.SysPoliciesProcedureService.Remove(Id);
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

            var ddTypesList = await listHelper.GetList(381, hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddTypesList), ddTypesList);

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
