using Logix.Application.Common;
using Logix.Application.DTOs.GB;
using Logix.Application.DTOs.Main;
using Logix.Application.Extensions;
using Logix.Application.Interfaces.IServices;
using Logix.Application.Services;
using Logix.Domain.Main;
using Logix.MVC.Extentions;
using Logix.MVC.Helpers;
using Logix.MVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Logix.MVC.Controllers
{
    public class TestFilesController : Controller
    {

        private readonly ISessionHelper _session;
        private readonly IWebHostEnvironment env;
        private readonly ILocalizationService localization;
        private readonly IDDListHelper listHelper;

        public TestFilesController(
            ISessionHelper session, IWebHostEnvironment env, ILocalizationService localization, IDDListHelper listHelper)
        {

            this._session = session;
            this.env = env;
            this.localization = localization;
            this.listHelper = listHelper;
        }

        private void setErrors()
        {
            var errors = new ErrorsHelper(ModelState);
            ViewData["ERRORS"] = errors;
        }
        public async Task GetDDl()
        {
            //int lang = session.GetData<int>("language");
            var ddlvm = new DDLViewModel();
            var DDLFielType = await listHelper.GetList(344, defaultText: localization.GetResource1("Choose")); // types
            ddlvm.AddList(nameof(DDLFielType), DDLFielType);
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Add()
        {
            return View();
        }

        public async Task<IActionResult> GetFilesUpload()
        {
            await Task.Delay(TimeSpan.FromSeconds(3));
            return PartialView("_AddFiles");
        }

        /*   public IActionResult TestAdd()
           {
               //var files = new List<AttFile> { new AttFile(), new AttFile()};
               return View();
           }*/


        #region "transactions_Files_Add"
        [HttpPost]
        public async Task<IActionResult> TestAdd(SysFileDto attFile, IFormFile file)
        {
            setErrors();
            try
            {
                if (attFile != null && file != null)
                {

                    string tempName = "";
                    string ext = "";
                    if (file != null && file.Length > 0)
                    {
                        if (attFile.TableId == null || attFile.TableId == 0)
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noTableIdInAddFiles");
                            return PartialView("_AddFiles", model: attFile.TableId ?? 0);
                        }
                        if (string.IsNullOrEmpty(attFile.FileName))
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noAttachNameInAddFiles");
                            return PartialView("_AddFiles", model: attFile.TableId ?? 0);
                        }
                        string tempFolder = Path.Join(env.WebRootPath, FilesPath.TempPath);
                        if (!Directory.Exists(tempFolder))
                        {
                            Directory.CreateDirectory(tempFolder);
                        }
                        string tempFileName = Path.Join(tempFolder, DateTime.Now.ToString("ddmmyyhhmmssfff", CultureInfo.InvariantCulture));
                        ext = Path.GetExtension(file.FileName);
                        tempName = $"{tempFileName}{ext}";
                        using (var stream = new FileStream(tempName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    var files = _session.GetData<List<SysFileDto>>(SessionKeys.AddTempFiles);
                    if (files == null)
                    {
                        files = new List<SysFileDto>();
                    }
                    attFile.IncreasId = files.Count + 1;
                    attFile.FileUrl = tempName;
                    attFile.FileExt = ext;
                    attFile.CreatedOn = DateTime.Now;
                    attFile.CreatedBy = _session.UserId;
                    attFile.IsDeleted = false;
                    attFile.FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    attFile.FacilityId = Convert.ToInt32(_session.FacilityId);
                    files.Add(attFile);
                    _session.AddData<List<SysFileDto>>(SessionKeys.AddTempFiles, files);

                    return PartialView("_AddFiles", model: attFile.TableId ?? 0);
                }

                TempData["filesError"] = localization.GetMessagesResource("fileNullError");
                return PartialView("_AddFiles", model: attFile?.TableId ?? 0);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("_AddFiles", model: attFile?.TableId ?? 0);
            }


        }

        [HttpPost]
        public async Task<IActionResult> AddCustomerFile(SysCustomerFileDto attFile, IFormFile file)
        {
            await GetDDl();
            setErrors();
            try
            {
                if (attFile != null && file != null)
                {

                    string tempName = "";
                    string ext = "";
                    string fileDate = "";
                    if (file != null && file.Length > 0)
                    {

                        if (string.IsNullOrEmpty(attFile.FileName))
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noAttachNameInAddFiles");
                            return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
                        }

                        if (string.IsNullOrEmpty(attFile.FileDate))
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noDateInAddFiles");
                            return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
                        }

                        //var date = DateTime.TryParseExact(attFie.FileDate, "yyyy/MM/dd", null, System.Globalization.DateTimeStyles.None, out DateTime dateValue);
                        if (attFile.FileDate.Length != 10)
                        {
                            TempData["filesError"] = localization.GetMessagesResource("formatDateErrorInAddFiles");
                            return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
                        }

                        string tempFolder = Path.Join(env.WebRootPath, FilesPath.TempPath);
                        if (!Directory.Exists(tempFolder))
                        {
                            Directory.CreateDirectory(tempFolder);
                        }
                        string tempFileName = Path.Join(tempFolder, DateTime.Now.ToString("ddmmyyhhmmssfff", CultureInfo.InvariantCulture));
                        ext = Path.GetExtension(file.FileName);
                        tempName = $"{tempFileName}.{ext}";
                        using (var stream = new FileStream(tempName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    var files = _session.GetData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles);
                    if (files == null)
                    {
                        files = new List<SysCustomerFileDto>();
                    }
                    attFile.IncreasId = files.Count + 1;
                    attFile.FileUrl = tempName;
                    attFile.FileExt = ext;
                    attFile.IsDeleted = false;
                    attFile.FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    files.Add(attFile);
                    _session.AddData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles, files);

                    return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
                }

                TempData["filesError"] = localization.GetMessagesResource("fileNullError");
                return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
            }


        }

        [HttpPost]
        public IActionResult Add(AttFile attFile, IFormFile file)
        {
            try
            {
                if (attFile != null && file != null)
                {

                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }


        [HttpPost]
        public IActionResult FilesDelete(int Id, int tableId)
        {
            try
            {
                if (Id != 0)
                {
                    var Files = _session.GetData<List<SysFileDto>>(SessionKeys.AddTempFiles);
                    if (Files != null)
                    {
                        var itemToRemove = Files.FirstOrDefault(x => x.IncreasId == Id);
                        if (itemToRemove != null)
                        {
                            Files.Remove(itemToRemove);
                            UpdateIncreasId(Files);
                            _session.AddData<List<SysFileDto>>(SessionKeys.AddTempFiles, Files);
                        }
                    }
                }
                return PartialView("_AddFiles", model: tableId);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("_AddFiles", model: tableId);
            }

        }

        [HttpPost]
        public IActionResult CustomerFilesDelete(int Id)
        {
            setErrors();
            try
            {
                if (Id != 0)
                {
                    var Files = _session.GetData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles);
                    if (Files != null)
                    {
                        var itemToRemove = Files.FirstOrDefault(x => x.IncreasId == Id);
                        if (itemToRemove != null)
                        {
                            Files.Remove(itemToRemove);
                            // UpdateIncreasId(Files);
                            _session.AddData<List<SysCustomerFileDto>>(SessionKeys.AddTempCustomerFiles, Files);
                        }
                    }
                }
                return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("PartialFiles/_AddCustomerFiles", model: 1);
            }

        }


        public void UpdateIncreasId(List<SysFileDto> Files)
        {
            if (Files != null)
            {
                int No = 1;
                foreach (var files in Files)
                {
                    files.IncreasId = No;
                    No += No;
                }
            }
        }
        #endregion "transactions_Files_Add"

        #region "transactions_Files_Edit"
        [HttpPost]
        public IActionResult FilesDeleteInEdit(int Id, int tableId)
        {
            setErrors();
            try
            {
                if (Id != 0)
                {
                    var Filess = _session.GetData<List<SysFileDto>>(SessionKeys.EditTempFiles);
                    if (Filess != null)
                    {
                        var itemToRemove = Filess.FirstOrDefault(x => x.Id == Id);
                        if (itemToRemove != null)
                        {
                            if (itemToRemove.Id == 0)
                            {
                                Filess.Remove(itemToRemove);
                                UpdateIncreasId(Filess);
                            }
                            else
                            {
                                foreach (var item in Filess)
                                {
                                    if (item.Id == Id)
                                    {
                                        item.IsDeleted = true;
                                        item.ModifiedBy = (int)_session.UserId;
                                        item.ModifiedOn = DateTime.Now;
                                    }
                                }
                            }

                            _session.AddData<List<SysFileDto>>(SessionKeys.EditTempFiles, Filess);

                        }
                    }
                }
                else
                {

                    TempData["filesError"] = localization.GetMessagesResource("NoItemFoundToDelete");

                }

                return PartialView("_EditFiles", model: tableId);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("_EditFiles", model: tableId);
            }

        }

        [HttpPost]

        public async Task<IActionResult> FilesAddInEdit(SysFileDto attFile, IFormFile file)
        {
            setErrors();
            try
            {
                if (attFile != null && file != null)
                {

                    string tempName = "";
                    string ext = "";
                    if (file != null && file.Length > 0)
                    {
                        if (attFile.TableId == null || attFile.TableId == 0)
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noTableIdInAddFiles");
                            return PartialView("_EditFiles", model: attFile.TableId ?? 0);
                        }
                        if (string.IsNullOrEmpty(attFile.FileName))
                        {
                            TempData["filesError"] = localization.GetMessagesResource("noAttachNameInAddFiles");
                            return PartialView("_EditFiles", model: attFile.TableId ?? 0);
                        }
                        string tempFolder = Path.Join(env.WebRootPath, FilesPath.TempPath);
                        if (!Directory.Exists(tempFolder))
                        {
                            Directory.CreateDirectory(tempFolder);
                        }
                        string tempFileName = Path.Join(tempFolder, DateTime.Now.ToString("ddmmyyhhmmssfff", CultureInfo.InvariantCulture));
                        ext = Path.GetExtension(file.FileName);
                        tempName = $"{tempFileName}.{ext}";
                        using (var stream = new FileStream(tempName, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                    }
                    var files = _session.GetData<List<SysFileDto>>(SessionKeys.EditTempFiles);
                    if (files == null)
                    {
                        files = new List<SysFileDto>();
                    }
                    attFile.FileUrl = tempName;
                    attFile.FileExt = ext;
                    attFile.CreatedOn = DateTime.Now;
                    attFile.CreatedBy = _session.UserId;
                    attFile.IsDeleted = false;
                    attFile.FileDate = DateTime.Now.ToString("yyyy/MM/dd", CultureInfo.InvariantCulture);
                    files.Add(attFile);
                    _session.AddData<List<SysFileDto>>(SessionKeys.EditTempFiles, files);

                    return PartialView("_EditFiles", model: attFile.TableId ?? 0);
                }

                TempData["filesError"] = localization.GetMessagesResource("fileNullError");
                return PartialView("_EditFiles", model: attFile?.TableId ?? 0);
            }
            catch (Exception ex)
            {
                TempData["filesError"] = localization.GetMessagesResource($"EXP: {ex.Message}");
                return PartialView("_EditFiles", model: attFile?.TableId ?? 0);
            }


        }




        #endregion "transactions_Files_Edit"

    }
}
