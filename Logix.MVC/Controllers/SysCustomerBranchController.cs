using Logix.Application.Interfaces.IServices;
using Logix.Application.Services;
using Logix.MVC.Helpers;
using Logix.MVC.Services.ExportService;
using Microsoft.AspNetCore.Mvc;

namespace Logix.MVC.Controllers
{
    public class SysCustomerBranchController : Controller

    {
        private readonly IMainServiceManager mainServiceManager;
        private readonly IExportService _exportService;
        private readonly IPermissionHelper permission;
        private readonly IWebHostEnvironment env;
        private readonly IFilesHelper filesHelper;
        private readonly IDDListHelper listHelper;
        private readonly ISession _session;
        public SysCustomerBranchController(
            IMainServiceManager mainServiceManager,
            IExportService exportService,
            IPermissionHelper permission,
             IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor,
            IFilesHelper filesHelper,
            IDDListHelper listHelper
            )
        {
            this.mainServiceManager = mainServiceManager;
            this._exportService = exportService;
            this.permission = permission;
            this.env = env;
            this.filesHelper = filesHelper;
            this.listHelper = listHelper;
            _session = httpContextAccessor.HttpContext.Session;
        }
        [NonAction]
        public async Task<DDLViewModel> GetDDl()
        {
            var ddlvm = new DDLViewModel();

            var Cites = await mainServiceManager.SysCitesService.GetAll() ;
            var DrpCitesId = listHelper.GetFromList<long>(Cites.Data.Select(s => new DDListItem<long> { Name = s.CityName, Value = s.CityID }), hasDefault: false);
            ddlvm.AddList(nameof(DrpCitesId), DrpCitesId);

            ViewData["DDL"] = ddlvm;

            return ddlvm;
        }
        
        public IActionResult Index()
        {
            
            return View();
        }
        public IActionResult Add()
        {

            return View();
        }
        //public async Task<IActionResult> Add()
        //{
        //     await GetDDl();
        //    return View();
        //}
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
    }
}
