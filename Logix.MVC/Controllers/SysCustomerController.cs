using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Services;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.Services.ExportService;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Logix.MVC.Controllers
{
    public class SysCustomerController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IDDListHelper listHelper;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly IPermissionHelper permission;
        private readonly ISessionHelper session;
        private readonly ILocalizationService localization;
        private readonly ISalServiceManager salServiceManager;
        private readonly IHrServiceManager hrServiceManager;
        private readonly IExportService exportService;
        private readonly IAccServiceManager accServiceManager;
        private readonly IOPMServiceManager opmServiceManager;

        public SysCustomerController(IMainServiceManager mainServiceManager, IDDListHelper listHelper,
            ISysConfigurationHelper configurationHelper, IPermissionHelper permission, ISessionHelper session
            , ILocalizationService localization
             , ISalServiceManager salServiceManager
             , IHrServiceManager hrServiceManager
              , IExportService exportService
              , IAccServiceManager accServiceManager
            , IOPMServiceManager opmServiceManager
            )
        {
            this.mainServiceManager = mainServiceManager;
            this.listHelper = listHelper;
            this.configurationHelper = configurationHelper;
            this.permission = permission;
            this.session = session;
            this.localization = localization;
            this.salServiceManager = salServiceManager;
            this.hrServiceManager = hrServiceManager;
            this.exportService = exportService;
            this.accServiceManager = accServiceManager;
            this.opmServiceManager = opmServiceManager;
        }
        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDl(string DDLDefaultText)
        {
            int lang = session.GetData<int>("language");
            var ddlvm = new DDLViewModel();


            var ddSource = await listHelper.GetList(298, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddSource), ddSource);

            var ddNationality = await listHelper.GetList(40, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddNationality), ddNationality);

            var ddEnable = await listHelper.GetList(328, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddEnable), ddEnable);

            var DDLIndustry = await listHelper.GetList(300, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLIndustry), DDLIndustry);

            var ddPaymentType = await listHelper.GetList(332, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddPaymentType), ddPaymentType);

            var DDLSalesType = await listHelper.GetList(333, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLSalesType), DDLSalesType);

            var DDLbanks = await listHelper.GetList(24, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLbanks), DDLbanks);

            var DDLFielType = await listHelper.GetList(344, defaultText: localization.GetResource1("Choose"));
            ddlvm.AddList(nameof(DDLFielType), DDLFielType);


            var AllGroup = await mainServiceManager.SysCustomerGroupService.GetAll();
            var deptGroup = AllGroup.Data?.Where(d => d.IsDeleted == false && d.CusTypeId == 2 && d.FacilityId == session.FacilityId) ?? new List<SysCustomerGroupDto>();
            var DDLGroup = listHelper.GetFromList<long>(deptGroup.OrderBy(d => d.Name).Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLGroup), DDLGroup);

            var AllCoType = await mainServiceManager.SysCustomerCoTypeService.GetAll();
            var deptCoType = AllCoType.Data ?? new List<SysCustomerCoTypeDto>();
            var DDLCoType = listHelper.GetFromList<long>(deptCoType.OrderBy(d => d.Name).Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCoType), DDLCoType);


            var AllCity = await mainServiceManager.SysCitesService.GetAll();
            var deptCity = AllCity.Data?.Where(d => d.IsDeleted == false) ?? new List<SysCitesDto>();
            var DDLCity = listHelper.GetFromList<long>(deptCity.OrderBy(d => d.CityName).Select(s => new DDListItem<long> { Name = lang == 1 ? s.CityName : s.CityName2 ?? s.CityName, Value = s.CityID }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCity), DDLCity);

            var AllEMP = await hrServiceManager.HrEmployeeService.GetAll();
            var deptEmp = AllEMP.Data?.Where(d => d.IsDeleted == false) ?? new List<HrEmployeeDto>();
            var DDLEmp = listHelper.GetFromList<long>(deptEmp.OrderBy(d => d.EmpName).Select(s => new DDListItem<long> { Name = lang == 1 ? s.EmpName : s.EmpName2 ?? s.EmpName, Value = s.Id }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLEmp), DDLEmp);


            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            //  var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }));
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);


            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = lang == 1 ? s.BraName : s.BraName2 ?? s.BraName, Value = s.BranchId }), defaultText: DDLDefaultText);
            //var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = s.BraName, Value = s.BranchId }));
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            var DefaultCurrencyID = await mainServiceManager.SysExchangeRateService.GetOne(s => s.Id, x => x.Id == 1);
            var allCurrency = await mainServiceManager.SysExchangeRateService.GetAllVW();
            var deCurrency = allCurrency.Data.Where(d => d.IsDeleted == false);

            var DDLCurrencys = listHelper.GetFromList<long>(deCurrency.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }), selectedValue: (long)(DefaultCurrencyID.Data), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCurrencys), DDLCurrencys);


            var GroupPrice = await salServiceManager.SalItemsPriceMService.GetAll();
            var GroupPrices = GroupPrice.Data.Where(d => d.IsDeleted == false);
            var DDLGroupPrices = listHelper.GetFromList<long>(GroupPrices.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLGroupPrices), DDLGroupPrices);

            var PosSetting = await salServiceManager.SalPosSettingService.GetAll();
            var PosSettings = PosSetting.Data.Where(d => d.IsDeleted == false);
            //var GroupPrices = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            // var DDLPOS = listHelper.GetFromList<long>(PosSettings.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var DDLPOS = listHelper.GetFromList<long>(PosSettings.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLPOS), DDLPOS);
            //------------------------------
            var ddStatusList = await listHelper.GetList(6); // types
            ddlvm.AddList(nameof(ddStatusList), ddStatusList);
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysCustomerDto model, string DDLDefaultText = "", bool hasDefault = true)
        {
            if (model == null)
            {
                return await GetDDl(localization.GetResource1("Choose"));
            }

            var ddlvm = new DDLViewModel();

            int lang = session.Language;


            //int lang = session.GetData<int>("language");
            //if (model == null)
            //{
            //    return await GetDDl();
            //}

            //var ddlvm = new DDLViewModel();


            //var ddTypeList = await listHelper.GetList(313, selectedValue: model.Type ?? 0, hasDefault: hasDefault, defaultText: defaultText); // types
            //ddlvm.AddList(nameof(ddTypeList), ddTypeList);

            var ddSource = await listHelper.GetList(298, selectedValue: model.SourceId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddSource), ddSource);

            var ddNationality = await listHelper.GetList(40, selectedValue: model.NationalityId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddNationality), ddNationality);

            var ddEnable = await listHelper.GetList(328, selectedValue: model.Enable ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddEnable), ddEnable);

            var DDLIndustry = await listHelper.GetList(300, selectedValue: model.IndustryId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLIndustry), DDLIndustry);

            var ddPaymentType = await listHelper.GetList(332, selectedValue: model.PaymentType ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddPaymentType), ddPaymentType);

            var DDLSalesType = await listHelper.GetList(333, selectedValue: model.SalesType ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLSalesType), DDLSalesType);

            var DDLbanks = await listHelper.GetList(24, selectedValue: model.BankId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLbanks), DDLbanks);

            var DDLFielType = await listHelper.GetList(344, hasDefault: hasDefault, defaultText: localization.GetResource1("Choose"));
            ddlvm.AddList(nameof(DDLFielType), DDLFielType);


            var AllGroup = await mainServiceManager.SysCustomerGroupService.GetAll();
            var deptGroup = AllGroup.Data?.Where(d => d.IsDeleted == false && d.CusTypeId == 2 && d.FacilityId == session.FacilityId) ?? new List<SysCustomerGroupDto>();
            var DDLGroup = listHelper.GetFromList<long>(deptGroup.OrderBy(d => d.Name).Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), selectedValue: (long)(model.GroupId ?? 0), hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLGroup), DDLGroup);

            var AllCoType = await mainServiceManager.SysCustomerCoTypeService.GetAll();
            var deptCoType = AllCoType.Data ?? new List<SysCustomerCoTypeDto>();
            var DDLCoType = listHelper.GetFromList<long>(deptCoType.OrderBy(d => d.Name).Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), selectedValue: (long)(model.ComanyType ?? 0), hasDefault: true, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCoType), DDLCoType);

            var AllCity = await mainServiceManager.SysCitesService.GetAll();
            var deptCity = AllCity.Data?.Where(d => d.IsDeleted == false) ?? new List<SysCitesDto>();
            var DDLCity = listHelper.GetFromList<long>(deptCity.OrderBy(d => d.CityName).Select(s => new DDListItem<long> { Name = lang == 1 ? s.CityName : s.CityName2 ?? s.CityName, Value = s.CityID }), selectedValue: (long)(model.CityId ?? 0), hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCity), DDLCity);

            var AllEMP = await hrServiceManager.HrEmployeeService.GetAll();
            var deptEmp = AllEMP.Data?.Where(d => d.IsDeleted == false) ?? new List<HrEmployeeDto>();
            var DDLEmp = listHelper.GetFromList<long>(deptEmp.OrderBy(d => d.EmpName).Select(s => new DDListItem<long> { Name = lang == 1 ? s.EmpName : s.EmpName2 ?? s.EmpName, Value = s.Id }), selectedValue: (long)(model.EmpId ?? 0), hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLEmp), DDLEmp);

            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            //  var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);


            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = lang == 1 ? s.BraName : s.BraName2 ?? s.BraName, Value = s.BranchId }), selectedValue: (int)(model.BranchId ?? 0), hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            var DefaultCurrencyID = await mainServiceManager.SysExchangeRateService.GetOne(s => s.Id, x => x.Id == 1);
            var allCurrency = await mainServiceManager.SysExchangeRateService.GetAllVW();
            var deCurrency = allCurrency.Data.Where(d => d.IsDeleted == false);

            var DDLCurrencys = listHelper.GetFromList<long>(deCurrency.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }), selectedValue: model.CurrencyId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLCurrencys), DDLCurrencys);


            var GroupPrice = await salServiceManager.SalItemsPriceMService.GetAll();
            var GroupPrices = GroupPrice.Data.Where(d => d.IsDeleted == false);
            var DDLGroupPrices = listHelper.GetFromList<long>(GroupPrices.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }), selectedValue: model.ItemPriceMId ?? 0, hasDefault: hasDefault, defaultText: DDLDefaultText);
            ddlvm.AddList(nameof(DDLGroupPrices), DDLGroupPrices);

            var PosSetting = await salServiceManager.SalPosSettingService.GetAll();
            var PosSettings = PosSetting.Data.Where(d => d.IsDeleted == false);
            //var GroupPrices = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            // var DDLPOS = listHelper.GetFromList<long>(PosSettings.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var DDLPOS = listHelper.GetFromList<long>(PosSettings.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }));
            ddlvm.AddList(nameof(DDLPOS), DDLPOS);
            //-----------------------
            var ddStatusList = await listHelper.GetList(6); // types
            ddlvm.AddList(nameof(ddStatusList), ddStatusList);


            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        //[HttpPost]
        public async Task<IActionResult> Index(SysCustomerDto filter)
        {
            var chk = await permission.HasPermission(1958, PermissionType.Show);
            if (!chk)
            {
                return View("AccessDenied");
            }
            var model = new SearchVM<SysCustomerDto, SysCustomerVw>(filter, new List<SysCustomerVw>());
            // await GetDDl();
            await GetDDlWithDefault(filter, localization.GetResource1("All"));
            //   await GetDDl(localization.GetResource1("All"));
            try
            {
                var res = await Filter(filter, 2);
                if (res != null && res.Count() > 0)
                {
                    model.ListModel = res.ToList();
                    return View("Index", model);
                }
                
                return View("Index", model);
            }
            catch (Exception exp)
            {
                TempData.AddErrorMessage($"EXP: {exp.Message}");
                return View("Index", model);
            }
        }

        public async Task<IActionResult> Add()
        {
            setErrors();
            var chk = await permission.HasPermission(1958, PermissionType.Add);
            if (!chk)
            {
                return View("AccessDenied");
            }
            session.AddData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles, new List<SysCustomerFileDto>());
            //await GetDDl();
            await GetDDl(localization.GetResource1("Choose"));
            //  await GetDDlWithDefault( SysCustomerDto, hasDefault: true, defaultText: "choose");
            var daysList = new List<HrAttDayVM>();
            var allDays = await hrServiceManager.HrAttDayService.GetAll();
            if (allDays.Succeeded && allDays.Data != null)
            {
                foreach (var day in allDays.Data)
                {
                    daysList.Add(new HrAttDayVM { DayNo = day.DayNo ?? 0, DayName = session.Language == 1 ? day.DayName ?? "" : day.DayName2 ?? "", Selected = false });
                }
            }
            var facility = await accServiceManager.AccFacilityService.GetById(session.FacilityId);
            var sepChk = false;
            if (facility.Succeeded && facility.Data != null)
            {
                sepChk = facility.Data.SeparateAccountCustomer ?? false;
            }


            var vatChk = false;
            var getVat = await configurationHelper.GetValue(56, session.FacilityId);
            if (!string.IsNullOrEmpty(getVat))
            {
                if (getVat == "1")
                {
                    vatChk = true;
                }
            }

            var codeReadOnly = true;
            var getCodeCheck = await configurationHelper.GetValue(179, session.FacilityId);
            if (!string.IsNullOrEmpty(getCodeCheck))
            {
                if (getCodeCheck == "1")
                {
                    codeReadOnly = false;
                }
            }
            var customerDto = new SysCustomerDto { AccSeparate = sepChk, VatEnable = vatChk, CodeReadOnly = codeReadOnly };
            var vm = new SysCustomerAddVM { AttDays = daysList, SysCustomerDto = customerDto };

            var employee = await hrServiceManager.HrEmployeeService.GetOne(e => e.Id == session.EmpId && e.IsDeleted == false);
            if (employee.Succeeded && employee.Data != null)
            {
                vm.SysCustomerDto.EmpCode = employee.Data.EmpId;
                vm.SysCustomerDto.EmpName = (session.Language == 1) ? employee.Data.EmpName : employee.Data.EmpName2;
            }
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysCustomerAddVM obj)
        {
            //Console.WriteLine("in else of isValid --------------");
            //ModelState.AsEnumerable().ToList().ForEach(
            //    x => Console.WriteLine("KEY: {0} , VALUE: {1}", x.Key, ModelState.GetValidationState(x.Key).ToString()));
            setErrors();
            var chk = await permission.HasPermission(1958, PermissionType.Add);
            if (!chk)
            {
                return View("AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                await GetDDlWithDefault(obj.SysCustomerDto, localization.GetResource1("Choose"));
                // var ddl = await GetDDl(localization.GetResource1("Choose"));
                return View(obj);
            }
            // هنا نعبي بيانات الاتصال للشخص الاخر
            var files = session.GetData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles);
            if (files != null)
            {
                obj.SysCustomerFiles = files;
            }
            var addRes = await mainServiceManager.SysCustomerService.Add(obj);
            if (addRes.Succeeded)
            {
                session.AddData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles, new List<SysCustomerFileDto>());
                TempData.AddSuccessMessage("success");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await GetDDlWithDefault(obj.SysCustomerDto, localization.GetResource1("Choose"));
                //await GetDDl(localization.GetResource1("Choose"));
                TempData.AddErrorMessage($"{addRes.Status.message}");
                return View(obj);
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> AddContacts(SysCustomerContactDto obj)
        {
            setErrors();
            // var ddl = await GetDDlWithDefault(obj);
            try
            {
                if (obj != null)
                {
                    if (!ModelState.IsValid)
                    {
                        //TempData["itemsError"] = localization.GetMessagesResource("ArrorAddDetails");
                        return PartialView("_EditCustomerContacts", obj);
                    }

                    obj.IsDeleted = false;
                    if (obj.CusId == null || obj.CusId == 0)
                    {
                        TempData["itemsError"] = localization.GetMessagesResource("CustomerIdNotFound");
                        return PartialView("_EditCustomerContacts", obj);
                    }

                    var addContact = await mainServiceManager.SysCustomerContactService.Add(obj);
                    if (addContact.Succeeded)
                    {
                        var contactsList = new List<SysCustomerContactDto>();
                        var contacts = await mainServiceManager.SysCustomerContactService.GetAll(c => c.CusId == obj.CusId && c.IsDeleted != true);
                        if (contacts.Succeeded && contacts.Data != null)
                        {
                            contactsList = contacts.Data.ToList();
                        }
                        session.AddData<List<SysCustomerContactDto>>(SessionKeys.EditCustomerContacts, contactsList);
                        TempData["itemsSuccess"] = localization.GetMessagesResource("success");
                        return PartialView("_EditCustomerContacts", new SysCustomerContactDto { CusId = obj?.CusId ?? 0 });
                    }
                }
                TempData["itemsError"] = localization.GetMessagesResource("يرجى ادخال كل البيانات");
                return PartialView("_EditCustomerContacts", new SysCustomerContactDto { CusId = obj?.CusId ?? 0 });
            }
            catch (Exception ex)
            {
                TempData["itemsError"] = $"Exp: {ex.Message}";
                return PartialView("_EditCustomerContacts", obj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteContacts(long Id, long customerId)
        {
            setErrors();
            try
            {
                if (Id != 0 && customerId != 0)
                {
                    var delContact = await mainServiceManager.SysCustomerContactService.Remove(Id);
                    if (delContact.Succeeded)
                    {
                        var contactsList = new List<SysCustomerContactDto>();
                        var contacts = await mainServiceManager.SysCustomerContactService.GetAll(c => c.CusId == (int)customerId && c.IsDeleted != true);
                        if (contacts.Succeeded && contacts.Data != null)
                        {
                            contactsList = contacts.Data.ToList();
                        }

                        session.AddData<List<SysCustomerContactDto>>(SessionKeys.EditCustomerContacts, contactsList);
                        TempData["itemsSuccess"] = localization.GetMessagesResource("success");
                        return PartialView("_EditCustomerContacts", new SysCustomerContactDto { CusId = (int)customerId });
                    }
                }

                TempData["itemsError"] = localization.GetMessagesResource("NoItemFoundToDelete");
                return PartialView("_EditCustomerContacts", new SysCustomerContactDto { CusId = (int)customerId });
            }
            catch (Exception ex)
            {
                TempData["itemsError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("_EditCustomerContacts", new SysCustomerContactDto { CusId = (int)customerId });
            }

        }

        public async Task<IActionResult> Edit(string encId)
        {
            setErrors();
            var chk = await permission.HasPermission(1958, PermissionType.Edit);
            if (!chk)
            {
                return View("AccessDenied");
            }
            session.AddData<List<SysFileDto>>(SessionKeys.EditTempFiles, new List<SysFileDto>());
            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"NoIdInUpdate");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                long mainId = EncryptionHelper.Decrypt<long>(encId);
                var getItem = await mainServiceManager.SysCustomerService.GetForUpdate<SysCustomerEditDto>(mainId);
                if (getItem.Succeeded && getItem.Data != null)
                {
                    var editItem = new SysCustomerEditDto();
                    editItem = getItem.Data;
                    var contactsList = new List<SysCustomerContactDto>();
                    var contacts = await mainServiceManager.SysCustomerContactService.GetAll(c => c.CusId == (int)mainId && c.IsDeleted != true);
                    if (contacts.Succeeded && contacts.Data != null)
                    {
                        contactsList = contacts.Data.ToList();
                    }
                    session.AddData<List<SysCustomerContactDto>>(SessionKeys.EditCustomerContacts, contactsList);
                    var ddl = await GetDDlWithDefault(editItem);

                    var daysList = new List<HrAttDayVM>();
                    var allDays = await hrServiceManager.HrAttDayService.GetAll();
                    if (allDays.Succeeded && allDays.Data != null)
                    {
                        foreach (var day in allDays.Data)
                        {
                            daysList.Add(new HrAttDayVM { DayNo = day.DayNo ?? 0, DayName = session.Language == 1 ? day.DayName ?? "" : day.DayName2 ?? "", Selected = false });
                        }
                    }
                    var currentDays = editItem.DaysOfVisit ?? "";
                    var currList = currentDays.Split(",");
                    if (!string.IsNullOrEmpty(currentDays))
                    {
                        foreach (var d in currList)
                        {
                            foreach (var day in daysList)
                            {
                                if (day.DayNo == int.Parse(d))
                                {
                                    day.Selected = true;
                                    break;
                                }
                            }
                        }
                    }
                    editItem.AttDays = daysList;

                    //get emp
                    if (editItem.EmpId > 0)
                    {
                        var getEmp = await hrServiceManager.HrEmployeeService.GetOne(e => e.Id == editItem.EmpId && e.IsDeleted == false);
                        if (getEmp.Succeeded && getEmp != null)
                        {
                            editItem.EmpCode = getEmp.Data.EmpId;
                            editItem.EmpName = getEmp.Data.EmpName;
                        }
                    }
                    if (editItem.AccAccountId > 0)
                    {
                        var getAcc = await accServiceManager.AccAccountService.GetOne(e => e.AccAccountId == editItem.AccAccountId && e.IsDeleted == false);
                        if (getAcc.Succeeded && getAcc != null)
                        {
                            editItem.AccAccountCode = getAcc.Data.AccAccountCode;
                            editItem.AccAccountName = getAcc.Data.AccAccountName;
                        }
                    }
                    return View(editItem);

                }
                TempData.AddErrorMessage($"NoIdInUpdate");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                TempData.AddErrorMessage($"{exp.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SysCustomerEditDto obj)
        {
            setErrors();
            var chk = await permission.HasPermission(1958, PermissionType.Edit);
            if (!chk)
            {
                return View("AccessDenied");
            }
            if (!ModelState.IsValid)
            {
                await GetDDlWithDefault(obj, localization.GetResource1("Choose"));
                // var ddl = await GetDDl(localization.GetResource1("Choose"));
                return View(obj);
            }
            // هنا نعبي بيانات الاتصال للشخص الاخر
            var editRes = await mainServiceManager.SysCustomerService.Update(obj);
            if (editRes.Succeeded)
            {
                session.AddData<List<SysCustomerFileDto>>(SessionKeys.EditTempCustomerFiles, new List<SysCustomerFileDto>());
                TempData.AddSuccessMessage("success");
                return RedirectToAction(nameof(Index));
            }
            else
            {
                await GetDDlWithDefault(obj, localization.GetResource1("Choose"));
                //await GetDDl(localization.GetResource1("Choose"));
                TempData.AddErrorMessage($"{editRes.Status.message}");
                return View(obj);
            }
        }

        public async Task<IActionResult> Edit222(string encId)
        {
            setErrors();
            var chk = await permission.HasPermission(1958, PermissionType.Edit);
            if (!chk)
            {
                return View("AccessDenied");
            }
            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"NoIdInUpdate");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                long mainId = EncryptionHelper.Decrypt<long>(encId);
                SysCustomerAddVM Customer;
                var getItem = await mainServiceManager.SysCustomerService.GetForUpdate<SysCustomerEditDto>(mainId);
                //   Customer.SysCustomerDto = getItem.Data;
                if (getItem.Succeeded && getItem.Data != null)
                {
                    var ddl = await GetDDlWithDefault(getItem.Data);
                    // var ddl = await GetDDlWithDefault(getItem.Data);
                    return View(getItem.Data);
                }
                TempData.AddErrorMessage($"NoIdInUpdate");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                TempData.AddErrorMessage($"{exp.Message}");
                return RedirectToAction(nameof(Index));
            }
        }
        
        public async Task<IActionResult> Delete(long Id)
        {
            try
            {
                var chk = await permission.HasPermission(1958, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

                if (Id == 0)
                {
                    TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                    return RedirectToAction(nameof(Index));
                }

                //chk if customer has contract
                var chkCustomer = await opmServiceManager.OpmContractService.GetAll(c => c.TransTypeId == 1 && c.CustomerId > 0 && c.CustomerId == Id && c.IsDeleted == false);
                if (chkCustomer.Succeeded)
                {
                    if(chkCustomer.Data.Any())
                    {
                        TempData.AddErrorMessage("هذا العميل مرتبط بعمليات أخرى");
                    }
                    else
                    {
                        var delete = await mainServiceManager.SysCustomerService.Remove(Id);
                        if (delete.Succeeded)
                            TempData.AddSuccessMessage("success");
                        else
                            TempData.AddErrorMessage($"{delete.Status.message}");
                    }
                    
                    return RedirectToAction("Index");
                }
                TempData.AddErrorMessage($"{chkCustomer.Status.message}");
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction("Index");
            }
        }

        [NonAction]
        public async Task<IQueryable<SysCustomerVw>?> Filter(SysCustomerDto filter, int CusTypeId)
        {
            try
            {
                var items = await mainServiceManager.SysCustomerService.GetAllVW(a => a.IsDeleted == false && a.CusTypeId == CusTypeId);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    // var res1 = res.Where(s => s.IdNo == filter.IdNo);
                    //var res2 = res.Where(s => s.IdNo != null && s.IdNo.Contains("0123456789")).ToList();
                    if (filter == null)
                    {
                        return res;
                    }
                    if (!string.IsNullOrEmpty(filter.Code))
                    {
                        res = res.Where(s => s.Code != null && s.Code.Contains(filter.Code));
                    }
                    if (!string.IsNullOrEmpty(filter.Mobile))
                    {
                        res = res.Where(s => s.Mobile != null && s.Mobile.Contains(filter.Mobile));
                    }
                    if (!string.IsNullOrEmpty(filter.Name))
                    {
                        res = res.Where(s => s.Name != null && s.Name.Contains(filter.Name));
                    }
                    if (!string.IsNullOrEmpty(filter.IdNo))
                    {
                        res = res.Where(s => s.IdNo != null && s.IdNo.Contains(filter.IdNo));
                    }
                    if (!string.IsNullOrEmpty(filter.BankAccount))
                    {
                        res = res.Where(s => s.BankAccount != null && s.BankAccount.Contains(filter.BankAccount));
                    }
                    if (filter.ItemPriceMId != null && filter.ItemPriceMId > 0)
                    {
                        res = res.Where(s => s.ItemPriceMId != null && s.ItemPriceMId.Equals(filter.ItemPriceMId));
                    }
                    if (filter.Enable != null && filter.Enable > 0)
                    {
                        res = res.Where(s => s.Enable != null && s.Enable.Equals(filter.Enable));
                    }
                    if (filter.ComanyType != null && filter.ComanyType > 0)
                    {
                        res = res.Where(s => s.ComanyType != null && s.ComanyType.Equals(filter.ComanyType));
                    }
                    if (filter.BranchId != null && filter.BranchId > 0)
                    {
                        res = res.Where(s => s.BranchId != null && s.BranchId.Equals(filter.BranchId));
                    }
                    if (filter.BankId != null && filter.BankId > 0)
                    {
                        res = res.Where(s => s.BankId != null && s.BankId.Equals(filter.BankId));
                    }
                    if (filter.GroupId != null && filter.GroupId > 0)
                    {
                        res = res.Where(s => s.GroupId != null && s.GroupId.Equals(filter.GroupId));
                    }
                    if (filter.CurrencyId != null && filter.CurrencyId > 0)
                    {
                        res = res.Where(s => s.CurrencyId != null && s.CurrencyId.Equals(filter.CurrencyId));
                    }
                    if (filter.SalesType != null && filter.SalesType > 0)
                    {
                        res = res.Where(s => s.SalesType != null && s.SalesType.Equals(filter.SalesType));
                    }
                    if (filter.IndustryId != null && filter.IndustryId > 0)
                    {
                        res = res.Where(s => s.IndustryId != null && s.IndustryId.Equals(filter.IndustryId));
                    }
                    if (filter.CityId != null && filter.CityId > 0)
                    {
                        res = res.Where(s => s.CityId.Equals(filter.CityId));
                    }
                    if (filter.EmpId != null && filter.EmpId > 0)
                    {
                        res = res.Where(s => s.EmpId != null && s.EmpId.Equals(filter.EmpId));
                    }

                    if (!res.Any())
                        TempData.AddSuccessMessage($"{localization.GetResource1("NosearchResult")}");
                    return res;
                }

                TempData.AddErrorMessage($"{items.Status.message}");
                return null;
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return null;
            }
        }
        [HttpPost]
        public async Task<IActionResult> ExportExcel(SysCustomerDto filter, string format = ExportFormat.Excel)
        {
            await GetDDlWithDefault(filter, localization.GetResource1("All"));
            var model = new SearchVM<SysCustomerDto, SysCustomerVw>(filter, new List<SysCustomerVw>());
            try
            {
                var res = await Filter(filter, 2);
                if (res != null && res.Count() > 0)
                {
                    switch (format)
                    {
                        case ExportFormat.Excel:
                            return File(
                                await exportService.ExportToExcel<SysCustomerVw>(res.ToList()),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                "AccCostCenter.xlsx");

                        case ExportFormat.Csv:
                            return File(await exportService.ExportToCsv<SysCustomerVw>(res.ToList()),
                                "application/csv",
                                "data.csv");

                        case ExportFormat.Html:
                            return File(await exportService.ExportToHtml<SysCustomerVw>(res.ToList()),
                                "application/csv",
                                "data.html");

                        case ExportFormat.Json:
                            return File(await exportService.ExportToJson<SysCustomerVw>(res.ToList()),
                                "application/json",
                                "data.json");

                        case ExportFormat.Xml:
                            return File(await exportService.ExportToXml<SysCustomerVw>(res.ToList()),
                                "application/xml",
                                "data.xml");

                        case ExportFormat.Yaml:
                            return File(await exportService.ExportToYaml<SysCustomerVw>(res.ToList()),
                                "application/yaml",
                                "data.yaml");
                    }

                    model.ListModel = res.ToList();
                    return View("Index", model);
                }

                return View("Index", model);
            }
            catch (Exception exp)
            {

                TempData.AddErrorMessage($"{exp.Message}");
                return View("Index", model);
            }
        }
    }
}
