using Logix.Application.Common;
using Logix.Application.DTOs.HR;
using Logix.Application.DTOs.Main;
using Logix.Application.Extensions;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.DataTableModel;
using Logix.MVC.Extentions;
using Logix.MVC.Filters;
using Logix.MVC.Helpers;
using Logix.MVC.Services.ExportService;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
namespace Logix.MVC.Controllers
{
    public class MyModel
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public string Property3 { get; set; }
        public string Property4 { get; set; }
    }

    public class AnnouncementController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IExportService _exportService;
        private readonly IPermissionHelper permission;
        private readonly IWebHostEnvironment env;
        private readonly IFilesHelper filesHelper;
        private readonly IDDListHelper listHelper;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly IScreenPropertiesHelper screenPropertiesHelper;
        private readonly ISessionHelper _session;
        public AnnouncementController(
            IMainServiceManager mainServiceManager,
            IExportService exportService,
            IPermissionHelper permission,
             IWebHostEnvironment env,
            ISessionHelper session,
            IFilesHelper filesHelper,
            IDDListHelper listHelper,
            ISysConfigurationHelper configurationHelper,
            IScreenPropertiesHelper screenPropertiesHelper
            )
        {
            this.mainServiceManager = mainServiceManager;
            this._exportService = exportService;
            this.permission = permission;
            this.env = env;
            this.filesHelper = filesHelper;
            this.listHelper = listHelper;
            this.configurationHelper = configurationHelper;
            this.screenPropertiesHelper = screenPropertiesHelper;
            _session = session;
        }

        [NonAction]
        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDl(bool hasDefault= true, string defaultText="all")
        {
            var ddlvm = new DDLViewModel();


            var ddTypeList = await listHelper.GetList(313, hasDefault: hasDefault, defaultText: defaultText); // types
            ddlvm.AddList(nameof(ddTypeList), ddTypeList);


            var locations = await mainServiceManager.SysAnnouncementService.GetSysAnnouncementLocationVw();
            var ddAnnouncementLocationList = listHelper.GetFromList<long>(locations.Data.Select(s => new DDListItem<long> { Name = s.LocationName, Value = s.Id.Value }), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddAnnouncementLocationList), ddAnnouncementLocationList);


            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data?.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1) ?? new List<SysDepartmentDto>(); 
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }),hasDefault:hasDefault, defaultText:defaultText);
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);


            var deptLocations = allDepts.Data?.Where(d => d.TypeId == 2 && d.IsDeleted == false && d.StatusId == 1) ?? new List<SysDepartmentDto>();
            var ddDeptLocationsList = listHelper.GetFromList<long>(deptLocations.OrderBy(d => d.Name).Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddDeptLocationsList), ddDeptLocationsList);


            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();   
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = s.BraName, Value = s.BranchId }), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysAnnouncementDto model, bool hasDefault = true, string defaultText = "all")
        {
            if(model == null)
            {
                return await GetDDl();
            }

            var ddlvm = new DDLViewModel();


            var ddTypeList = await listHelper.GetList(313, selectedValue: model.Type??0, hasDefault: hasDefault, defaultText: defaultText); // types
            ddlvm.AddList(nameof(ddTypeList), ddTypeList);


            var locations = await mainServiceManager.SysAnnouncementService.GetSysAnnouncementLocationVw();
            var ddAnnouncementLocationList = listHelper.GetFromList<long>(locations.Data.Select(s => new DDListItem<long> { Name = s.LocationName, Value = s.Id.Value }), selectedValue: (long)(model.LocationId??0), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddAnnouncementLocationList), ddAnnouncementLocationList);


            var allDepts = await mainServiceManager.SysDepartmentService.GetAll();
            var depts = allDepts.Data.Where(d => d.TypeId == 1 && d.IsDeleted == false && d.StatusId == 1);
            var ddDeptsList = listHelper.GetFromList<long>(depts.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), selectedValue: (long)(model.DeptId ?? 0), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddDeptsList), ddDeptsList);


            var deptLocations = allDepts.Data.Where(d => d.TypeId == 2 && d.IsDeleted == false && d.StatusId == 1).OrderBy(d => d.Name);
            var ddDeptLocationsList = listHelper.GetFromList<long>(deptLocations.Select(s => new DDListItem<long> { Name = s.Name, Value = s.Id }), selectedValue: (long)(model.DeptLocationId ?? 0), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddDeptLocationsList), ddDeptLocationsList);


            var allBranches = await mainServiceManager.SysSystemService.GetSysBranchVw();
            var branches = allBranches.Data.Where(d => d.Isdel == false);
            var ddBranchesList = listHelper.GetFromList<int>(branches.Select(s => new DDListItem<int> { Name = s.BraName, Value = s.BranchId }), selectedValue: (int)(model.BranchId ?? 0), hasDefault: hasDefault, defaultText: defaultText);
            ddlvm.AddList(nameof(ddBranchesList), ddBranchesList);
            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [HttpGet]
        public async Task<IActionResult> Index(SysAnnouncementDto filter)
        {
            var testScrProp1 = await screenPropertiesHelper.GetById(19);
            var testScrProp2 = await screenPropertiesHelper.IsAllowed(19);
            var testScrProp3 = await screenPropertiesHelper.GetValue(19);           
            var chk = await permission.HasPermission(632, PermissionType.Show);
            if (!chk)
            {
                return View("AccessDenied");
            }
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            var ddl = await GetDDlWithDefault(filter);
            var items = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (items.Succeeded)
            {
                Console.WriteLine($"========== Returned Items Before Where: {items.Data.Count()} ====================");
                var res = items.Data.Where(s => s.IsDeleted == false).OrderBy(s => s.Id).AsQueryable();
                if (filter == null)
                {
                    model.ListModel = res.ToList();
                    return View(nameof(Index), model);
                }
                if (filter.IsActive != null)
                {
                    res = res.Where(s => s.IsActive == filter.IsActive);
                }
                if (!string.IsNullOrEmpty(filter.Subject))
                {
                    res = res.Where(s => s.Subject.Contains(filter.Subject));
                }
                if (filter.Type != null && filter.Type > 0)
                {
                    res = res.Where(s => s.Type.Equals(filter.Type));
                }
                if (filter.LocationId != null && filter.LocationId > 0)
                {
                    res = res.Where(s => s.LocationId.Equals(filter.LocationId));
                }
                if (filter.Type != null && filter.Type > 0)
                {
                    res = res.Where(s => s.Type.Equals(filter.Type));
                }
                // start date
                if (filter.StartDate != null)
                {
                    DateTime startDate = DateTime.ParseExact(filter.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    res = res.Where(s => DateTime.ParseExact(s.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) >= startDate);
                }
                // end date
                if (filter.EndDate != null )
                {
                    DateTime endDate = DateTime.ParseExact(filter.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                    res = res.Where(s => DateTime.ParseExact(s.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) <= endDate);
                }
                
                if (filter.BranchId != null && filter.BranchId > 0)
                {
                    res = res.Where(s => s.BranchId.Equals(filter.BranchId));
                }
                if (filter.DeptLocationId != null && filter.DeptLocationId > 0)
                {
                    res = res.Where(s => s.DeptLocationId.Equals(filter.DeptLocationId));
                }
                if (filter.DeptId != null && filter.DeptId > 0)
                {
                    res = res.Where(s => s.DeptId.Equals(filter.DeptId));
                }

                model.ListModel = res.ToList();
                return View("Index", model);
            }
            TempData["error"] = items.Status.message;
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> TestGrid()
        {            
            return View("TestGrid");
        }
        [HttpPost]
        public async Task<IActionResult> IndexFilter(SysAnnouncementDto filter)
        {
            var chk = await permission.HasPermission(632, PermissionType.Show);
            if (!chk)
            {
                return View("AccessDenied");
            }
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            await GetDDlWithDefault(filter);
            try
            {
                var res = await Filter(filter);
                if (res != null && res.Count() > 0)
                {
                    model.ListModel = res.ToList();
                    return View("Index", model);
                }
                return View("Index", model);
            }
            catch (Exception exp)
            {
                TempData["error"] = $"EXP: {exp.Message}";
                return View("Index", model);
            }
        }

        public async Task<IActionResult> Add()
        {
            setErrors();
            var chk = await permission.HasPermission(632, PermissionType.Add);
            if (!chk)
            {              
                return View("AccessDenied");
            }

            await GetDDl(hasDefault: true, defaultText: "choose");
            //var obj = new SysAnnouncementDto { Credit = 52.79m };
            return View();
        }


        [HttpPost]
        //[ModifyDecimalInputs]
        public async Task<IActionResult> Add(SysAnnouncementDto obj, IFormFile? file)
        {
            //string valueAsString = "57.32";
            ////valueAsString = valueAsString?.Replace("٫", ".");
            ////obj.Credit = 32٫45m;
            //if (decimal.TryParse(valueAsString, NumberStyles.Number, CultureInfo.InvariantCulture, out var originalValue))
            //{
            //    // Format the value based on the current culture
            //    var formattedValue = originalValue.ToString("N", CultureInfo.CurrentCulture);
            //    var tst = obj.Credit;// decimal.Parse(formattedValue, CultureInfo.CurrentCulture);
            //    var t1 = tst;
            //}
            //Console.WriteLine($"===== Credit in announcement: {obj.Credit} ================");
            setErrors();
            
            if (!ModelState.IsValid)
            {
                Console.WriteLine("in else of isValid --------------");
                ModelState.AsEnumerable().ToList().ForEach(
                    x => Console.WriteLine("KEY: {0} , VALUE: {1}", x.Key, ModelState.GetValidationState(x.Key).ToString()));
                await GetDDl(hasDefault: true, defaultText: "choose");
                return View(obj);
            }
            try
            {
                var user = _session.GetData<Domain.Main.SysUser>("user");
                if (user == null)
                {
                    TempData["error"] = "error, current user has a problem";

                    await GetDDl(hasDefault: true, defaultText: "choose");
                    return View(obj);
                }
                //obj.UsersId = $"{_session.UserId}";
                //obj.GroupsId = $"{_session.GroupId}";
                //obj.FacilityId = _session.FacilityId;
                if(file != null && file.Length > 0)
                {
                    var addFile = await filesHelper.SaveFile(file, "AllFiles");
                    if (!string.IsNullOrEmpty(addFile))
                    {
                        obj.AttachFile = addFile;
                    }
                }
                var addRes = await mainServiceManager.SysAnnouncementService.Add(obj);
                if (addRes.Succeeded)
                {
                    // var files = await filesHelper.SaveFiles(addRes.Data.Id, 14);
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    TempData.AddErrorMessage($"{addRes.Status.message}");
                    await GetDDl(hasDefault: true, defaultText: "choose");
                    return View(obj);
                }
            }

            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                await GetDDl(hasDefault: true, defaultText: "choose");
                return View(obj);
            }
        }


        public async Task<IActionResult> Edit(string encId)
        {
            setErrors();
            var chk = await permission.HasPermission(632, PermissionType.Edit);
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
                var getItem = await mainServiceManager.SysAnnouncementService.GetForUpdate<SysAnnouncementEditDto>(mainId);
                if (getItem.Succeeded && getItem.Data != null)
                {
                    await GetDDl(hasDefault:true, defaultText: "choose");
                    return View(getItem.Data);
                }
                TempData.AddErrorMessage($"NoIdInUpdate");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
               //var msg= ExceptionsHelper.GetCustomErrorMessage(exp);
                TempData.AddErrorMessage($"{exp.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SysAnnouncementEditDto obj, IFormFile? file)
        {
            setErrors();
            if (!ModelState.IsValid)
            {
                await GetDDl(hasDefault: true, defaultText: "choose");
                return View(obj);
            }
            try
            {
                var user = _session.GetData<Domain.Main.SysUser>("user");
                if (user == null)
                {
                    TempData["error"] = "error, current user has a problem";
                    await GetDDl(hasDefault: true, defaultText: "choose");
                    return View(obj);
                }

                if (file != null && file.Length > 0)
                {
                    if (!string.IsNullOrEmpty(obj.AttachFile))
                    {
                        if (System.IO.File.Exists(Path.Combine(env.WebRootPath, obj.AttachFile)))
                        {
                            System.IO.File.Delete(Path.Combine(env.WebRootPath, obj.AttachFile));
                        }
                    }
                    var addFile = await filesHelper.SaveFile(file, "AllFiles");
                    if (!string.IsNullOrEmpty(addFile))
                    {
                        obj.AttachFile = addFile;
                    }
                }

                var addRes = await mainServiceManager.SysAnnouncementService.Update(obj);
                if (addRes.Succeeded)
                {
                    TempData.AddSuccessMessage("success");
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    TempData.AddErrorMessage($"{addRes.Status.message}");
                    await GetDDl(hasDefault: true, defaultText: "choose");
                    return View(obj);
                }
            }

            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");

                await GetDDl(hasDefault: true, defaultText: "choose");
                return View(obj);
            }
        }

        public async Task<IActionResult> Delete(long Id)
        {
            var chk = await permission.HasPermission(632, PermissionType.Delete);
            if (!chk)
            {
                return View("AccessDenied");
            }
            if (Id == 0)
            {
                TempData["error"] = "Please choose an entity to delete it, there is no id passed";
                return RedirectToAction(nameof(Index));
            }

            try
            {
                var del = await mainServiceManager.SysAnnouncementService.Remove(Id);
                if (del.Succeeded)
                {

                    TempData["msg"] = "Item deleted successfully";
                    return RedirectToAction(nameof(Index));
                }
                TempData["error"] = $"{del.Status.message}";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception exp)
            {
                TempData.AddErrorMessage($"{exp.Message}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(SysAnnouncementEditDto obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            try
            {
                var addRes = await mainServiceManager.SysAnnouncementService.Update(obj);
                if (addRes.Succeeded)
                {
                    TempData["msg"] = "تمت العملية بنجاح";
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    TempData["error"] = addRes.Status.message;
                    return View(obj);
                }
            }

            catch (Exception ex)
            {
                TempData.AddErrorMessage($"{ex.Message}");
                return View(obj);
            }
        }

        [HttpPost]
        public async Task<IActionResult> ExportExcel(SysAnnouncementDto filter, string format = ExportFormat.Excel)
        {
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            try
            {
                var res = await Filter(filter);
                if (res != null && res.Count() > 0)
                {
                    switch (format)
                    {
                        case ExportFormat.Excel:
                            return File(
                                await _exportService.ExportToExcel<SysAnnouncementVw>(res.ToList()),
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                                $"announcements-{DateTime.Now.ToString("yyyyMMdd", CultureInfo.InvariantCulture)}.xlsx");

                        case ExportFormat.Csv:
                            return File(await _exportService.ExportToCsv<SysAnnouncementVw>(res.ToList()),
                                "application/csv",
                                "data.csv");

                        case ExportFormat.Html:
                            return File(await _exportService.ExportToHtml<SysAnnouncementVw>(res.ToList()),
                                "application/csv",
                                "data.html");

                        case ExportFormat.Json:
                            return File(await _exportService.ExportToJson<SysAnnouncementVw>(res.ToList()),
                                "application/json",
                                "data.json");

                        case ExportFormat.Xml:
                            return File(await _exportService.ExportToXml<SysAnnouncementVw>(res.ToList()),
                                "application/xml",
                                "data.xml");

                        case ExportFormat.Yaml:
                            return File(await _exportService.ExportToYaml<SysAnnouncementVw>(res.ToList()),
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
                TempData["error"] = $"EXP: {exp.Message}";
                return View("Index", model);
            }
        }

        [NonAction]
        public async Task<IQueryable<SysAnnouncementVw>?> Filter(SysAnnouncementDto filter)
        {
            try
            {
                var items = await mainServiceManager.SysAnnouncementService.GetAllVW(a => a.IsDeleted == false);
                if (items.Succeeded)
                {
                    var res = items.Data.AsQueryable();
                    if (filter == null)
                    {
                        return res;
                    }
                    if (filter.IsActive != null)
                    {
                        res = res.Where(s => s.IsActive == filter.IsActive);
                    }
                    if (!string.IsNullOrEmpty(filter.Subject))
                    {
                        res = res.Where(s => s.Subject.Contains(filter.Subject));
                    }
                    if (filter.Type != null && filter.Type > 0)
                    {
                        res = res.Where(s => s.Type.Equals(filter.Type));
                    }
                    if (filter.LocationId != null && filter.LocationId > 0)
                    {
                        res = res.Where(s => s.LocationId.Equals(filter.LocationId));
                    }
                    if (filter.Type != null && filter.Type > 0)
                    {
                        res = res.Where(s => s.Type.Equals(filter.Type));
                    }
                    // start date
                    if (filter.StartDate != null)
                    {
                        DateTime startDate = DateTime.ParseExact(filter.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                        res = res.Where(s => DateTime.ParseExact(s.StartDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) >= startDate);
                    }
                    // end date
                    if (filter.EndDate != null)
                    {
                        DateTime endDate = DateTime.ParseExact(filter.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture);
                        res = res.Where(s => DateTime.ParseExact(s.EndDate, "yyyy/MM/dd", CultureInfo.InvariantCulture) <= endDate);
                    }

                    if (filter.BranchId != null && filter.BranchId > 0)
                    {
                        res = res.Where(s => s.BranchId.Equals(filter.BranchId));
                    }
                    if (filter.DeptLocationId != null && filter.DeptLocationId > 0)
                    {
                        res = res.Where(s => s.DeptLocationId.Equals(filter.DeptLocationId));
                    }
                    if (filter.DeptId != null && filter.DeptId > 0)
                    {
                        res = res.Where(s => s.DeptId.Equals(filter.DeptId));
                    }

                    return res;
                }

                TempData["error"] = items.Status.message;
                return null;
            }
            catch (Exception ex)
            {
                TempData["error"] = $"EXP: {ex.Message}";
                return null;
            }
        }
    }
}
