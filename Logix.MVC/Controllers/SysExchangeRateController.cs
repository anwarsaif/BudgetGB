using Logix.Application.Common;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Logix.MVC.Controllers
{
    public class SysExchangeRateController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;

        public SysExchangeRateController(IMainServiceManager mainServiceManager,
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
        public async Task<IActionResult> Index(SysExchangeRateDto filter)
        {
            var chk = await permission.HasPermission(405, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            await GetDDl(true, "all");
            var model = new SearchVM<SysExchangeRateDto, SysExchangeRateVw>(filter, new List<SysExchangeRateVw>());
            try
            {
                var items = await mainServiceManager.SysExchangeRateService.GetAllExRateVw();
                if (items.Succeeded)
                {
                    var res = items.Data.Where(r => r.IsDeleted == false).AsQueryable();
                    if (filter == null)
                    {
                        model.ListModel = res.ToList();
                        return View(nameof(Index), model);
                    }
                    if (filter.CurrencyFromID > 0)
                    {
                        res = res.Where(r => r.CurrencyFromId.Equals(filter.CurrencyFromID));
                    }
                    if (filter.CurrencyToID > 0)
                    {
                        res = res.Where(r => r.CurrencyToId.Equals(filter.CurrencyToID));
                    }
                    if (filter.ExchangeRate != null)
                    {
                        res = res.Where(r => r.ExchangeRate.Equals(filter.ExchangeRate));
                    }
                    if (filter.FromDate != null)
                    {
                        res = res.Where(r => r.ExchangeDate != null && (DateTime.ParseExact(r.ExchangeDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) >= DateTime.ParseExact(filter.FromDate, "yyyy/MM/dd", CultureInfo.InvariantCulture)));
                    }
                    if (filter.ToDate != null)
                    {
                        res = res.Where(r => r.ExchangeDate != null && (DateTime.ParseExact(r.ExchangeDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) <= DateTime.ParseExact(filter.ToDate, "yyyy/MM/dd", CultureInfo.InvariantCulture)));
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
            var chk = await permission.HasPermission(405, PermissionType.Add);
            if (!chk)
                return View("AccessDenied");

            setErrors();
            await GetDDl(true, "Tselect");
            //return view with dto to take the default value of exchangeDate->(dateTime.Now)
            return View(new SysExchangeRateDto());
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysExchangeRateDto obj)
        {
            setErrors();
            await GetDDlWithDefault(obj, true, "Tselect");

            if (!ModelState.IsValid)
                return View(obj);
            try
            {
                var add = await mainServiceManager.SysExchangeRateService.Add(obj);
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
            catch(Exception ex)
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
                setErrors();
                await GetDDl(false);
                var chk = await permission.HasPermission(405, PermissionType.Edit);
                if (!chk)
                    return View("AccessDenied");

                if (string.IsNullOrEmpty(encId))
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                    return RedirectToAction(nameof(Index));
                }
                long Id = EncryptionHelper.Decrypt<long>(encId);

                var getItem = await mainServiceManager.SysExchangeRateService.GetForUpdate<SysExchangeRateEditDto>(Id);
                if (getItem.Succeeded)
                {
                    return View(getItem.Data);
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
        public async Task<IActionResult> Edit(SysExchangeRateEditDto entity)
        {
            setErrors();
            await GetDDl(false);

            if (!ModelState.IsValid)
                return View(entity);
            try
            {
                var update = await mainServiceManager.SysExchangeRateService.Update(entity);
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

        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                var chk = await permission.HasPermission(405, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                var delete = await mainServiceManager.SysExchangeRateService.Remove(Id);
                
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
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "")
        {
            var ddlvm = new DDLViewModel();

            var currencies = await mainServiceManager.SysCurrencyService.GetAll(c => c.IsDeleted == false);
            var ddCurrenciesList = listHelper.GetFromList<int>(currencies.Data.Select(c => new DDListItem<int> { Name = (session.Language == 1) ? c.Name : c.Name2, Value = c.Id }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddCurrenciesList), ddCurrenciesList);

            ViewData["DDL"] = ddlvm;
                
            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysExchangeRateDto model, bool hasDefault, string text = "")
        {
            if (model == null)
            {
                return await GetDDl(hasDefault, text);
            }

            var ddlvm = new DDLViewModel();

            var currencies = await mainServiceManager.SysCurrencyService.GetAll(c => c.IsDeleted == false);
            var ddCurrenciesList = listHelper.GetFromList<int>(currencies.Data.Select(c => new DDListItem<int> { Name = (session.Language == 1) ? c.Name : c.Name2, Value = c.Id }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddCurrenciesList), ddCurrenciesList);

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
