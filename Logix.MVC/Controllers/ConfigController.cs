using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Domain.Main;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class ConfigController : Controller
    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IPermissionHelper permission;
        private readonly IDDListHelper listHelper;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly ISession _session;

        public ConfigController(
            IMainServiceManager mainServiceManager,
            IPermissionHelper permission,
            IHttpContextAccessor httpContextAccessor,
            IDDListHelper listHelper,
            ISysConfigurationHelper configurationHelper)
        {
            this.mainServiceManager = mainServiceManager;
            this.permission = permission;
            this.listHelper = listHelper;
            this.configurationHelper = configurationHelper;
            _session = httpContextAccessor.HttpContext.Session;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDl()
        {
            var ddlvm = new DDLViewModel();

            var allsys = await mainServiceManager.SysSystemService.GetAll();
            var sys = allsys.Data.Where(d => d.Isdel == false);
            var ddlSys = listHelper.GetFromList<long>(sys.Select(s => new DDListItem<long> { Name = s.ShortName, Value = s.SystemId }));
            ddlvm.AddList(nameof(ddlSys), ddlSys);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }

        [NonAction]
        public async Task<DDLViewModel> GetDDlWithDefault(SysPropertyDto model)
        {
            if (model == null)
            {
                return await GetDDl();
            }

            var ddlvm = new DDLViewModel();
            
            var allsys = await mainServiceManager.SysSystemService.GetAll();
            var sys = allsys.Data.Where(d=> d.Isdel == false);
            var ddlSys = listHelper.GetFromList<int>(sys.Select(s => new DDListItem<int> { Name = s.ShortName, Value = s.SystemId }), selectedValue: model.SystemId??0);
            ddlvm.AddList(nameof(ddlSys), ddlSys);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
        public async Task<IActionResult> Index(SysPropertyDto filter)
        {
            
            var chk = await permission.HasPermission(821, PermissionType.Show);
            if (!chk)
            {
                return View("AccessDenied");
            }

            var model = new SearchVM<SysPropertyDto, SysPropertiesVw>(filter, new List<SysPropertiesVw>());
            await GetDDlWithDefault(filter);
            var get = await mainServiceManager.SysPropertyService.GetAllVW();
            if (get.Succeeded)
            {
                var query = get.Data. OrderBy(s=>s.Id).AsQueryable();
                if(filter == null)
                {
                    model.ListModel = query.ToList();
                    return View(model);
                }

                if (!string.IsNullOrEmpty(filter.PropertyName))
                {
                    query = query.Where(f=>f.PropertyName.Contains(filter.PropertyName));
                }
                
                if(filter.SystemId != null && filter.SystemId != 0)
                {
                    query = query.Where(f=>f.SystemId == filter.SystemId);
                }

                model.ListModel = query.ToList();
                return View(model);
            }
            TempData["error"] = get.Status.message;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SysPropertyValueDto obj)
        {
            await Task.Delay(100);
            Console.WriteLine($"---- new value: {obj.PropertyValue??""} ---------");
            return Ok(obj.PropertyValue??"");
        }
    }
}
