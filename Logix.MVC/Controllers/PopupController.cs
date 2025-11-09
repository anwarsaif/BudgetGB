using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.GB;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.DTOs.OPM;
using Logix.Application.DTOs.PM.PmProjects;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.ACC;
using Logix.Domain.GB;
using Logix.MVC.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class PopupController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IHrServiceManager hrServiceManager;
        private readonly IAccServiceManager accServiceManager;
        private readonly IPMServiceManager pmServiceManager;
        private readonly IOPMServiceManager opmServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly IScreenPropertiesHelper screenPropertiesHelper;
        private readonly IGbServiceManager gbServiceManager;
        private readonly ISessionHelper session;
        public PopupController(
            IMainServiceManager mainServiceManager,
            IHrServiceManager hrServiceManager,
            IAccServiceManager accServiceManager,
            IPMServiceManager pmServiceManager,
            IOPMServiceManager opmServiceManager,
            IPermissionHelper permission,
            ISessionHelper session,
            IDDListHelper listHelper,
            ISysConfigurationHelper configurationHelper,
            IScreenPropertiesHelper screenPropertiesHelper,
            IGbServiceManager gbServiceManager

            )
        {
            this.mainServiceManager = mainServiceManager;
            this.hrServiceManager = hrServiceManager;
            this.accServiceManager = accServiceManager;
            this.pmServiceManager = pmServiceManager;
            this.opmServiceManager = opmServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.configurationHelper = configurationHelper;
            this.screenPropertiesHelper = screenPropertiesHelper;
            this.gbServiceManager = gbServiceManager;
            this.session = session;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDl_Employees()
        {
            int lang = session.GetData<int>("language");
            var ddlvm = new DDLViewModel();

            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            //  var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }));
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);

            var ddStatusList = await listHelper.GetList(6); // types
            ddlvm.AddList(nameof(ddStatusList), ddStatusList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDl()
        {
            int lang = session.GetData<int>("language");
            var ddlvm = new DDLViewModel();
            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            //  var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = lang == 1 ? s.Name : s.Name2 ?? s.Name, Value = s.Id }));
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }));
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);



            var ddStatusList = await listHelper.GetList(6); // types
            ddlvm.AddList(nameof(ddStatusList), ddStatusList);


            var allGroup = await accServiceManager.AccGroupService.GetAll(d => d.IsDeleted == false && d.SystemId == 65 && d.FacilityId == session.FacilityId);
            var deGroup = allGroup.Data.Where(d => d.IsDeleted == false && d.SystemId == 65 && d.FacilityId == session.FacilityId);
            var ddGroupList = listHelper.GetFromList<long>(deGroup.Select(s => new DDListItem<long> { Name = lang == 1 ? s.AccGroupName : s.AccGroupName2 ?? s.AccGroupName, Value = s.AccGroupId }));
            ddlvm.AddList(nameof(ddGroupList), ddGroupList);


            var allParent = await gbServiceManager.SubitemsService.GetAll(d => d.IsSub == true && d.IsDeleted == false && d.SystemId == 65 && d.FacilityId == session.FacilityId);
            var deParent = allParent.Data;
            var ddParentList = listHelper.GetFromList<long>(deParent.Select(s => new DDListItem<long> { Name = lang == 1 ? (s.AccAccountCode + "-" + "ح/" + (s.AccAccountName)) : (s.AccAccountCode + "-" + "ِAcc/" + (s.AccAccountName2)) ?? s.AccAccountName, Value = s.AccAccountId }), defaultText: "all");
            ddlvm.AddList(nameof(ddParentList), ddParentList);
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchScreens(string term)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                var screens = new List<ScreenSearchDto>();
                if (string.IsNullOrEmpty(term))
                {
                    return Ok(screens);
                }

                var getScreens = await mainServiceManager.SysSystemService.SearchScreens(term);
                if (getScreens.Succeeded && getScreens.Data != null)
                {
                    if (getScreens.Data.Count() > 0)
                    {
                        foreach (var item in getScreens.Data)
                        {
                            if (!string.IsNullOrEmpty(item.ScreenUrl))
                            {
                                item.ScreenUrl = item.ScreenUrl.StartsWith("/") ? item.ScreenUrl : item.Folder + "/" + item.ScreenUrl;
                                item.ScreenUrl = $"{session.OldBaseUrl}{item.ScreenUrl}";
                            }
                            if (!item.IsCore)
                            {
                                item.Url = item.ScreenUrl;
                            }

                            item.ScreenName = session.Language == 1 ? item.ScreenName : item.ScreenName2;
                            screens.Add(item);
                        }
                        return Ok(screens);

                    }
                }

                //return Ok("");
                return Ok(screens);
            }
            catch (Exception ex)
            {
                return BadRequest($"{ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeesPopUp(HrEmployeeDto filter)
        {
            await GetDDl_Employees();
            if (filter != null)
            {
                var list = new List<HrEmployeeDto>();
                var screens = await hrServiceManager.HrEmployeeService.GetAll(e => e.IsDeleted == false);
                if (screens.Succeeded && screens.Data != null)
                {
                    list.AddRange(screens.Data);
                }

                if (!string.IsNullOrEmpty(filter.EmpName))
                {
                    list = list.Where(e => e.EmpName != null && e.EmpName.ToLower().Contains(filter.EmpName.ToLower())).ToList();
                }

                if (filter.DeptId != null && filter.DeptId > 0)
                {
                    list = list.Where(e => e.DeptId == filter.DeptId).ToList();
                }
                if (filter.StatusId != null && filter.StatusId > 0)
                {
                    list = list.Where(e => e.StatusId == filter.StatusId).ToList();
                }

                return PartialView("PopUps/Employees/_EmployeesPopUp", list);
            }
            else
            {
                var screens = await hrServiceManager.HrEmployeeService.GetAll();
                if (screens.Succeeded && screens.Data != null)
                {
                    return PartialView("PopUps/Employees/_EmployeesPopUp", screens.Data);
                }
            }

            return PartialView("PopUps/Employees/_EmployeesPopUp");

        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeNameById(string Id)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(Id))
                {
                    return Ok("");
                }
                var getEmp = await hrServiceManager.HrEmployeeService.GetOne(a => a.EmpId == Id && a.IsDeleted == false && a.StatusId == 1); ;
                if (getEmp.Succeeded && getEmp.Data != null)
                {
                    if (session.Language == 1)
                    {
                        return Ok(getEmp.Data.EmpName);
                    }
                    else
                    {
                        return Ok(getEmp.Data.EmpName2 ?? getEmp.Data.EmpName);
                    }

                }
                else
                {
                    return Ok("");
                }
            }
            catch (Exception)
            {

                return Ok("");
            }

        }

        public async Task<IActionResult> GetAccountNameById(string code)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.AccAccountCode == code);
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.Language == 1)
                        {
                            return Ok(getAcc.Data.Single().AccAccountName);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().AccAccountName2);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsPopUp(AccAccountDto filter)
        {
            if (filter != null)
            {
                var list = new List<AccAccountsSubHelpeVw>();
                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.Isdel == false));
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName != null && e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.AccAccountCode))
                {
                    list = list.Where(e => e.AccAccountCode != null && e.AccAccountCode.StartsWith(filter.AccAccountCode)).ToList();
                }

                return PartialView("PopUps/Accounts/_AccountsPopUp", list);
            }
            else
            {
                var accounts = await accServiceManager.AccAccountService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Accounts/_AccountsPopUp", accounts.Data.Where(a => a.IsDeleted == false));
                }
            }

            return PartialView("PopUps/Accounts/_AccountsPopUp");

        }

        [HttpGet]
        public async Task<IActionResult> GetAccountsPopUpWithClass(AccAccountDto filter)
        {
            if (filter != null)
            {
                var list = new List<AccAccountDto>();
                var accounts = await accServiceManager.AccAccountService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.IsDeleted == false));
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName != null && e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.AccAccountCode))
                {
                    list = list.Where(e => e.AccAccountCode != null && e.AccAccountCode.StartsWith(filter.AccAccountCode)).ToList();
                }

                return PartialView("PopUps/Accounts/_AccountsPopUpWithClass", list);
            }
            else
            {
                var accounts = await accServiceManager.AccAccountService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Accounts/_AccountsPopUpWithClass", accounts.Data.Where(a => a.IsDeleted == false));
                }
            }

            return PartialView("PopUps/Accounts/_AccountsPopUpWithClass");

        }




        public async Task<IActionResult> GetCostCenterNameById(string code)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                var getAcc = await accServiceManager.AccCostCenteHelpVwService.GetAll(a => a.CostCenterCode == code && a.Isdel == false);
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.Language == 1)
                        {
                            return Ok(getAcc.Data.Single().CostCenterName);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().CostCenterName2 ?? getAcc.Data.Single().CostCenterName);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCostCentersPopUp(AccCostCenterDto filter)
        {
            if (filter != null)
            {
                var list = new List<AccCostCenteHelpVw>();
                var accounts = await accServiceManager.AccCostCenteHelpVwService.GetAll(a => a.Isdel == false);
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.Isdel == false));
                }

                if (!string.IsNullOrEmpty(filter.CostCenterName))
                {
                    list = list.Where(e => e.CostCenterName != null && e.CostCenterName.ToLower().Contains(filter.CostCenterName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.CostCenterCode))
                {
                    list = list.Where(e => e.CostCenterCode != null && e.CostCenterCode.StartsWith(filter.CostCenterCode)).ToList();
                }

                return PartialView("PopUps/CostCenters/_CostCentersPopUp", list);
            }
            else
            {
                var costCenters = await accServiceManager.AccCostCenterService.GetAll();
                if (costCenters.Succeeded && costCenters.Data != null)
                {
                    return PartialView("PopUps/CostCenters/_CostCentersPopUp", costCenters.Data.Where(a => a.IsDeleted == false));
                }
            }

            return PartialView("PopUps/CostCenters/_CostCentersPopUp");

        }



        public async Task<IActionResult> GetContractNameById(string code, int contractType)
        {
            try
            {
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }

                var getContract = await opmServiceManager.OpmContractService.GetAll(a => a.Code != null && a.Code.Equals(code) && a.IsDeleted == false && a.TransTypeId == contractType);
                if (getContract.Succeeded && getContract.Data != null)
                {
                    if (getContract.Data.Any())
                    {
                        return Ok(getContract.Data.Single().Name);
                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }



        [HttpGet]
        public async Task<IActionResult> GetContractsPopUp(OpmContractDto filter)
        {
            if (filter != null)
            {
                var list = new List<OpmContractDto>();
                var contracts = await opmServiceManager.OpmContractService.GetAll(c => c.IsDeleted == false && c.TransTypeId == filter.TransTypeId);
                if (contracts.Succeeded && contracts.Data != null)
                {
                    list.AddRange(contracts.Data.Where(c => c.IsDeleted == false && c.FacilityId == session.FacilityId));
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    list = list.Where(e => e.Name != null && e.Name.ToLower().Contains(filter.Name.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    list = list.Where(e => e.Code != null && e.Code.StartsWith(filter.Code)).ToList();
                }

                return PartialView("PopUps/Contracts/_ContractsPopUp", list);
            }
            else
            {
                var contracts = await opmServiceManager.OpmContractService.GetAll();
                if (contracts.Succeeded && contracts.Data != null)
                {
                    return PartialView("PopUps/Contracts/_ContractsPopUp", contracts.Data.Where(c => c.IsDeleted == false && c.FacilityId == session.FacilityId));
                }
            }

            return PartialView("PopUps/Contracts/_ContractsPopUp");
        }


        public async Task<IActionResult> GetprojectNameById(string code)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                var getProject = await pmServiceManager.PMProjectsService.GetAll(a => a.Code == Convert.ToInt64(code));
                if (getProject.Succeeded && getProject.Data != null)
                {
                    if (getProject.Data.Any())
                    {
                        if (session.Language == 1)
                        {
                            return Ok(getProject.Data.Single().Name);
                        }
                        else
                        {
                            return Ok(getProject.Data.Single().Name2 ?? getProject.Data.Single().Name);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetprojectsPopUp(PMProjectsDto filter)
        {
            if (filter != null)
            {
                var list = new List<PMProjectsDto>();
                var projects = await pmServiceManager.PMProjectsService.GetAll();
                if (projects.Succeeded && projects.Data != null)
                {
                    list.AddRange(projects.Data.Where(a => a.IsDeleted == false));
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    list = list.Where(e => e.Name != null && e.Name.ToLower().Contains(filter.Name.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.Code.ToString()))
                {
                    list = list.Where(e => e.Code != null && e.Code.ToString().StartsWith(filter.Code.ToString())).ToList();
                }

                return PartialView("PopUps/Projects/_ProjectsPopUp", list);
            }
            else
            {
                var projects = await pmServiceManager.PMProjectsService.GetAll();
                if (projects.Succeeded && projects.Data != null)
                {
                    return PartialView("PopUps/Projects/_ProjectsPopUp", projects.Data.Where(a => a.IsDeleted == false));
                }
            }
            return PartialView("PopUps/Projects/_ProjectsPopUp");
        }




        public async Task<IActionResult> GetCustomersById(string code, int? idType)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                var getAcc = await mainServiceManager.SysCustomerService.GetAll(a => a.Code == code && a.CusTypeId == idType && a.IsDeleted == false);
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.Language == 1)
                        {
                            return Ok(getAcc.Data.Single().Name);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().Name2 ?? getAcc.Data.Single().Name);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomersPopUp(SysCustomerDto filter)
        {
            if (filter != null)
            {
                var list = new List<SysCustomerDto>();
                var accounts = await mainServiceManager.SysCustomerService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.IsDeleted == false));
                }

                if (!string.IsNullOrEmpty(filter.Name))
                {
                    list = list.Where(e => e.Name.ToLower().Contains(filter.Name.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.Code))
                {
                    list = list.Where(e => e.Code != null && e.Code.Equals(filter.Code)).ToList();
                }

                if (filter.IdType != null && filter.IdType != 0)
                {
                    list = list.Where(e => e.CusTypeId != null && e.CusTypeId.Equals(filter.IdType)).ToList();
                }

                return PartialView("PopUps/Customer/_CustomerPopUp", list);
            }
            else
            {
                var accounts = await mainServiceManager.SysCustomerService.GetAll();
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Customer/_CustomerPopUp", accounts.Data.Where(a => a.IsDeleted == false));
                }
            }

            return PartialView("PopUps/Customer/_CustomerPopUp");

        }

        #region "GB"
        public async Task<IActionResult> GetSubitemsNameById(string code, long? accGroupId)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();


                var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.AccAccountCode == code
                && a.Isdel == false && a.SystemId == 65
                && a.AccGroupId == accGroupId
                        && allowedIds.Contains(a.AccAccountId)
                );
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.GetData<int>("language") == 1)
                        {
                            return Ok(getAcc.Data.Single().AccAccountName);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().AccAccountName2);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubitemsPopUp(AccAccountDto filter)
        {
            await GetDDl();

            // تحويل الصلاحيات إلى قائمة من الأرقام
            var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
            var allowedIds = permissionsOverAccAccountID
       .Split(',', StringSplitOptions.RemoveEmptyEntries)
       .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
       .ToList();


            // جلب البيانات مع الفلاتر الأساسية
            var result = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
      a.Isdel == false &&
      a.SystemId == 65 &&
      allowedIds.Contains(a.AccAccountId) // فقط عندما توجد صلاحيات
  );


            if (result.Succeeded && result.Data != null)
            {
                var list = result.Data.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filter?.AccAccountName))
                {
                    var name = filter.AccAccountName.Trim().ToLower();
                    list = list.Where(e => !string.IsNullOrWhiteSpace(e.AccAccountName) &&
                                           e.AccAccountName.ToLower().Contains(name));
                }

                if (!string.IsNullOrWhiteSpace(filter?.AccAccountCode))
                {
                    list = list.Where(e => !string.IsNullOrWhiteSpace(e.AccAccountCode) &&
                                           e.AccAccountCode.StartsWith(filter.AccAccountCode));
                }

                if (filter?.AccGroupId != null && filter.AccGroupId != 0)
                {
                    list = list.Where(e => e.AccGroupId == filter.AccGroupId);
                }

                if (filter?.AccAccountParentId != null && filter.AccAccountParentId != 0)
                {
                    list = list.Where(e => e.AccAccountParentID == filter.AccAccountParentId);
                }

                return PartialView("PopUps/Accounts/_SubitemsPopUp", list.ToList());
            }

            return PartialView("PopUps/Accounts/_SubitemsPopUp");
        }

        public async Task<JsonResult> GetItemsValue(string Code)
        {
            // جلب بيانات الحساب بناءً على الكود
            var itemsIdResponse = await gbServiceManager.SubitemsService.GetOne(
                x => x.AccAccountCode == Code && x.IsDeleted == false && x.SystemId == 65);

            if (itemsIdResponse?.Data == null)
            {
                return Json(new { error = "الحساب غير موجود" });
            }

            var itemsId = itemsIdResponse.Data.AccAccountId;
            var accGroupId = itemsIdResponse.Data.AccGroupId ?? 0;
            var additionalValue = itemsIdResponse.Data.AccAccountName ?? "";
            var note = itemsIdResponse.Data.Note ?? "";
            BudgTransactionBalanceVW? dataResponse = null;

            if (itemsIdResponse.Data.itemType == 1)
            {
                var response = await gbServiceManager.BudgTransactionBalanceVWService.GetOne(
                    x => x.AccAccountId == itemsId
                      && x.FinYear == session.FinYear
                      && x.BudgTypeId == 1
                      && x.FlagDelete == false);

                dataResponse = response?.Data;
            }
            else
            {
                var response = await gbServiceManager.BudgTransactionBalanceVWService.GetOne(
                    x => x.AccAccountId == itemsId
                      && x.BudgTypeId == 2
                      && x.FlagDelete == false);

                dataResponse = response?.Data;
            }



            if (dataResponse == null)
            {
                var result1 = new
                {
                    accGroupId,
                    additionalValue,
                    note,
                    initialLinks = 0.0m,
                    amountInitial = 0.0m,
                    amountTransfersFrom = 0.0m,
                    amountTransfersTo = 0.0m,
                    amountReinforcements = 0.0m,
                    amountLinks = 0.0m,
                    amountDiscounts = 0.0m,
                    amountTotal = 0.0m
                };
                return Json(result1);
            }

            var data = dataResponse;

            var result = new
            {
                accGroupId,
                additionalValue,
                note,
                initialLinks = data.InitialLinks.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountInitial = data.AmountInitial.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersFrom = data.AmountTransfersFrom.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersTo = data.AmountTransfersTo.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountReinforcements = data.AmountReinforcements.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountLinks = data.AmountLinks.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountDiscounts = data.AmountDiscounts.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTotal = data.AmountTotal.ToString("N2", new System.Globalization.CultureInfo("en-US"))
            };


            return Json(result);
        }




        public async Task<IActionResult> GetSubitemsNamelinkById(string code, long? accGroupId)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }

                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();

                var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.AccAccountCode == code && a.Isdel == false
                && a.SystemId == 65
                && a.AccGroupId == accGroupId
                && allowedIds.Contains(a.AccAccountId)
                );
                // تحويل الصلاحيات إلى قائمة من الأرقام



                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.GetData<int>("language") == 1)
                        {
                            return Ok(getAcc.Data.Single().AccAccountName);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().AccAccountName2);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubitemsPopUplink(AccAccountDto filter)
        {
            await GetDDl();

            if (filter != null)
            {

                var list = new List<AccAccountsSubHelpeVw>();
                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();


                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false
                && a.SystemId == 65
                            && allowedIds.Contains(a.AccAccountId)
                );
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.Isdel == false && a.SystemId == 65));
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName != null && e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.AccAccountCode))
                {
                    list = list.Where(e => e.AccAccountCode != null && e.AccAccountCode.StartsWith(filter.AccAccountCode)).ToList();
                }
                if (filter.AccGroupId != null && filter.AccGroupId != 0)
                {
                    list = list.Where(e => e.AccGroupId != null && e.AccGroupId.Equals(filter.AccGroupId)).ToList();
                }
                return PartialView("PopUps/Accounts/_SubitemsPopUpLINK", list);
            }
            else
            {


                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false && a.SystemId == 65);
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Accounts/_SubitemsPopUpLINK", accounts.Data.Where(a => a.Isdel == false));
                }
            }

            return PartialView("PopUps/Accounts/_SubitemsPopUpLINK");

        }
        public async Task<JsonResult> GetItemsValueLink(string Code)
        {
            // جلب بيانات الحساب بناءً على الكود
            var itemsIdResponse = await gbServiceManager.SubitemsService.GetOne(
                x => x.AccAccountCode == Code && x.IsDeleted == false && x.SystemId == 65);

            if (itemsIdResponse?.Data == null)
            {
                return Json(new { error = "الحساب غير موجود" });
            }

            var itemsId = itemsIdResponse.Data.AccAccountId;
            var accGroupId = itemsIdResponse.Data.AccGroupId ?? 0;
            var additionalValue = itemsIdResponse.Data.AccAccountName ?? "";
            var note = itemsIdResponse.Data.Note ?? "";
            BudgTransactionBalanceVW? dataResponse = null;

            if (itemsIdResponse.Data.itemType == 1)
            {
                var response = await gbServiceManager.BudgTransactionBalanceVWService.GetOne(
                    x => x.AccAccountId == itemsId
                      && x.FinYear == session.FinYear
                      && x.BudgTypeId == 1
                      && x.FlagDelete == false);

                dataResponse = response?.Data;
            }
            else
            {
                var response = await gbServiceManager.BudgTransactionBalanceVWService.GetOne(
                    x => x.AccAccountId == itemsId
                      && x.BudgTypeId == 2
                      && x.FlagDelete == false);

                dataResponse = response?.Data;
            }



            if (dataResponse == null)
            {
                var result1 = new
                {
                    accGroupId,
                    additionalValue,
                    note,
                    initialLinks = 0.0m,
                    amountInitial = 0.0m,
                    amountTransfersFrom = 0.0m,
                    amountTransfersTo = 0.0m,
                    amountReinforcements = 0.0m,
                    amountLinks = 0.0m,
                    amountDiscounts = 0.0m,
                    amountTotal = 0.0m
                };
                return Json(result1);
            }



            var data = dataResponse;

            var result = new
            {
                accGroupId,
                additionalValue,
                note,
                initialLinks = data.InitialLinks.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountInitial = data.AmountInitial.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersFrom = data.AmountTransfersFrom.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersTo = data.AmountTransfersTo.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountReinforcements = data.AmountReinforcements.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountLinks = data.LinksFinal.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountDiscounts = data.AmountDiscounts.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTotal = (data.AmountTotal + data.InitialLinks).ToString("N2", new System.Globalization.CultureInfo("en-US"))
            };

            return Json(result);
        }

        public async Task<JsonResult> GetItemsValueLink2(string Code)
        {
            var itemsIdResponse = await gbServiceManager.SubitemsService.GetOne(x => x.AccAccountCode == Code && x.IsDeleted == false && x.SystemId == 65);
            if (itemsIdResponse.Data == null)
            {
                return Json(new { error = "Invalid Code" });
            }

            var itemsId = itemsIdResponse.Data.AccAccountId;

            // تعديل الاستعلام بحيث يتم تجاهل Finyear إذا كان BudgTypeId == 2
            var dataResponse = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(x =>
                x.AccAccountId == itemsId && (x.BudgTypeId != 2 && x.Finyear == session.FinYear || x.BudgTypeId == 2) && x.IsDeleted == false);
            var data = dataResponse.Data ?? new List<BudgTransactionDetailesVw>();

            var canceledItems = await gbServiceManager.BudgTransactionsService.GetAllVW(a =>
                a.DocTypeId == 9 && a.FlagDelete == false);
            var canceledRefs = canceledItems.Data.Select(x => x.ReferenceNo).ToHashSet();

            decimal AmountInitial = data.Where(item =>
                (itemsIdResponse.Data.itemType == 1 && item.DocTypeId == 1) ||
                (itemsIdResponse.Data.itemType == 2 && item.DocTypeId == 7))
                .Sum(item => item.Credit);

            decimal AmountDiscounts = data.Where(item =>
                (itemsIdResponse.Data.itemType == 1 && item.DocTypeId == 4 && item.BudgTypeId == 1) ||
                (itemsIdResponse.Data.itemType == 2 && item.DocTypeId == 4 && item.BudgTypeId == 2))
                .Sum(item => item.Debit);

            decimal AmountTransfersFrom = data.Where(item => item.DocTypeId == 2).Sum(item => item.Debit);
            decimal AmountTransfersTo = data.Where(item => item.DocTypeId == 2).Sum(item => item.Credit);
            decimal AmountReinforcements = data.Where(item => item.DocTypeId == 3).Sum(item => item.Credit);

            decimal LinksFinal = data.Where(item =>
                item.DocTypeId == 5 && (item.StatusId == 2 || item.WFStatusId == 5) &&
                !canceledRefs.Contains(item.MReferenceNo))
                .Sum(item => item.Debit);

            decimal InitialLinks = data.Where(item =>
                item.DocTypeId == 8 && (item.StatusId == 2 || item.WFStatusId == 5) &&
                !canceledRefs.Contains(item.TId))
                .Sum(item => item.Debit);

            decimal AmountLinks = LinksFinal;
            decimal AmountTotal = (AmountInitial + AmountTransfersTo + AmountReinforcements) -
                                  (AmountDiscounts + AmountTransfersFrom + AmountLinks);

            var result = new
            {
                initialLinks = InitialLinks.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                accGroupId = itemsIdResponse.Data.AccGroupId ?? 0,
                amountInitial = AmountInitial.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersFrom = AmountTransfersFrom.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTransfersTo = AmountTransfersTo.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountReinforcements = AmountReinforcements.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountLinks = AmountLinks.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountDiscounts = AmountDiscounts.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                amountTotal = AmountTotal.ToString("N2", new System.Globalization.CultureInfo("en-US")),
                additionalValue = itemsIdResponse.Data.AccAccountName ?? "",
                note = itemsIdResponse.Data.Note ?? ""
            };

            return Json(result);
        }

        public async Task<IActionResult> LinkById(string code)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                decimal Amount = 0;

                var getAcc = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(x => x.FacilityId == session.FacilityId && x.Finyear == session.FinYear && x.IsDeleted == false && x.DocTypeId == 5 && x.MCode == code);
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    var NatureAccount = await accServiceManager.AccGroupService.GetOne(x => x.NatureAccount, s => s.AccGroupId == getAcc.Data.Single().AccGroupId && s.FacilityId == session.FacilityId);
                    if (NatureAccount != null)
                    {
                        if (NatureAccount.Data == 1)
                        {
                            Amount += getAcc.Data.Single().Credit;


                        }
                        else if (NatureAccount.Data == -1)

                        {
                            Amount += getAcc.Data.Single().Debit;

                        }
                    }
                    return Ok(Amount);
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> LinkPopUp(BudgTransactionDetailesVw filter)
        {
            if (filter != null)
            {

                var list = new List<BudgTransactionDetailesVw>();
                var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(x => x.FacilityId == session.FacilityId && x.Finyear == session.FinYear && x.IsDeleted == false && x.DocTypeId == 5);
                //var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW();
                if (items.Succeeded && items.Data != null)
                {
                    list.AddRange(items.Data);


                    if (!string.IsNullOrEmpty(filter.MCode))
                    {
                        list = list.Where(e => e.MCode != null && e.MCode.StartsWith(filter.MCode)).ToList();
                    }
                    foreach (var item in list)
                    {
                        var NatureAccount = await accServiceManager.AccGroupService.GetOne(x => x.NatureAccount, s => s.AccGroupId == item.AccGroupId && s.FacilityId == session.FacilityId);
                        if (NatureAccount != null)
                        {
                            if (NatureAccount.Data == 1)
                            {
                                item.Debit += item.Credit;


                            }
                            else if (NatureAccount.Data == -1)

                            {
                                item.Debit += item.Debit;

                            }
                        }

                    }
                    return PartialView("PopUps/Links/_LinksPopUp", list);
                }
            }
            else
            {
                var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(a => a.IsDeleted == false);
                if (items.Succeeded && items.Data != null)
                {
                    return PartialView("PopUps/Links/_LinksPopUp", items.Data.Where(a => a.IsDeleted == false));
                }
            }

            return PartialView("PopUps/Links/_LinksPopUp");

        }
        public async Task<decimal> GetExchangeRateValue(long? ID)
        {
            try
            {
                var dataTemp = await mainServiceManager.SysExchangeRateService.GetAll(x => x.CurrencyFromID == ID);
                if (dataTemp.Succeeded)
                {
                    var data = dataTemp.Data.OrderBy(S => S.ExchangeDate).FirstOrDefault();
                    if (data != null)
                    {
                        return (data.ExchangeRate ?? 0);
                    }
                }

                return 0;

            }
            catch (Exception)
            {
                return 0;
            }
        }


        public async Task<JsonResult> GetExpensesAmount(string Code)
        {
            try
            {
                long lag = session.Language;
                decimal? Amount = 0;
                decimal ExpensesAmount = 0;
                decimal AmountInitial = 0;

                decimal AmountTotal = 0;
                string ItemCode = "";
                string ItemName = "";
                var itemsResponse = await gbServiceManager.BudgTransactionDetaileService.GetOneVW(s => s.MCode == Code && s.IsDeleted == false && s.Finyear == session.FinYear && s.DocTypeId == 5 && s.FacilityId == session.FacilityId);
                if (itemsResponse.Succeeded)
                {
                    var result1 = await gbServiceManager.BudgExpensesLinksService.GetAll(x => x.LinkID == itemsResponse.Data.TId && x.IsDeleted == false);

                    if (result1.Succeeded)
                    {
                        ExpensesAmount = result1.Data.Sum(x => x.Amount) ?? 0;
                    }
                    var Item = await gbServiceManager.BudgTransactionDetaileService.GetOneVW(x => x.TId == itemsResponse.Data.TId && x.IsDeleted == false);
                    if (Item.Succeeded)
                    {
                        ItemCode = Item.Data.AccAccountCode;
                        if (lag == 1)
                        {
                            ItemName = Item.Data.AccAccountName;

                        }
                        else
                        {
                            ItemName = Item.Data.AccAccountName2;

                        }
                    }
                    var itemsLinkInitial = await gbServiceManager.BudgTransactionDetaileService.GetOneVW(s => s.TId == itemsResponse.Data.MReferenceNo && s.IsDeleted == false && s.Finyear == session.FinYear && s.DocTypeId == 8 && s.FacilityId == session.FacilityId);
                    if (itemsLinkInitial.Succeeded)

                    {
                        AmountInitial = itemsLinkInitial.Data.Debit;
                    }
                    Amount = itemsResponse.Data.Debit;

                }
                AmountTotal = Amount - ExpensesAmount ?? 0;
                var result = new { amountTotal = AmountTotal.ToString("N2", new System.Globalization.CultureInfo("en-US")), expensesAmount = ExpensesAmount.ToString("N2", new System.Globalization.CultureInfo("en-US")), itemCode = ItemCode, itemName = ItemName, amountInitial = AmountInitial.ToString("N2", new System.Globalization.CultureInfo("en-US")) };
                return Json(result);

            }
            catch (Exception)
            {
                var result = new { amountTotal = 0, expensesAmount = 0, itemCode = "", itemName = "" };

                return Json(result);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetSubitemsNameById2(string code, long? accGroupId)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required");
            }

            try
            {
                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();

                var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
                    a.AccAccountCode == code &&
                    a.Isdel == false &&
                    a.SystemId == 65 && allowedIds.Contains(a.AccAccountId)
                );

                if (getAcc.Succeeded && getAcc.Data?.Any() == true)
                {
                    var accData = getAcc.Data.FirstOrDefault();
                    return Ok(session.GetData<int>("language") == 1 ? accData.AccAccountName : accData.AccAccountName2);
                }

                return NotFound("No matching account found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubitemsPopUp2(AccAccountDto filter)
        {
            if (filter != null)
            {

                var list = new List<AccAccountsSubHelpeVw>();
                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();


                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false
                && a.SystemId == 65
                 && allowedIds.Contains(a.AccAccountId)

                );
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.Isdel == false && a.SystemId == 65));
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName != null && e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.AccAccountCode))
                {
                    list = list.Where(e => e.AccAccountCode != null && e.AccAccountCode.StartsWith(filter.AccAccountCode)).ToList();
                }

                if (filter.AccGroupId != null && filter.AccGroupId != 0)
                {
                    list = list.Where(e => e.AccGroupId != null && e.AccGroupId.Equals(filter.AccGroupId)).ToList();
                }

                return PartialView("PopUps/Accounts/_SubitemsPopUp2", list);
            }
            else
            {


                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false && a.SystemId == 65);
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Accounts/_SubitemsPopUp2", accounts.Data.Where(a => a.Isdel == false));
                }
            }

            return PartialView("PopUps/Accounts/_SubitemsPopUp2");

        }


        public async Task<IActionResult> GetSubitemsNameByIdSearch(string code)
        {
            try
            {
                //await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }

                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();

                var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.AccAccountCode == code
                && a.Isdel == false
                && a.SystemId == 65
                        && allowedIds.Contains(a.AccAccountId)
                );
                if (getAcc.Succeeded && getAcc.Data != null)
                {
                    if (getAcc.Data.Any())
                    {
                        if (session.GetData<int>("language") == 1)
                        {
                            return Ok(getAcc.Data.Single().AccAccountName);
                        }
                        else
                        {
                            return Ok(getAcc.Data.Single().AccAccountName2);
                        }

                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSubitemsPopUpSearch(AccAccountDto filter)
        {
            await GetDDl();

            if (filter != null)
            {

                var list = new List<AccAccountsSubHelpeVw>();

                var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
                var allowedIds = permissionsOverAccAccountID
           .Split(',', StringSplitOptions.RemoveEmptyEntries)
           .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
           .ToList();

                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false
                && a.SystemId == 65
                        && allowedIds.Contains(a.AccAccountId)
                );
                if (accounts.Succeeded && accounts.Data != null)
                {
                    list.AddRange(accounts.Data.Where(a => a.Isdel == false && a.SystemId == 65));
                }

                if (!string.IsNullOrEmpty(filter.AccAccountName))
                {
                    list = list.Where(e => e.AccAccountName != null && e.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())).ToList();
                }

                if (!string.IsNullOrEmpty(filter.AccAccountCode))
                {
                    list = list.Where(e => e.AccAccountCode != null && e.AccAccountCode.StartsWith(filter.AccAccountCode)).ToList();
                }
                if (filter.AccGroupId != null && filter.AccGroupId != 0)
                {
                    list = list.Where(e => e.AccGroupId != null && e.AccGroupId.Equals(filter.AccGroupId)).ToList();
                }
                return PartialView("PopUps/Accounts/_SubitemsPopUpSearch", list);
            }
            else
            {


                var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a => a.Isdel == false && a.SystemId == 65);
                if (accounts.Succeeded && accounts.Data != null)
                {
                    return PartialView("PopUps/Accounts/_SubitemsPopUpSearch", accounts.Data.Where(a => a.Isdel == false));
                }
            }

            return PartialView("PopUps/Accounts/_SubitemsPopUpSearch");

        }
        public async Task<IActionResult> LinksInitialById(string code)
        {
            try
            {
                await Task.Delay(TimeSpan.FromMilliseconds(50));
                if (string.IsNullOrEmpty(code))
                {
                    return Ok("");
                }
                decimal Amount = 0;
                var getAcc = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(x => x.FacilityId == session.FacilityId && x.Finyear == session.FinYear && x.IsDeleted == false && x.DocTypeId == 8 && x.MCode == code);
                if (getAcc.Succeeded && getAcc.Data != null)
                {

                    if (getAcc.Data.Any())
                    {
                        var NatureAccount = await accServiceManager.AccGroupService.GetOne(x => x.NatureAccount, s => s.AccGroupId == getAcc.Data.Single().AccGroupId && s.FacilityId == session.FacilityId);
                        if (NatureAccount != null)
                        {
                            if (NatureAccount.Data == 1)
                            {
                                Amount += getAcc.Data.Single().Credit;


                            }
                            else if (NatureAccount.Data == -1)

                            {
                                Amount += getAcc.Data.Single().Debit;

                            }
                        }
                        return Ok(Amount);


                    }
                }

                return Ok("");
            }
            catch (Exception)
            {
                return Ok("");
            }
        }
        [HttpGet]
        public async Task<IActionResult> LinksInitialPopUp(BudgTransactionDetailesVw filter)
        {
            if (filter != null)
            {

                var CanceledItems = await gbServiceManager.BudgTransactionsService.GetAllVW(a => a.DocTypeId == 9 && a.FlagDelete == false);
                if (CanceledItems.Succeeded)
                {
                    var list = new List<BudgTransactionDetailesVw>();
                    var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(x => x.FacilityId == session.FacilityId &&
                    !CanceledItems.Data.Select(x => x.ReferenceNo).Contains(x.TId) 
                    //&& x.WFStatusId == 5
                    && x.Finyear == session.FinYear && x.IsDeleted == false && x.DocTypeId == 8);

                    //var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW();
                    if (items.Succeeded && items.Data != null)
                    {
                        list.AddRange(items.Data);


                        if (!string.IsNullOrEmpty(filter.MCode))
                        {
                            list = list.Where(e => e.MCode != null && e.MCode.StartsWith(filter.MCode)).ToList();
                        }
                        foreach (var item in list)
                        {
                            var NatureAccount = await accServiceManager.AccGroupService.GetOne(x => x.NatureAccount, s => s.AccGroupId == item.AccGroupId && s.FacilityId == session.FacilityId);
                            if (NatureAccount != null)
                            {
                                if (NatureAccount.Data == 1)
                                {
                                    item.Debit += item.Credit;


                                }
                                else if (NatureAccount.Data == -1)

                                {
                                    item.Debit += item.Debit;

                                }
                            }

                        }

                        return PartialView("PopUps/Links/_LinksInitialPopUp", list);
                    }
                }
            }
            else
            {
                var items = await gbServiceManager.BudgTransactionDetaileService.GetAllVW(a => a.IsDeleted == false);
                if (items.Succeeded && items.Data != null)
                {
                    return PartialView("PopUps/Links/_LinksInitialPopUp", items.Data);
                }
            }

            return PartialView("PopUps/Links/_LinksInitialPopUp");

        }
        #region ================================= Transfers

        [HttpGet]
        public async Task<IActionResult> GetSubitemsTransfersFromPopUp(SubitemsDto filter)
        {
            await GetDDl();

            var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
            var allowedIds = permissionsOverAccAccountID
       .Split(',', StringSplitOptions.RemoveEmptyEntries)
       .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
       .ToList();

            // استعلام الفلترة مباشرة داخل قاعدة البيانات
            var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
                a.Isdel == false &&
                a.SystemId == 65 && allowedIds.Contains(a.AccAccountId) &&
                (filter == null ||
                 (filter.itemType == 0 || a.itemType == filter.itemType) &&
                 (string.IsNullOrEmpty(filter.AccAccountName) || a.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())) &&
                 (string.IsNullOrEmpty(filter.AccAccountCode) || a.AccAccountCode.StartsWith(filter.AccAccountCode)) &&
                 (!filter.AccGroupId.HasValue || filter.AccGroupId == 0 || a.AccGroupId == filter.AccGroupId)
                )
            );

            return PartialView("PopUps/Transfers/_SubitemsTransfersFromPopUp", accounts.Succeeded ? accounts.Data : new List<AccAccountsSubHelpeVw>());
        }
        [HttpGet]
        public async Task<IActionResult> GetSubitemsTransfersFromNameById(string code, long? accGroupId, int? itemType)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required");
            }

            var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
            var allowedIds = permissionsOverAccAccountID
       .Split(',', StringSplitOptions.RemoveEmptyEntries)
       .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
       .ToList();

            var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
                a.AccAccountCode == code &&
                a.Isdel == false &&
                a.SystemId == 65
               && allowedIds.Contains(a.AccAccountId) &&
                (itemType == null || itemType == 0 || a.itemType == itemType)
            );

            var accData = getAcc.Data?.FirstOrDefault();
            if (accData == null)
            {
                return NotFound("No matching account found");
            }

            bool isEnglish = session.GetData<int>("language") == 1;
            return Ok(isEnglish ? accData.AccAccountName : accData.AccAccountName2);
        }


        [HttpGet]
        public async Task<IActionResult> GetSubitemsTransfersToPopUp(SubitemsDto filter)
        {
            var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
            var allowedIds = permissionsOverAccAccountID
       .Split(',', StringSplitOptions.RemoveEmptyEntries)
       .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
       .ToList();

            // استعلام الفلترة مباشرة داخل قاعدة البيانات
            var accounts = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
                a.Isdel == false &&
                a.SystemId == 65 && allowedIds.Contains(a.AccAccountId) &&
                (filter == null ||
                 (filter.itemType == 0 || a.itemType == filter.itemType) &&
                 (string.IsNullOrEmpty(filter.AccAccountName) || a.AccAccountName.ToLower().Contains(filter.AccAccountName.ToLower())) &&
                 (string.IsNullOrEmpty(filter.AccAccountCode) || a.AccAccountCode.StartsWith(filter.AccAccountCode)) &&
                 (!filter.AccGroupId.HasValue || filter.AccGroupId == 0 || a.AccGroupId == filter.AccGroupId)
                )
            );

            return PartialView("PopUps/Transfers/_SubitemsTransfersTOPopUp", accounts.Succeeded ? accounts.Data : new List<AccAccountsSubHelpeVw>());

        }
        [HttpGet]
        public async Task<IActionResult> GetSubitemsTransfersToNameById(string code, long? accGroupId, int? itemType)
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Code is required");
            }

            var permissionsOverAccAccountID = session.PermissionsOverAccAccountID ?? "";
            var allowedIds = permissionsOverAccAccountID
       .Split(',', StringSplitOptions.RemoveEmptyEntries)
       .Select(id => long.TryParse(id, out var parsed) ? parsed : 0L)
       .ToList();

            var getAcc = await accServiceManager.AccAccountsSubHelpeVwService.GetAll(a =>
                a.AccAccountCode == code &&
                a.Isdel == false &&
                a.SystemId == 65 && allowedIds.Contains(a.AccAccountId) &&
                (itemType == null || itemType == 0 || a.itemType == itemType)
            );

            var accData = getAcc.Data?.FirstOrDefault();
            if (accData == null)
            {
                return NotFound("No matching account found");
            }

            bool isEnglish = session.GetData<int>("language") == 1;
            return Ok(isEnglish ? accData.AccAccountName : accData.AccAccountName2);
        }


        #endregion ================================= Transfers

        #endregion "GB"
    }
}
