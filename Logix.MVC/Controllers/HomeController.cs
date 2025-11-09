using AutoMapper;
using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.DTOs.Main;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Interfaces.IServices.Main;
using Logix.Application.Services;
using Logix.Application.Services.Main;
using Logix.Domain.ACC;
using Logix.Domain.Main;
using Logix.Infrastructure.DbContexts;
using Logix.Infrastructure.Services;
using Logix.MVC.Helpers;
using Logix.MVC.Models;
using Logix.MVC.Resources;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using NuGet.Protocol.Plugins;
using OfficeOpenXml.Filter;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Logix.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMainServiceManager mainServiceManager;
        private readonly Infrastructure.DbContexts.ApplicationDbContext context;
        //private readonly IStringLocalizer<SharedResources> localizer;
        private readonly ILocalizationService localization;
        private readonly ISessionHelper session;
        private readonly IAccServiceManager accServiceManager;
        private readonly ISysConfigurationHelper configurationHelper;
        private readonly IMapper mapper;

        public HomeController(
            ILogger<HomeController> logger,
            IMainServiceManager mainServiceManager,
            Infrastructure.DbContexts.ApplicationDbContext context,
            IStringLocalizer<SharedResources> localizer,
            ILocalizationService localization,
            ISessionHelper session,
            IAccServiceManager accServiceManager
            , ISysConfigurationHelper configurationHelper
            , IMapper mapper
            )
        {
            _logger = logger;
            this.mainServiceManager = mainServiceManager;
            this.context = context;
            //this.localizer = localizer;
            this.localization = localization;
            this.session = session;
            this.accServiceManager = accServiceManager;
            this.configurationHelper = configurationHelper;
            this.mapper = mapper;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            //foreach(var cul in CultureInfo.)
            try
            {
                if (culture != null)
                {
                    Console.WriteLine($"00000000000000 {culture} 000000000000000000000000");
                    //var lang = CultureInfo.CurrentCulture.Name;
                    if (culture.StartsWith("ar"))
                    {
                        session.AddData<int>("language", 1);
                        session.AddData<int>("Language", 1);
                    }
                    else
                    {
                        session.AddData<int>("language", 2);
                        session.AddData<int>("Language", 2);
                    }
                    CultureInfo.CurrentCulture = new CultureInfo(culture);
                    CultureInfo.CurrentUICulture = new CultureInfo(culture);
                    Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                    new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
                }

                return LocalRedirect(returnUrl);
            }                     
            catch (Exception)
            {
                return LocalRedirect("/Home/Index");
            }
           // return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> ChangeFinYear(long finYear, string returnUrl)
        {
            try
            {
                if (finYear != 0)
                {
                    session.AddData<long>("FinYear", finYear);
                    long currPeriodId = 0;

                    var getPeriodId = await accServiceManager.AccPeriodsService.GetAll();
                    if (getPeriodId.Succeeded)
                    {

                        var Period = getPeriodId.Data.Where(x => x.IsDeleted == false && x.PeriodState == 1 && x.FinYear == finYear).FirstOrDefault();
                        if (Period != null)
                        {
                            currPeriodId = Period.PeriodId;

                            session.AddData<long>("Period", currPeriodId);
                        }
                    }

                }

                return LocalRedirect(returnUrl);
            }
            catch (Exception)
            {
                return LocalRedirect("/Home/Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> ChangefacilityUser(long userId, string returnUrl)
        {
            try
            {

                //userId = 0;
                if (userId != 0)
                {
                    var result = await mainServiceManager.SysUserService.GetOne(x => x.Enable == 1 && x.Id == userId && x.Isdel == false);

                    if (result.Succeeded && result.Data != null)
                    {
                        var currentUser = result.Data;
                        var obj = mapper.Map<SysUser>(currentUser);

                        session.AddData<Domain.Main.SysUser>("user", obj);

                        var secret = await configurationHelper.GetValue(265, 1);
                        var oldBaseUrl = await configurationHelper.GetValue(266, 1);
                        var coreBaseUrl = await configurationHelper.GetValue(267, 1);
                        session.AddData<string>("OldBaseUrl", oldBaseUrl);
                        session.AddData<string>("CoreBaseUrl", coreBaseUrl);
                        long currFacilityId = 0;
                        long currFinYear = 0;
                        long currPeriodId = 0;
                        if (currentUser.UserTypeId == 1)
                        {
                            var Facilitydata = await accServiceManager.AccFacilityService.GetById(currentUser.FacilityId ?? 0);

                            if (Facilitydata.Succeeded && Facilitydata.Data != null)
                            {
                                currFacilityId = Facilitydata.Data.FacilityId;
                            }

                            session.AddData<long>("finYear", currFinYear);
                            var getFinYears = await mainServiceManager.SysSystemService.GetFinancialYears(currentUser.FacilityId ?? 0);
                            if (getFinYears.Succeeded && getFinYears.Data.Any())
                            {
                                currFinYear = getFinYears.Data.Where(n => n.FacilityId == currentUser.FacilityId && n.IsDeleted == false).Last().FinYear;
                                session.AddData<long>("finYear", currFinYear);
                            }

                            //-----------------------------------------
                            var getLang = CultureInfo.CurrentCulture.Name;
                            var currLang = 1;
                            if (getLang.StartsWith("ar"))
                            {
                                currLang = 1;
                                session.AddData<int>("language", 1);
                            }
                            else
                            {
                                currLang = 2;
                                session.AddData<int>("language", 2);
                            }
                            ///--------------------------------------------------

                            var getPeriodId = await accServiceManager.AccPeriodsService.GetAll();
                            if (getPeriodId.Succeeded)
                            {
                                var Period = getPeriodId.Data.Where(x => x.IsDeleted == false && x.PeriodState == 1 && x.FinYear == currFinYear).FirstOrDefault();
                                if (Period != null)
                                {
                                    currPeriodId = Period.PeriodId;
                                    session.AddData<long>("Period", currPeriodId);
                                }
                            }
                            //-------------------------------------------------------------
                            int groupId = 0;

                            if (!string.IsNullOrEmpty(currentUser.GroupsId))
                            {
                                var hasGroup = int.TryParse(currentUser.GroupsId, out groupId);
                            }
                            //----------------------------------------------------------------------
                            /////////  to get Calendat Type
                            string CalendarType = "1";
                            var getCalendarType = await configurationHelper.GetValue(19, currFacilityId);
                            if (!string.IsNullOrEmpty(getCalendarType))
                            {
                                CalendarType = getCalendarType;
                            }


                            session.SetMainData(currentUser.Id, currentUser.EmpId ?? 0, groupId, currFacilityId, currFinYear, currLang, currentUser.BranchsId ?? "", CalendarType,currentUser.UserPkId??0, currentUser.PermissionsOverAccAccountID);

                       
                            return LocalRedirect(returnUrl);


                        }




                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, $"Error: {result.Status.message}");
                    }

                    return LocalRedirect(returnUrl);
                }
                else
                {

                    return RedirectToAction("Logout", "Account");


                }


            }

            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
                return LocalRedirect(returnUrl);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var systems = await mainServiceManager.SysSystemService.GetAll();
            if (systems.Succeeded)
            {

                var sysList = systems.Data.Where(s => s.Isdel == false && s.ShowInHome == true).OrderBy(s => s.SysSort).ToList();
                foreach (var sys in sysList)
                {
                    if (sys.IsCore)
                    {
                        var url = $"/{sys.Area}/{sys.Controller}/{sys.Action}";
                        sys.DefaultPage = url;
                    }
                    else
                    {
                        var url = $"{session.OldBaseUrl}{sys.Folder}{sys.DefaultPage}";
                        sys.DefaultPage = url;
                    }
                }

                return View(sysList);
            }
            TempData["error"] = systems.Status.message;
            return View();
        }

        

        public IActionResult Privacy()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            var res = asm.GetTypes()
                  .Where(type => typeof(Controller).IsAssignableFrom(type))//filter controllers
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic && !method.IsDefined(typeof(NonActionAttribute)));
            
            Console.WriteLine($"------------------- start listing -------------------");
            foreach(var type in res)
            {
                Console.WriteLine($"Controller: {type.Name}- {type.ReturnType.Name}");
                /*foreach (var type2 in type.GetMethods().Where(m => m.ReturnType == typeof(ViewResult)))
                {
                    Console.WriteLine($"Action: {type2.ToString}");
                }*/
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("TestError");
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}