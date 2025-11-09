using Logix.Application.Common;
using Logix.Application.DTOs.ACC;
using Logix.Application.DTOs.Main;
using Logix.Application.DTOs.WA;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Logix.MVC.Controllers
{
    public class InvestBranchController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IAccServiceManager accServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISessionHelper session;
        private readonly IWaServiceManager waServiceManager;
        private readonly ILocalizationService localization;

        public InvestBranchController(IMainServiceManager mainServiceManager,
            IAccServiceManager accServiceManager,
            IPermissionHelper permission,
            IDDListHelper listHelper,
            ISessionHelper session,
            IWaServiceManager waServiceManager,
            ILocalizationService localization)
        {
            this.mainServiceManager = mainServiceManager;
            this.accServiceManager = accServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.session = session;
            this.waServiceManager = waServiceManager;
            this.localization = localization;
        }

        // GET: InvestBranchController
        [HttpGet]
        public async Task<ActionResult> Index(InvestBranchDto filter)
        {
            var model = new SearchVM<InvestBranchDto, InvestBranchVw>(filter, new List<InvestBranchVw>());
            var chk = await permission.HasPermission(1, PermissionType.Show);
            if (!chk)
                return View("AccessDenied");

            var ddl = await GetDDlWithDefault(filter, true, "all");
            //model.SearchModel.FacilityId = session.FacilityId;
            var items = await mainServiceManager.InvestBranchService.GetAllVW();
            if (items.Succeeded)
            {
                var res = items.Data.Where(b => b.Isdel == false).OrderBy(s => s.BranchId).AsQueryable();
                if (filter == null)
                {
                    model.ListModel = res.ToList();
                    return View(nameof(Index), model);
                }

                if (filter.FacilityId != null && filter.FacilityId > 0)
                {
                    res = res.Where(r => r.FacilityId.Equals(filter.FacilityId));
                }

                if (!string.IsNullOrEmpty(filter.BraName))
                {
                    res = res.Where(r => r.BraName.Contains(filter.BraName));
                }

                model.ListModel = res.ToList();
                if (!model.ListModel.Any())
                    TempData.AddSuccessMessage($"{localization.GetResource1("NosearchResult")}");
                return View("Index", model);
            }
            TempData.AddErrorMessage($"{items.Status.message}");
            return View("Index", model);
        }

        // GET: InvestBranchController/Create
        public async Task<ActionResult> Add()
        {
            var chk = await permission.HasPermission(1, PermissionType.Add);
            if (!chk)
            {
                return View("AccessDenied");
            }

            setErrors();
            await GetDDl(true, "Tselect");

            var obj = new InvestBranchDto();
            obj.FacilityId = session.FacilityId;
            return View(obj);
        }

        // POST: InvestBranchController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Add(InvestBranchDto obj)
        {
            setErrors();
            await GetDDl(true, "Tselect");
            if (!ModelState.IsValid)
            {
                return View(obj);
            }

            if (obj.Auto == false && string.IsNullOrEmpty(obj.BranchCode))
            {
                TempData.AddErrorMessage($"{localization.GetMainResource("BranchCode")}");
                return View(obj);
            }
            try
            {
                if (!string.IsNullOrEmpty(obj.CostCenterCode))
                {
                    var getCostCenter = await accServiceManager.AccCostCenterService.GetAll(a => a.CostCenterCode == obj.CostCenterCode && a.IsDeleted == false);
                    if (getCostCenter.Succeeded && getCostCenter.Data.Any())
                    {
                        obj.CcId = (int)getCostCenter.Data.Single().CcId;
                    }
                    else
                    {
                        TempData.AddErrorMessage($"{localization.GetResource1("CostCenterNotExsists")}");
                        return View(obj);
                    }
                }
                


                var add = await mainServiceManager.InvestBranchService.Add(obj);
                if (add.Succeeded)
                {
                    var waResult = await waServiceManager.WhatsappBusinessService.SendWhatsappMessage(

                   new WhatsappBusinessDataSendDto
                   {

                       DocumentUrl = "https://www.pdf995.com/samples/pdf.pdf",
                       RecipientPhoneNumber = "967775699645",
                       DataMessage = add.Data,
                       HasDocument = true,
                      //WaTemplateMessageValue = add.Data,
                   }, 1);

                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Add");
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

        // GET: InvestBranchController/Edit/5
        public async Task<ActionResult> Edit(string encId)
        {

            var chk = await permission.HasPermission(1, PermissionType.Edit);
            if (!chk)
            {
                return View("AccessDenied");
            }

            setErrors();

            if (string.IsNullOrEmpty(encId))
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInUpdate")}");
                return RedirectToAction(nameof(Index));
            }

            try
            {
                int Id = EncryptionHelper.Decrypt<int>(encId);
                await GetDDl(false);
                var obj = new InvestBranchDto();
                var getBranch = await mainServiceManager.InvestBranchService.GetForUpdate<InvestBranchDto>(Id);
                if (getBranch.Succeeded)
                {
                    obj = getBranch.Data;
                    if (obj.CcId != 0)
                    {
                        //get costCenterCode by costCenterId to display it in view
                        var getCostCenter = await accServiceManager.AccCostCenterService.GetAll(a => a.CcId == obj.CcId);
                        if (getCostCenter.Succeeded && getCostCenter.Data != null)
                        {
                            obj.CostCenterCode = getCostCenter.Data.Single().CostCenterCode;
                            obj.CostCenterName = getCostCenter.Data.Single().CostCenterName;
                        }
                    }

                    return View(obj);
                }
                TempData.AddErrorMessage($"{getBranch.Status.message}");
                return View(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(nameof(Index));
            }
        }

        // POST: InvestBranchController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(InvestBranchDto obj)
        {
            setErrors();
            await GetDDl(false);
            if (!ModelState.IsValid)
                return View(obj);

            try
            {
                if (!string.IsNullOrEmpty(obj.CostCenterCode))
                {
                    var getCostCenter = await accServiceManager.AccCostCenterService.GetAll(a => a.CostCenterCode == obj.CostCenterCode);
                    if (getCostCenter.Succeeded && getCostCenter.Data != null)
                    {
                        obj.CcId = (int)getCostCenter.Data.Single().CcId;
                    }
                }

                var update = await mainServiceManager.InvestBranchService.Update(obj);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("Edit", routeValues: new { encId = EncryptionHelper.Encrypt(obj.BranchId) });
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

        // GET: InvestBranchController/Delete/5
        public async Task<ActionResult> Delete(int Id)
        {
            if (Id == 0)
            {
                TempData.AddErrorMessage($"{localization.GetMessagesResource("NoIdInDelete")}");
                return RedirectToAction(nameof(Index));
            }
                

            try
            {
                var chk = await permission.HasPermission(1, PermissionType.Delete);
                if (!chk)
                    return View("AccessDenied");

               // int id = EncryptionHelper.Decrypt<int>(Id);
                var delete = await mainServiceManager.InvestBranchService.Remove(Id);
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

        [HttpGet]
        public async Task<ActionResult> BranchAccount(string encId)
        {
            if (string.IsNullOrEmpty(encId))
                return RedirectToAction(nameof(Index));

            try
            {
                
                var chk = await permission.HasPermission(1, PermissionType.Delete);
                if (!chk)
                    return RedirectToAction("AccessDenied");

                var branchId = EncryptionHelper.Decrypt<int>(encId);
                await GetDDl(false, "",branchId);
                //get branch data
                var branch = await mainServiceManager.InvestBranchService.GetById(branchId);
                if (!branch.Succeeded)
                {
                    TempData.AddErrorMessage($"{branch.Status.message}");
                    return RedirectToAction(nameof(Index));
                }
                
                //get all Acc_Branch_Account_Type
                var AccBranchAccTypes = await accServiceManager.AccBranchAccountTypeService.GetAll(t => t.IsDeleted == false);

                //get list from Acc_Branch_Account_Vw where Br_Acc_Type_ID = id of Acc_Branch_Account_Type
                var AllDataFromView = await accServiceManager.AccBranchAccountService.GetAllVW();
                List<AccBranchAccountsVwsDto> BranchAccountList = new List<AccBranchAccountsVwsDto>();
                foreach (var item in AccBranchAccTypes.Data)
                {
                    var singleItem = AllDataFromView.Data.Where(s => s.BranchId == branchId && s.BrAccTypeId == item.Id).ToList();
                    if (singleItem.Count() == 0)
                    {
                        //that means this branch has no account with type of item.Id
                        //we save some initial data, to show all types of account but with empty AccAccountCode 
                        AccBranchAccountsVwsDto record = new AccBranchAccountsVwsDto();
                        record.BranchId = branchId;
                        record.BrAccTypeId = item.Id;
                        record.Name = item.Name;
                        record.Name2 = item.Name2;
                        singleItem.Add(record);
                    }
                    BranchAccountList.AddRange(singleItem);
                }
                //return view with this list of AccVW
                return View(BranchAccountList);
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<ActionResult> BranchAccount(List<AccBranchAccountsVwsDto> list)
        {
            string encId = EncryptionHelper.Encrypt(list.First().BranchId);
            try
            {
                await GetDDl(false, "", (int) list.First().BranchId);
                var update = await accServiceManager.AccBranchAccountService.Update(list);
                if (update.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction("BranchAccount", routeValues: new { encId });
                }
                else
                {
                    TempData.AddErrorMessage($"{update.Status.message}");
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(list);
            }
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault, string text = "", int branchId=0)
        {
            var ddlvm = new DDLViewModel();

            var facilities = await accServiceManager.AccFacilityService.GetAll(f => f.IsDeleted == false);
            var ddFacilitiesList = listHelper.GetFromList<long>(facilities.Data.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.FacilityName:s.FacilityName2, Value = s.FacilityId }), hasDefault: hasDefault, defaultText: text);
            ddlvm.AddList(nameof(ddFacilitiesList), ddFacilitiesList);

            var branches = await mainServiceManager.InvestBranchService.GetAll(b => b.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<long>(branches.Data.Select(b => new DDListItem<long> { Name =(session.Language==1) ? b.BraName:b.BraName2, Value = b.BranchId }), hasDefault: hasDefault, defaultText: text, selectedValue: branchId);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(InvestBranchDto model, bool hasDefault, string text = "")
        {
            if (model == null)
            {
                return await GetDDl(hasDefault, text);
            }

            var ddlvm = new DDLViewModel();

            var allFacilityList = await accServiceManager.AccFacilityService.GetAll();
            var facilities = allFacilityList.Data.Where(f => f.IsDeleted == false);
            var ddFacilitiesList = listHelper.GetFromList<long>(facilities.Select(s => new DDListItem<long> { Name = (session.Language == 1) ? s.FacilityName : s.FacilityName2, Value = s.FacilityId }), selectedValue: (int)(model.FacilityId ?? 0));
            ddlvm.AddList(nameof(ddFacilitiesList), ddFacilitiesList);

            var branches = await mainServiceManager.InvestBranchService.GetAll(b => b.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<long>(branches.Data.Select(b => new DDListItem<long> { Name = (session.Language == 1) ? b.BraName : b.BraName2, Value = b.BranchId }), selectedValue: model.BranchId);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
    }
}
