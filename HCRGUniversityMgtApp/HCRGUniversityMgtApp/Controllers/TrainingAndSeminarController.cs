using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.TrainingAndSeminar;
using HCRGUniversityMgtApp.Domain.ViewModels.TrainingAndSeminarViewModel;
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
    public class TrainingAndSeminarController : BaseController
    {
        // GET: TrainingAndSeminar

        private readonly NEPService.NewsService.INewsService _NewsService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public TrainingAndSeminarController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;
        }

        public ActionResult Index()
        {
            TrainingSeminarViewModel _objTrainingAndSeminarResults = new TrainingSeminarViewModel();
            _objTrainingAndSeminarResults.TrainingAndSeminarResults = Mapper.Map<IEnumerable<TrainingAndSeminar>>(_NewsService.GetAllTrainingAndSeminarByOrganizationID(0,HCRGCLIENT.ClientID));

            if (HCRGCLIENT.ClientTypeID == 1)
                _objTrainingAndSeminarResults.IsHCRGAdmin = true;
            else
                _objTrainingAndSeminarResults.IsHCRGAdmin = false;
            foreach (TrainingAndSeminar _trainingAndSeminar in _objTrainingAndSeminarResults.TrainingAndSeminarResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _trainingAndSeminar.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_trainingAndSeminar.TrainingAndSeminarDesc), String.Empty);
                _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Replace("&nbsp;", "");
                if (_trainingAndSeminar.DescriptionShort.Length > 1000)
                    _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Substring(0, 1000);
            }

            Editor Editor = new Editor(System.Web.HttpContext.Current, "Editor");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;



            Editor.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor.FullScreen = false;
            Editor.DisabledItems = "save, help";
            string content = Request.Form["Editor"];
            Editor.MvcInit();
            ViewData["Editor"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return View(_objTrainingAndSeminarResults);
        }

        
        [HttpPost]
        public ActionResult AddTrainingAndSeminar(TrainingAndSeminar _trainingAndSeminar, string hdTrainingAndSeminarID)
        {          
            if (hdTrainingAndSeminarID == "")
            {
                _trainingAndSeminar.OrganizationID = HCRGCLIENT.OrganizationID;
                var _trainingAndSeminarPolicyID = _NewsService.AddTrainingAndSeminar(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.TrainingAndSeminar>(_trainingAndSeminar));
                _trainingAndSeminar.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                _trainingAndSeminar.TrainingAndSeminarID = _trainingAndSeminarPolicyID;
                _trainingAndSeminar.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _trainingAndSeminar.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_trainingAndSeminar.TrainingAndSeminarDesc), String.Empty);
                _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Replace("&nbsp;", "");
                if (_trainingAndSeminar.DescriptionShort.Length > 1000)
                    _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                _trainingAndSeminar.TrainingAndSeminarID = Convert.ToInt32(hdTrainingAndSeminarID);
                var privacyPolicyID = _NewsService.UpdateTrainingAndSeminar(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.TrainingAndSeminar>(_trainingAndSeminar));
                _trainingAndSeminar.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _trainingAndSeminar.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_trainingAndSeminar.TrainingAndSeminarDesc), String.Empty);
                _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Replace("&nbsp;", "");
                if (_trainingAndSeminar.DescriptionShort.Length > 1000)
                    _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Substring(0, 1000);
            }
            return Json(_trainingAndSeminar, GlobalConst.Message.text_html);  
        }

        [HttpPost]
        public JsonResult GetTrainingAndSeminarByOrganizationID(int organizationID)
        {
            TrainingSeminarViewModel _objTrainingAndSeminarResults = new TrainingSeminarViewModel();
            _objTrainingAndSeminarResults.TrainingAndSeminarResults = Mapper.Map<IEnumerable<TrainingAndSeminar>>(_NewsService.GetAllTrainingAndSeminarByOrganizationID(organizationID, HCRGCLIENT.ClientID));

            foreach (TrainingAndSeminar _trainingAndSeminar in _objTrainingAndSeminarResults.TrainingAndSeminarResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _trainingAndSeminar.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_trainingAndSeminar.TrainingAndSeminarDesc), String.Empty);
                _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Replace("&nbsp;", "");
                if (_trainingAndSeminar.DescriptionShort.Length > 1000)
                    _trainingAndSeminar.DescriptionShort = _trainingAndSeminar.DescriptionShort.Substring(0, 1000);
            }
            Editor Editor = new Editor(System.Web.HttpContext.Current, "Editor");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;

            Editor.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            Editor.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            Editor.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            Editor.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            Editor.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            Editor.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            Editor.FullScreen = false;
            Editor.DisabledItems = "save, help";
            string content = Request.Form["Editor"];
            Editor.MvcInit();
            ViewData["Editor"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return Json(_objTrainingAndSeminarResults);
        }

    }
}