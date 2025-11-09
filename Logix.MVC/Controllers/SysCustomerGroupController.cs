using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Extensions;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysCustomerGroupController : Controller
    {
        private readonly ISessionHelper session;
        private readonly IMainServiceManager serviceManager;
        private readonly IPermissionHelper permission;
        private readonly ILocalizationService localization;

        public SysCustomerGroupController(IMainServiceManager serviceManager,
            IPermissionHelper permission,
            ILocalizationService localization,
            ISessionHelper session)
        {
            this.session = session; 
            this.serviceManager = serviceManager;
            this.permission = permission;
            this.localization = localization;
        }
        // GET: SysCustomerGroupController
        public async Task<ActionResult> Index()
        {
            try
            {
                var chk = await permission.HasPermission(400, PermissionType.Show);
                if (!chk)
                    return View("AccessDenied");
               
                var obj = new SysCustomerTypeGroupVM();
                //to make "All" is the first choice in the DDlist
                obj.TypeId = 0;
                var result = await serviceManager.SysCustomerGroupService.GetAll();
                obj.sysCustomerGroups = (IEnumerable<SysCustomerGroupDto>?)result.Data.Where(s => s.IsDeleted == false);
                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> Index(SysCustomerTypeGroupVM obj)
        {
            try
            {
                if (obj.TypeId == 0)
                {
                    //select all
                    var result = await serviceManager.SysCustomerGroupService.GetAll();
                    obj.sysCustomerGroups = (IEnumerable<SysCustomerGroupDto>?)result.Data.Where(s => s.IsDeleted == false && s.FacilityId == session.FacilityId);
                }
                else
                {
                    var result = await serviceManager.SysCustomerGroupService.GetAll(s => s.CusTypeId == obj.TypeId && s.IsDeleted == false && s.FacilityId == session.FacilityId);
                    obj.sysCustomerGroups = (IEnumerable<SysCustomerGroupDto>?)result.Data;
                }
                if (obj.Name != null)
                    obj.sysCustomerGroups = obj.sysCustomerGroups.Where(s => s.Name!=null && s.Name.Contains(obj.Name));
                return View(obj);
            }
            catch
            {
                return View();
            }
        }

        // GET: SysCustomerGroupController/Add
        public async Task<ActionResult> Add()
        {
            var chk = await permission.HasPermission(400, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");
            setErrors();

            return View();
        }

        // POST: SysCustomerGroupController/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(SysCustomerGroupDto entity)
        {
            setErrors();
            if (!ModelState.IsValid)
                return View(entity);
            
            try
            {
                var result = await serviceManager.SysCustomerGroupService.Add(entity);
                if (result.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index");
                }
                else
                    TempData.AddErrorMessage($"{result.Status.message}");
                
                return View();
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View();
            }
        }


        // GET: SysCustomerGroupController/Edit/5
        public async Task<ActionResult> Edit(string encId)
        {
            var chk = await permission.HasPermission(400, PermissionType.Edit);
            if (!chk)
                return View("AccessDenied");

            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction("Index");
            }

            int id = EncryptionHelper.Decrypt<int>(encId);

            var result = await serviceManager.SysCustomerGroupService.GetForUpdate< SysCustomerGroupEditDto>(id);
            if (result.Succeeded)
            {
                if (result.Data == null)
                    return RedirectToAction("Index");
                var obj = result.Data;
                return View(obj);
            }
            else
            {
                TempData.AddErrorMessage($"{result.Status.message}");
                return RedirectToAction("Index");
            }
        }

        // POST: SysCustomerGroupController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(SysCustomerGroupEditDto obj)
        {
            if (!ModelState.IsValid)
                return View(obj);

            try
            {
                var result = await serviceManager.SysCustomerGroupService.Update(obj);
                if (result.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData.AddErrorMessage($"{result.Status.message}");
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View();
            }
        }


        // GET: SysCustomerGroupController/Delete/5
        public async Task<ActionResult> Delete(int id = 0)
        {
            var chk = await permission.HasPermission(400, PermissionType.Delete);
            if (!chk)
                return View("AccessDenied");
            
            if (id == 0)
                return RedirectToAction("Index", "SysCustomerGroup");
            try
            {
                var res = await serviceManager.SysCustomerGroupService.Remove(id);
                if (res.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Index", "SysCustomerGroup");
                }
                else
                {
                    TempData.AddErrorMessage($"{res.Status.message}");
                    return RedirectToAction("Index", "SysCustomerGroup");
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index", "SysCustomerGroup");
            }
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }

    }
}
