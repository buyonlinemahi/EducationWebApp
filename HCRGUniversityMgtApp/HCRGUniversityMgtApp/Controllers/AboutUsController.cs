using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.AboutUsModel;
using HCRGUniversityMgtApp.Domain.ViewModels.AboutUsViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class AboutUsController : BaseController
    {
        private readonly NEPService.AboutUsService.IAboutUsService _aboutusService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public AboutUsController(NEPService.AboutUsService.IAboutUsService aboutusService, NEPService.ClientService.IClientService clientService)
        {
            _aboutusService = aboutusService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/"; 
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");
      
             
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();

            PagedAboutUs aboutusModel = new PagedAboutUs();
            aboutusModel.AboutUsRecords = Mapper.Map<IEnumerable<AboutUs>>(_aboutusService.GetAllAboutUsByOrganizationID(0, HCRGCLIENT.ClientID));

            //aboutusModel = Mapper.Map<PagedAboutUs>(_aboutusService.GetAllPagedAboutus(GlobalConst.Records.Skip, GlobalConst.Records.Take, HCRGCLIENT.OrganizationID));
            foreach (AboutUs viewmodel in aboutusModel.AboutUsRecords)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.Description), String.Empty);
                viewmodel.DescriptionShort = viewmodel.DescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.DescriptionShort.Length > 1000)
                    viewmodel.DescriptionShort = viewmodel.DescriptionShort.Substring(0, 1000);
            }
            if (HCRGCLIENT.ClientTypeID == 1)
                aboutusModel.IsHCRGAdmin = true;
            else
                aboutusModel.IsHCRGAdmin = false;
            aboutusModel.PagedRecords = GlobalConst.Records.Take;
            return View(aboutusModel);
        }

        [HttpPost]
        public JsonResult GetAboutUsByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<AboutUs>>(_aboutusService.GetAllAboutUsByOrganizationID(OrgID, HCRGCLIENT.ClientID));
            foreach (AboutUs viewmodel in data)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.Description), String.Empty);
                viewmodel.DescriptionShort = viewmodel.DescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.DescriptionShort.Length > 1000)
                    viewmodel.DescriptionShort = viewmodel.DescriptionShort.Substring(0, 1000);
            }
            return Json(data);
        }

        public ActionResult ShowEditor()
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            Editor1.FullScreen = false;

            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");
            
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;
            return View();
        }

        public ActionResult GetAllPagedAboutus(int skip, int? take)
        {
            HCRGUniversityMgtApp.Domain.Models.Client.Client clientSession = (HCRGUniversityMgtApp.Domain.Models.Client.Client)Session[GlobalConst.SessionKeys.CLIENT];
            PagedAboutUs aboutusModel = new PagedAboutUs();
            if (take == null)
                aboutusModel = Mapper.Map<PagedAboutUs>(_aboutusService.GetAllPagedAboutus(skip, GlobalConst.Records.Take, clientSession.ClientID));
            else
                aboutusModel = Mapper.Map<PagedAboutUs>(_aboutusService.GetAllPagedAboutus(skip, take.Value, clientSession.ClientID));

            foreach (AboutUs viewmodel in aboutusModel.AboutUsRecords)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                viewmodel.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(viewmodel.Description), String.Empty);
                viewmodel.DescriptionShort = viewmodel.DescriptionShort.Replace("&nbsp;", "");
                if (viewmodel.DescriptionShort.Length > 1000)
                    viewmodel.DescriptionShort = viewmodel.DescriptionShort.Substring(0, 1000);
            }
            aboutusModel.PagedRecords = GlobalConst.Records.Take;
            return Json(aboutusModel, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public ActionResult Index(AboutUsViewModel aboutusModel)
        {
            aboutusModel.AboutUSResults = Mapper.Map<IEnumerable<AboutUs>>(_aboutusService.getAllRecords(HCRGCLIENT.OrganizationID));
            return Json(aboutusModel, GlobalConst.Message.text_html);
        }
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(AboutUs aboutus, string hdAboutUsID)
        {
            Editor Editor1 = new Editor(System.Web.HttpContext.Current, "Editor1");
            Editor1.ClientFolder = "/richtexteditor/";
            Editor1.Width = Unit.Pixel(1050);
            Editor1.Height = Unit.Pixel(660);
            Editor1.ResizeMode = RTEResizeMode.Disabled;


            Editor1.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor1.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor1.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor1.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor1.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor1.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];
            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();

            AboutUsViewModel aboutusModel = new AboutUsViewModel();
            if (hdAboutUsID == "")
            {
                aboutus.OrganizationID = HCRGCLIENT.OrganizationID;
                var aboutusID = _aboutusService.AddAboutUs(Mapper.Map<HCRGUniversityMgtApp.NEPService.AboutUsService.AboutUs>(aboutus));
                aboutus.AboutUsID = aboutusID;
                aboutus.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                aboutus.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                aboutus.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(aboutus.Description), String.Empty);
                aboutus.DescriptionShort = aboutus.DescriptionShort.Replace("&nbsp;", "");
                if (aboutus.DescriptionShort.Length > 1000)
                    aboutus.DescriptionShort = aboutus.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                aboutus.AboutUsID = Convert.ToInt32(hdAboutUsID);
                var aboutusID = _aboutusService.UpdateAboutUs(Mapper.Map<HCRGUniversityMgtApp.NEPService.AboutUsService.AboutUs>(aboutus));
                aboutus.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                aboutus.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(aboutus.Description), String.Empty);
                aboutus.DescriptionShort = aboutus.DescriptionShort.Replace("&nbsp;", "");
                if (aboutus.DescriptionShort.Length > 1000)
                    aboutus.DescriptionShort = aboutus.DescriptionShort.Substring(0, 1000);
            }
            return Json(aboutus, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult DeleteAboutUsInfo(AboutUs aboutus)
        {
            _aboutusService.DeleteAboutUs(aboutus.AboutUsID);
            return Json("AboutUs Information deleted Successfully");
        }

        public ActionResult PreTest()
        {
            return View("../PreTest/Add_New");
        }
        public ActionResult AddNewGrid()
        {
            return View("../PreTest/Search_PreTest");
        }
    }
}