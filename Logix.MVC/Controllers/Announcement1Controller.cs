using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.DataTableModel;
using Logix.MVC.Extentions;
using Logix.MVC.Helpers;
using Logix.MVC.Services.ExportService;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class Announcement1Controller : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IExportService _exportService;

        public Announcement1Controller(IMainServiceManager mainServiceManager, IExportService exportService)
        {
            this.mainServiceManager = mainServiceManager;
            this._exportService = exportService;
        }


        [HttpGet]
        public async Task<IActionResult> GetNameById(long id)
        {
            if (id == 0)
            {
                return Ok("Id is not correct");
            }
            var annRes = await mainServiceManager.SysScreenService.GetById(id);
            if (annRes.Succeeded && annRes.Data != null)
            {
                return Ok(annRes.Data.ScreenName);
            }
            else
            {
                return Ok(annRes.Status.message);
            }
            return Ok($"You are good {id}");
        }

        public async Task<IActionResult> Search(SysAnnouncementDto filter, string format = ExportFormat.Excel)
        {
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            var systems = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (systems.Succeeded)
            {
                Console.WriteLine($"========== Returned Items Before Where: {systems.Data.Count()} ====================");
                var res = systems.Data.Where(s => s.IsDeleted == false).OrderBy(s => s.Id).AsQueryable();
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
                // end date
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


                switch (format)
                {
                    case ExportFormat.Excel:
                        return File(
                            await _exportService.ExportToExcel<SysAnnouncementVw>(res.ToList()),
                            "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                            "data.xlsx");

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

                return View("Index", model);
            }
            TempData["error"] = systems.Status.message;
            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetPopUpResult(SysAnnouncementDto filter)
        {
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            var screens = await mainServiceManager.SysScreenService.GetAll();
            if (screens.Succeeded && screens.Data != null)
            {
                return View("_popUpResult", screens.Data);
            }
            return View("_popUpResult");
            /*var systems = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (systems.Succeeded)
            {
                Console.WriteLine($"========== Returned Items Before Where: {systems.Data.Count()} ====================");
                var res = systems.Data.Where(s => s.IsDeleted == false).OrderBy(s => s.Id).AsQueryable();
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
                // end date
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
            TempData["error"] = systems.Status.message;
            return View("Index", model);*/
        }

        public async Task<IActionResult> Index(SysAnnouncementDto filter)
        {
            var model = new SearchVM<SysAnnouncementDto, SysAnnouncementVw>(filter, new List<SysAnnouncementVw>());
            var items = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (items.Succeeded)
            {
                Console.WriteLine($"========== Returned Items Before Where: {items.Data.Count()} ====================");
                var res = items.Data.Where(s => s.IsDeleted == false).OrderBy(s => s.Id).AsQueryable();
                if (filter == null)
                {
                    model.ListModel = res.ToList();
                    return View(nameof(Index), model.ListModel);
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
                // end date
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
                return View("Index", model.ListModel);
            }
            TempData["error"] = items.Status.message;
            return View("Index", model.ListModel);
        }



        [HttpPost]
        public async Task<IActionResult> GetData(DataTableRequestModel request)
        {
            await Task.Delay(1000);
            var systems = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (systems.Succeeded)
            {
                var query = systems.Data.Where(s => s.IsDeleted == false && s.IsActive == true).OrderBy(s => s.Id).AsQueryable();

                query = ApplyMyColumnFilters(query, request.columns, request.search.value);

                // Get total record count before pagination
                var totalRecords = query.Count();

                // Apply ordering
                var orderColumn = request.columns[request.order[0].column].data;
                var orderDirection = request.order[0].dir;
                query = query.OrderByProperty(orderColumn, orderDirection);

                // Apply pagination
                var filteredRecords = query.Skip(request.start).Take(request.length).ToList();

                // Prepare the response
                var response = new DataTableResponseModel<SysAnnouncementVw>
                {
                    draw = request.draw,
                    recordsTotal = totalRecords,
                    recordsFiltered = totalRecords,
                    data = filteredRecords
                };

                return Json(response);
            }
            else
            {
                return BadRequest(systems.Status);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var systems = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (systems.Succeeded)
            {
                var res = systems.Data.Where(s => s.IsDeleted == false && s.IsActive == true).OrderBy(s => s.Id);
                return Json(res);
            }
            return BadRequest(systems.Status);
        }

        [HttpGet]
        public async Task<IActionResult> GetCount()
        {
            await Task.Delay(500);
            var systems = await mainServiceManager.SysAnnouncementService.GetAllVW();
            if (systems.Succeeded)
            {
                var res = systems.Data.Where(s => s.IsDeleted == false && s.IsActive == true).OrderBy(s => s.Id);
                return Json(res.Count());
            }
            return BadRequest(systems.Status);
        }

        private IQueryable<SysAnnouncementVw> ApplyMyColumnFilters(IQueryable<SysAnnouncementVw> data, List<ColumnModel> columns, string globalSearchValue)
        {
            if (columns != null && columns.Any(c => !string.IsNullOrEmpty(c.search.value)))
            {
                // Apply individual column filters
                foreach (var column in columns)
                {
                    var columnName = column.data;
                    var searchValue = column.search.value;

                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        if (column.searchable)
                        {
                            switch (columnName.ToLower())
                            {
                                case "typeName":
                                    { data = data.Where(r => r.TypeName != null && r.Id.ToString().ToUpper().Contains(searchValue.ToUpper())); }
                                    break;

                                case "subject":
                                    { data = data.Where(r => r.Subject != null && r.Subject.ToUpper().Contains(searchValue.ToUpper())); }
                                    break;

                                case "braName":
                                    { data = data.Where(r => r.BraName != null && r.BraName.ToUpper().Contains(searchValue.ToUpper())); }
                                    break;
                                    /* case "Date":
                                         { data = data.Where(r => r.CreatedDate != null && r.CreatedDate.ToString().ToUpper().Contains(searchValue.ToUpper())); }
                                         break;*/

                            }
                            // Apply column-specific filter
                            //data = data.Where(d => GetColumnValue(d, columnName).Contains(searchValue));
                        }
                    }
                }
            }
            /*else if (!string.IsNullOrEmpty(globalSearchValue))
            {
                // Apply global search filter to all searchable columns
                data = data.Where(d => columns.Any(c => c.searchable && GetColumnValue(d, c.data).Contains(globalSearchValue)));
            }*/

            return data;
        }





        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SysAnnouncementDto obj)
        {
            if (!ModelState.IsValid)
            {
                return View(obj);
            }
            try
            {
                //obj.UsersId = "1";
                //obj.GroupsId = "0";
                //obj.FacilityId = 1;
                var addRes = await mainServiceManager.SysAnnouncementService.Add(obj);
                if (addRes.Succeeded)
                {
                    TempData["msg"] = "تمت الاضافة بنجاح";
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
                TempData["error"] = ex.Message;
                return View(obj);
            }
        }
    }
}
