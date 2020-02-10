using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using HCRGUniversityMgtApp.Domain.Models.UserModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VzaarApi;


namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EducationModuleController : BaseController
    {
        private readonly NEPService.EducationModuleService.IEducationModuleService _educationModuleService;
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.UserService.IUserService _userService;
        private readonly IStorage _storageService;
        public EducationModuleController(NEPService.EducationModuleService.IEducationModuleService educationModuleService, IStorage storageService, NEPService.EducationService.IEducationService educationService, NEPService.UserService.IUserService userService)
        {
            _educationModuleService = educationModuleService;
            _storageService = storageService;
            _educationService = educationService;
            _userService = userService;
        }
        public ActionResult ShowEditorModule()
        {
            Editor Editor2 = new Editor(System.Web.HttpContext.Current, "Editor2");
            Editor2.ClientFolder = "/richtexteditor/";
            Editor2.Width = Unit.Pixel(1146);

            Editor2.Height = Unit.Pixel(660);
            Editor2.ResizeMode = RTEResizeMode.Disabled;
            Editor2.FullScreen = false;

            Editor2.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor2.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor2.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor2.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor2.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor2.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor2.DisabledItems = "save, help";
            string content1 = Request.Form["Editor2"];
            Editor2.MvcInit();
            ViewData["Editor2"] = Editor2.MvcGetString();
            // Desc Editor
            Editor Editor4 = new Editor(System.Web.HttpContext.Current, "Editor4");
            Editor4.ClientFolder = "/richtexteditor/";
            Editor4.Width = Unit.Pixel(1146);
            Editor4.Height = Unit.Pixel(660);
            Editor4.ResizeMode = RTEResizeMode.Disabled;

            Editor4.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor4.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor4.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor4.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor4.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor4.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor4.FullScreen = false;
            Editor4.DisabledItems = "save, help";
            string content2 = Request.Form["Editor2"];
            Editor4.MvcInit();
            ViewData["Editor4"] = Editor4.MvcGetString();

            return PartialView("_EducationModulePartial");
        }
        public ActionResult ShowEditEditorModule()
        {
            Editor Editor3 = new Editor(System.Web.HttpContext.Current, "Editor3");
            Editor3.ClientFolder = "/richtexteditor/";
            Editor3.Width = Unit.Pixel(1118);
            Editor3.Height = Unit.Pixel(660);
            Editor3.ResizeMode = RTEResizeMode.Disabled;

            Editor3.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            Editor3.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            Editor3.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            Editor3.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor3.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor3.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor3.FullScreen = false;
            Editor3.DisabledItems = "save, help";
            string content1 = Request.Form["Editor3"];
            Editor3.MvcInit();
            ViewData["Editor3"] = Editor3.MvcGetString();
            return PartialView("_EducationModuleUploadedFilePartial");
        }
        public ActionResult Index()
        {
            EducationModuleDetailViewModel educationModuleDetailViewModel = new EducationModuleDetailViewModel();
            EducationModuleViewModel educationModuleModel = new EducationModuleViewModel();
            educationModuleModel.EducationModuleResults = Mapper.Map<IEnumerable<EducationModule>>(_educationModuleService.GetAllEducationModuleByEducationID(1));
            educationModuleDetailViewModel.educationModuleViewModel = educationModuleModel;
            return View(educationModuleDetailViewModel);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ShowModule(int Educationid, int skip, int? take)
        {
            EducationModule1ViewModel educationModule1ViewModel = new EducationModule1ViewModel();
            List<EducationModule1> list_educationModule = new List<EducationModule1>();
            if (take == null)
                educationModule1ViewModel.pagedEducationModule = Mapper.Map<PagedEducationModule>(_educationModuleService.GetAllPagedEducationModuleByEid(Educationid, skip, GlobalConst.Records.Take));
            else
                educationModule1ViewModel.pagedEducationModule = Mapper.Map<PagedEducationModule>(_educationModuleService.GetAllPagedEducationModuleByEid(Educationid, skip, take.Value));
            foreach (EducationModule educationmodule in educationModule1ViewModel.pagedEducationModule.EducationModules)
            {
                EducationModuleFile educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(educationmodule.EducationModuleID));
                educationModuleFile.ModuleFile = "blank";
                EducationModule1 educationModule1 = new EducationModule1();
                educationModule1.EducationID = educationmodule.EducationID;
                educationModule1.EducationModuleDate = educationmodule.EducationModuleDate;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                educationModule1.EducationModuleShortDesc = regex.Replace(HttpUtility.HtmlDecode(educationmodule.EducationModuleDescription), String.Empty);
                educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Replace("&nbsp;", "");
                if (educationModule1.EducationModuleShortDesc.Length > 1000)
                {
                    educationModule1.EducationModuleShortDesc = educationModule1.EducationModuleShortDesc.Substring(0, 1000);
                }
                educationModule1.EducationModuleDescription = educationmodule.EducationModuleDescription;
                educationModule1.EducationModuleID = educationmodule.EducationModuleID;
                educationModule1.EducationModuleName = educationmodule.EducationModuleName;
                educationModule1.EducationModulePosition = educationmodule.EducationModulePosition;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.TEXT)
                    educationModule1.EducationModulePDFName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.PPT)
                    educationModule1.EducationModulePPTName = educationModuleFile.ModuleFile;
                else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.Video)
                    educationModule1.EducationModuleVideoName = educationModuleFile.ModuleFile;

                educationModule1.EducationModuleTypeFile = educationModuleFile.FileTypeID;
                educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
                educationModule1.flag = educationmodule.flag;
                list_educationModule.Add(educationModule1);
            }
            educationModule1ViewModel.pagedEducationModule.PagedRecords = GlobalConst.Records.Take;
            educationModule1ViewModel.list_educationModule = list_educationModule;
            return Json(educationModule1ViewModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult Add(EducationModule educationmodule, string hdEducationModuleID, string moduleTime, List<HttpPostedFileBase> list_ModuleFile2)
        {
            EducationModule1 educationModule1 = new EducationModule1();
            List<HttpPostedFileBase> list_ModuleFile = new List<HttpPostedFileBase>();


            if (list_ModuleFile2.Count() > 0 && list_ModuleFile2[0] != null)
                list_ModuleFile = list_ModuleFile2;
            else if (educationmodule.EducationModuleVideo != null)
                list_ModuleFile.Add(educationmodule.EducationModuleVideo);
            else if (educationmodule.EducationModuleText != null)
                educationmodule.EducationModuleText = educationmodule.EducationModuleText;
            educationmodule.EducationModuleDate = System.DateTime.Now;

            if (educationmodule.EducationModuleVideo == null)
            {
                if (educationmodule.EducationModuleTime != null && educationmodule.EducationModuleTime != "")
                    educationmodule.EducationModuleTime = educationmodule.EducationModuleTime + ":01";
            }

            if (moduleTime != null && moduleTime != "")
                educationmodule.EducationModuleTime = moduleTime;

            if (hdEducationModuleID == "")
            {
                if (educationmodule.EducationModuleDescription == null)
                    educationmodule.EducationModuleDescription = "";
                var moduleID = _educationModuleService.AddEducationModule(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModule>(educationmodule));
                educationmodule.EducationModuleID = moduleID;
                educationmodule.flag = true;
                educationmodule = UploadModulefiles(educationmodule, list_ModuleFile, educationmodule.EducationModuleID, educationmodule.EducationModuleText);
            }
            else
            {
                if (educationmodule.EducationModuleDescription == null)
                    educationmodule.EducationModuleDescription = "";
                educationmodule.EducationModuleID = Convert.ToInt32(hdEducationModuleID);
                var moduleID = _educationModuleService.UpdateEducationModule(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModule>(educationmodule));
                educationmodule.flag = false;
            }
            educationModule1.EducationID = educationmodule.EducationID;
            educationModule1.EducationModuleDate = educationmodule.EducationModuleDate;
            educationModule1.EducationModuleDescription = educationmodule.EducationModuleDescription;

            educationModule1.EducationModuleID = educationmodule.EducationModuleID;
            educationModule1.EducationModuleName = educationmodule.EducationModuleName;
            educationModule1.EducationModuleTypeFile = educationmodule.EducationModuleTypeFile;
            educationModule1.EducationModulePosition = educationmodule.EducationModulePosition;
            educationModule1.EducationModuleTime = educationmodule.EducationModuleTime;
            educationModule1.EducationModulePDFName = educationmodule.EducationModulePDFName;
            educationModule1.EducationModulePPTName = educationmodule.EducationModulePPTName;
            educationModule1.EducationModuleVideoName = educationmodule.EducationModuleVideoName;
            educationModule1.flag = educationmodule.flag;
            Education edu = new Education();
            edu = Mapper.Map<Education>(_educationService.GetEducationByID(educationModule1.EducationID));
            educationModule1.ReadyToDisplay = edu.ReadyToDisplay;
            return Json(educationModule1, GlobalConst.Message.text_html);
        }
        [HttpGet]
        public ActionResult AddSubModule()
        {
            return View();
        }

        [HttpPost]
        public JsonResult DeleteModuleInfo(EducationModule educationModule)
        {
            var _res = _educationModuleService.GetEducationModuleFileByModuleID(educationModule.EducationModuleID);
            if (_res.FileTypeID == 2)
            {
                if (Directory.Exists(Server.MapPath(Path.Combine(GlobalConst.FolderName.Storage, GlobalConst.MgmtDirectoryStructure.ModuleImages, _res.ModuleFile))))
                    Directory.Delete(Server.MapPath(Path.Combine(GlobalConst.FolderName.Storage, GlobalConst.MgmtDirectoryStructure.ModuleImages, _res.ModuleFile)), true);
            }
            else if (_res.FileTypeID == 3)
            {
                if (System.IO.File.Exists(Server.MapPath(Path.Combine(GlobalConst.FolderName.Storage, GlobalConst.MgmtDirectoryStructure.ModuleVideo, _res.ModuleFile))))
                    System.IO.File.Delete(Server.MapPath(Path.Combine(GlobalConst.FolderName.Storage, GlobalConst.MgmtDirectoryStructure.ModuleVideo, _res.ModuleFile)));
            }
            _educationModuleService.DeleteEducationModule(educationModule.EducationModuleID);
            return Json("Education Module Deleted Successfully");
        }

        [HttpPost]
        public ActionResult UploadMultipleModuleFiles(HttpPostedFileBase moduleMultiplefiles, int hdEducationModuleID)
        {
            var path = "";
            string uid = Guid.NewGuid().ToString();
            EducationModuleFile educationModuleFile = new EducationModuleFile();
            try
            {
                path = _storageService.SetModuleMultipleUploadStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), moduleMultiplefiles.FileName);
                educationModuleFile.FileTypeID = GlobalConst.FileTypes.ModuleUploads;
                moduleMultiplefiles.SaveAs(path);
                educationModuleFile.ModuleFile = Path.GetFileName(path);
                educationModuleFile.DocumentUploadedDate = System.DateTime.Now;
                educationModuleFile.UserID = HCRGCLIENT.ClientID;
                educationModuleFile.DocumentName = Path.GetFileNameWithoutExtension(moduleMultiplefiles.FileName);
                educationModuleFile.EducationModuleID = hdEducationModuleID;
                var _educationModuleFileID = _educationModuleService.AddEducationModuleFile(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModuleFile>(educationModuleFile));
                return Json(hdEducationModuleID);
            }
            catch
            {
                return Json(GlobalConst.CommonValues.Zero);
            }
        }


        [HttpPost]
        public JsonResult DeleteEducationModuleFileByEducationModuleFileID(int EducationModuleFileID, int FileTypeID)
        {
            try
            {
                _educationModuleService.DeleteEducationModuleFileByEducationModuleFileID(EducationModuleFileID, FileTypeID);
                return Json(GlobalConst.CommonValues.One);
            }
            catch
            {
                return Json(GlobalConst.CommonValues.Zero);
            }
        }

        [HttpPost]
        public ActionResult GetEducationModuleFileByEducationModuleFileID(int EMID)
        {
            EducationModuleFileViewModel myEducationModuleDetailViewModel = new EducationModuleFileViewModel();
            try
            {
                myEducationModuleDetailViewModel.EducationModuleFileResults = Mapper.Map<IEnumerable<EducationModuleFile>>(_educationModuleService.GetMyEducationModulesDetailsByMEID_FileTypeID(EMID, GlobalConst.FileTypes.ModuleUploads));

                foreach (var _res in myEducationModuleDetailViewModel.EducationModuleFileResults)
                {
                    User _user = Mapper.Map<User>(_userService.GetUserByID(_res.UserID.Value));
                    _res.UserName = _user.FirstName + " " + _user.LastName;
                }

                return Json(myEducationModuleDetailViewModel);
            }
            catch
            {
                return Json(GlobalConst.CommonValues.Zero);
            }
        }

        private EducationModule UploadModulefiles(EducationModule educationModule, List<HttpPostedFileBase> list_ModuleFile, int moduleID, string Mtext)
        {
            //var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationModule.EducationModuleID);
            string uid = Guid.NewGuid().ToString();
            EducationModuleFile educationModuleFile = new EducationModuleFile();
            if (list_ModuleFile.Count() > 0 && list_ModuleFile[0] != null)
            {
                for (int i = 0; i < list_ModuleFile.Count(); i++)
                {
                    var path = "";
                    string[] extension = list_ModuleFile[i].FileName.Split('.');
                    var extension1 = Path.GetExtension(list_ModuleFile[i].FileName);
                    //if (extension[1].ToLower() == "pdf")
                    if ((extension1.ToLower() == ".pdf") && (educationModule.UploadFile == "Upload Content"))
                    {
                        path = _storageService.SetModulePDFStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), list_ModuleFile[i].FileName);
                        educationModuleFile.FileTypeID = GlobalConst.FileTypes.TEXT;
                        list_ModuleFile[i].SaveAs(path);
                        educationModuleFile.ModuleFile = Path.GetFileName(path);
                        educationModule.EducationModulePDFName = Path.GetFileName(path);
                        educationModule.EducationModuleTypeFile = GlobalConst.FileTypes.TEXT;
                    }
                    else if ((extension1.ToLower() == ".pdf") && (educationModule.UploadFile == "Upload PPT"))
                    {
                        path = _storageService.SetModulePPTStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), list_ModuleFile[i].FileName);
                        educationModuleFile.FileTypeID = GlobalConst.FileTypes.PPT;
                        list_ModuleFile[i].SaveAs(path);
                        educationModuleFile.ModuleFile = Path.GetFileName(path);
                        educationModule.EducationModulePPTName = Path.GetFileName(path);
                        educationModule.EducationModuleTypeFile = GlobalConst.FileTypes.PPT;
                    }
                    else if (extension1.ToLower() == ".flv" || extension1.ToLower() == ".mp4" || extension1.ToLower() == ".mkv" || extension1.ToLower() == ".webm" || extension1.ToLower() == ".wmv" || extension1.ToLower() == ".avi")
                    {

                        path = _storageService.SetModuleVideoStoragePath(Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.DoubleBackSlash + HCRGCLIENT.ClientID), list_ModuleFile[i].FileName);
                        list_ModuleFile[i].SaveAs(path);

                        //GEt from vzaar
                        Client.client_id = System.Configuration.ConfigurationManager.AppSettings["Client_id"];
                        //GEt from vzaar
                        Client.auth_token = System.Configuration.ConfigurationManager.AppSettings["Auth_Token"];
                        //GEt vedio from clinet upload directly from client machine below is Vedio file its type only .mp4
                        Client.filepath = path;

                        string _uploadResult = Videos.UsingVideoCreateFromFile(Client.client_id, Client.auth_token, Client.filepath);
                       // string IsCorrect = "True";
                        string _videoNum = "";
                        string toBeSearched = "Id: ";

                        int ix = _uploadResult.IndexOf(toBeSearched);
                        if (ix != -1)
                        {
                            _videoNum = _uploadResult.Replace("Id: ", "");
                            educationModuleFile.ModuleFile = _videoNum;
                        }
                        else
                            educationModuleFile.ModuleFile = null;
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        educationModuleFile.FileTypeID = GlobalConst.FileTypes.Video;
                        educationModule.EducationModuleTypeFile = GlobalConst.FileTypes.Video;
                        educationModule.EducationModuleVideoName = Path.GetFileName(path);
                    }
                }
                educationModuleFile.EducationModuleID = moduleID;
                var s1 = _educationModuleService.AddEducationModuleFile(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModuleFile>(educationModuleFile));
            }
            else if (educationModule.EducationModuleVideoName != null)
            {
                string videourl = educationModule.EducationModuleVideoName;
                if (videourl.Contains("="))
                {
                    string[] tokens = videourl.Split('=');
                    videourl = "//www.youtube.com/embed/" + tokens[1];
                }
                educationModuleFile.FileTypeID = GlobalConst.FileTypes.Video;
                educationModule.EducationModuleTypeFile = GlobalConst.FileTypes.Video;
                educationModuleFile.EducationModuleID = moduleID;
                educationModuleFile.ModuleFile = videourl;
                var s1 = _educationModuleService.AddEducationModuleFile(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModuleFile>(educationModuleFile));
            }
            else if (Mtext != "")
            {
                educationModuleFile.FileTypeID = GlobalConst.FileTypes.TEXT;
                educationModule.EducationModuleTypeFile = GlobalConst.FileTypes.Video;
                educationModuleFile.EducationModuleID = moduleID;
                educationModuleFile.ModuleFile = Mtext == null ? "" : Mtext;
                var s1 = _educationModuleService.AddEducationModuleFile(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModuleFile>(educationModuleFile));
            }
            return educationModule;
        }
        [HttpPost]
        public ActionResult GetEducationModuleFile(int educationmoduleid)
        {
            var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationmoduleid);
            EducationModuleFile educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(educationmoduleid));
            if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.Video)
                educationModuleFile.ModuleFile = "http://view.vzaar.com/" + educationModuleFile.ModuleFile + "/player";
            else if (educationModuleFile.FileTypeID == GlobalConst.FileTypes.TEXT)
            {
                string decodedHTML = HttpUtility.HtmlDecode(educationModuleFile.ModuleFile);
                educationModuleFile.ModuleFile = decodedHTML;
            }
            return Json(educationModuleFile);
        }


        public FileResult Download(int educationmoduleid)
        {
            var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationmoduleid);
            EducationModuleFile educationModuleFile = Mapper.Map<EducationModuleFile>(_educationModuleService.GetEducationModuleFileByModuleID(educationmoduleid));
            educationModuleFile.ModuleFile = GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _client.ClientID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.MgmtDirectoryStructure.ModulePPT + GlobalConst.ConstantChar.ForwardSlash + educationModuleFile.ModuleFile;
            educationModuleFile.EducationModulePPTName = Path.GetFileName(educationModuleFile.ModuleFile);
            return File(educationModuleFile.ModuleFile, "application/pdf", educationModuleFile.EducationModulePPTName);
        }

        [HttpPost]
        public ActionResult UpdateEducationModuleFile(EducationModuleFile educationModulefile, string EducationModuleTime, List<HttpPostedFileBase> list_ModuleFile1)
        {
            try
            {
                if (educationModulefile.EducationModuleText != null)
                {
                    educationModulefile.FileTypeID = GlobalConst.FileTypes.TEXT;
                    educationModulefile.ModuleFile = educationModulefile.EducationModuleText;
                }
                else if (educationModulefile.EducationModulePPT != null)
                {
                    var path = "";
                    var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationModulefile.EducationModuleID);
                    educationModulefile.ModuleFile = Path.GetFileName(educationModulefile.EducationModulePPT.FileName);
                    educationModulefile.UploadFilePath = Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _client.ClientID + GlobalConst.ConstantChar.ForwardSlash);
                    educationModulefile.FileTypeID = GlobalConst.FileTypes.PPT;
                    path = _storageService.SetModulePPTStoragePath(educationModulefile.UploadFilePath, educationModulefile.ModuleFile);
                    educationModulefile.EducationModulePPT.SaveAs(path);
                    educationModulefile.ModuleFile = Path.GetFileName(path);
                }
                else if (educationModulefile.EducationModuleVideo != null)
                {
                    var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationModulefile.EducationModuleID);
                    var path = "";
                    educationModulefile.ModuleFile = Path.GetFileName(educationModulefile.EducationModuleVideo.FileName);
                    educationModulefile.UploadFilePath = Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _client.ClientID + GlobalConst.ConstantChar.ForwardSlash);
                    educationModulefile.FileTypeID = GlobalConst.FileTypes.Video;
                    path = _storageService.SetModuleVideoStoragePath(educationModulefile.UploadFilePath, educationModulefile.ModuleFile);
                    educationModulefile.EducationModuleVideo.SaveAs(path);
                    educationModulefile.UploadFilePath = path;

                    //GEt from vzaar
                    Client.client_id = System.Configuration.ConfigurationManager.AppSettings["Client_id"];
                    //GEt from vzaar
                    Client.auth_token = System.Configuration.ConfigurationManager.AppSettings["Auth_Token"];
                    //GEt vedio from clinet upload directly from client machine below is Vedio file its type only .mp4
                    Client.filepath = path;

                    string _uploadResult = Videos.UsingVideoCreateFromFile(Client.client_id, Client.auth_token, Client.filepath);
                   // string IsCorrect = "True";
                    string _videoNum = "";
                    string toBeSearched = "Id: ";

                    int ix = _uploadResult.IndexOf(toBeSearched);
                    if (ix != -1)
                    {
                        _videoNum = _uploadResult.Replace("Id: ", "");
                        educationModulefile.ModuleFile = "http://view.vzaar.com/" + _videoNum + "/player";
                    }
                    else
                        educationModulefile.ModuleFile = null;
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                    educationModulefile.EducationModuleVideo = null;
                }
                else if (educationModulefile.EducationModuleVideoName != null)
                {
                    string videourl = educationModulefile.EducationModuleVideoName;
                    if (videourl.Contains("="))
                    {
                        string[] tokens = videourl.Split('=');
                        videourl = "//www.youtube.com/embed/" + tokens[1];
                    }
                    educationModulefile.FileTypeID = GlobalConst.FileTypes.Video;
                    educationModulefile.ModuleFile = videourl;
                }
                else if (list_ModuleFile1.Count() > 0 && list_ModuleFile1[0] != null)
                {
                    var _client = _educationModuleService.GetOrganizationClientByEducationModuleID(educationModulefile.EducationModuleID);
                    var path = "";
                    string uid = Guid.NewGuid().ToString();
                    educationModulefile.FileTypeID = GlobalConst.FileTypes.PPT;
                    educationModulefile.ModuleFile = Guid.NewGuid().ToString() + Path.GetExtension(list_ModuleFile1[0].FileName);
                    educationModulefile.UploadFilePath = Server.MapPath(GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + _client.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + _client.ClientID + GlobalConst.ConstantChar.ForwardSlash);
                    path = _storageService.SetModulePPTStoragePath(educationModulefile.UploadFilePath, educationModulefile.ModuleFile);
                    list_ModuleFile1[0].SaveAs(path);
                    educationModulefile.UploadFilePath = path;
                    educationModulefile.ModuleFile = Path.GetFileName(path);
                }
                int edcuationModuleID = _educationModuleService.UpdateEducationModuleFile(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModuleFile>(educationModulefile));
                if (educationModulefile.EducationModuleVideo != null)
                {
                    EducationModule educationModule = new EducationModule();
                    educationModule.EducationModuleID = educationModulefile.EducationModuleID;
                    if (EducationModuleTime == null || EducationModuleTime == "")
                        educationModule.EducationModuleTime = null;
                    else
                        educationModule.EducationModuleTime = EducationModuleTime;
                    _educationModuleService.UpdateEducationModuleTime(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModule>(educationModule));
                }
                if (educationModulefile.FileTypeID == 2)
                {
                    EducationModule educationModule = new EducationModule();
                    educationModule.EducationModuleID = educationModulefile.EducationModuleID;
                    educationModule.EducationModuleTime = null;
                    _educationModuleService.UpdateEducationModuleTime(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationModuleService.EducationModule>(educationModule));
                }
                return Json(educationModulefile, GlobalConst.Message.text_html);
            }
            catch 
            {
                return Json("error", GlobalConst.Message.text_html);
            }
        }
    }
}