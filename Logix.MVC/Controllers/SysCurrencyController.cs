using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysCurrencyController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysCurrencyController(IMainServiceManager mainServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            ILocalizationService localization,
            ISessionHelper session)
        {
            this.mainServiceManager = mainServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.session = session;
            this.localization = localization;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SysCurrencyDto filter)
        {
            var chk = await permission.HasPermission(404, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");
            
            var model = new SearchVM<SysCurrencyDto, SysCurrencyDto>(filter, new List<SysCurrencyDto>());
            try
            {
                var items = await mainServiceManager.SysCurrencyService.GetAll(c => c.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }
                    if (!string.IsNullOrEmpty(filter.Code))
                    {
                        res = res.Where(c => c.Code.ToLower().Equals(filter.Code.ToLower()));
                    }
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
            var chk = await permission.HasPermission(404, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");
            setErrors();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysCurrencyDto entity)
        {
            setErrors();
            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                var add = await mainServiceManager.SysCurrencyService.Add(entity);
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
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(entity);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string encId)
        {
            try
            {
                setErrors();
                var chk = await permission.HasPermission(404, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");
                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction(nameof(Index));
                }
                int Id = EncryptionHelper.Decrypt<int>(encId);

                var getCurrency = await mainServiceManager.SysCurrencyService.GetForUpdate<SysCurrencyEditDto>(Id);
                if (getCurrency.Succeeded)
                {
                    return View(getCurrency.Data);
                }
                else
                {
                    TempData.AddErrorMessage($"{getCurrency.Status.message}");
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
        public async Task<IActionResult> Edit(SysCurrencyEditDto entity)
        {
            setErrors();
            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                var update=await mainServiceManager.SysCurrencyService.Update(entity);
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
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Delete(int Id = 0)
        {
            try
            {
                var chk = await permission.HasPermission(404, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                if (Id==0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete=await mainServiceManager.SysCurrencyService.Remove(Id);
                if (delete.Succeeded)
                    TempData.AddSuccessMessage("success");
                else
                    TempData.AddErrorMessage($"{delete.Status.message}");

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
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
