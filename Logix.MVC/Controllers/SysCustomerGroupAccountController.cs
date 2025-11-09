using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Services;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysCustomerGroupAccountController : Controller
    {
        private readonly IMainServiceManager serviceManager;
        private readonly IAccServiceManager AccServiceManager;
        private readonly IPermissionHelper permission;
        private readonly ILocalizationService localization;
        
        public SysCustomerGroupAccountController(IMainServiceManager serviceManager,
            IPermissionHelper permission,
            IAccServiceManager AccServiceManager,
            ILocalizationService localization)
        {
            this.serviceManager = serviceManager;
            this.permission = permission;
            this.AccServiceManager = AccServiceManager;
            this.localization = localization;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        public async Task<IActionResult> Add(string encGroupId)
        {
            var chk = await permission.HasPermission(400, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");
            
            if (string.IsNullOrEmpty(encGroupId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction("Index", "SysCustomerGroup");
            }

            int id = EncryptionHelper.Decrypt<int>(encGroupId);
            
            try
            {
                //get CustmoerGroup Data 
                var customerGroup = await serviceManager.SysCustomerGroupService.GetById(id);
                if (!customerGroup.Succeeded)
                    return RedirectToAction("Index", "SysCustomerGroup");

                //var cusTypeId = customerGroup.Data.CusTypeId??0;
                //var cusType = await serviceManager.SysCustomerTypeService.GetById(cusTypeId);
                //obj.GroupName = cusType.Data.CusTypeName;

                var obj = new SysCustomerGroupAccountVM();
                obj.sysCustomerGroupAccountDto.GroupId = Convert.ToInt64(id);
                obj.GroupName = customerGroup.Data.Name;

                var cusGrpAccVw = serviceManager.SysCustomerGroupAccountService.GetAllVW();
                obj.sysCustomerGroupAccountsVws = cusGrpAccVw.Result.Data.Where(g => g.GroupId == obj.sysCustomerGroupAccountDto.GroupId && g.IsDeleted == false);

                return View(obj);
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Add(SysCustomerGroupAccountVM obj)
        {
            if (!ModelState.IsValid)
            {
                var cusGrpAccVw = serviceManager.SysCustomerGroupAccountService.GetAllVW();
                obj.sysCustomerGroupAccountsVws = cusGrpAccVw.Result.Data.Where(g => g.GroupId == obj.sysCustomerGroupAccountDto.GroupId && g.IsDeleted == false);
                return View(obj);
            }
           
            try
            {
                var add = await serviceManager.SysCustomerGroupAccountService.Add(obj.sysCustomerGroupAccountDto);
                if (add.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    var encGroupId = EncryptionHelper.Encrypt(obj.sysCustomerGroupAccountDto.GroupId);
                    return RedirectToAction("Add", routeValues: new { encGroupId });
                }
                else
                {
                    TempData.AddErrorMessage($"{add.Status.message}");
                    return View(obj);
                }
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(obj);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetNameById(long id)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(50));
            if (id == 0)
                return Ok("");
            
            var annRes = await AccServiceManager.AccAccountService.GetById(id);
            if (annRes.Succeeded && annRes.Data != null)
                return Ok(annRes.Data.AccAccountName);
            else
                return Ok("");     
        }

        [HttpGet]
        public async Task<IActionResult> GetPopUpResult(AccAccountDto filter)
        {
            if (filter != null)
            {
                var list = new List<AccAccountDto>();
                var screens = await AccServiceManager.AccAccountService.GetAll();
                if (screens.Succeeded && screens.Data != null)
                {
                    list.AddRange(screens.Data);
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }
                if (filter.AccAccountId != null && filter.AccAccountId > 0)
                {
                    list = list.Where(e => e.AccAccountId == filter.AccAccountId).ToList();
                }

                return View("_AccPopUpResult", list);
            }
            else
            {
                var screens = await AccServiceManager.AccAccountService.GetAll();
                if (screens.Succeeded && screens.Data != null)
                {
                    return View("_AccPopUpResult", screens.Data);
                }
            }

            return View("_AccPopUpResult");
        }

    }
}