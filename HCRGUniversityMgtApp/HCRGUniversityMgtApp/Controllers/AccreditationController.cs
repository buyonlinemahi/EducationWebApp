using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.AccreditationModel;
using HCRGUniversityMgtApp.Domain.ViewModels.AccreditationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using RTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class AccreditationController : BaseController
    {
     private readonly NEPService.NewsService.INewsService _NewsService;
     private readonly NEPService.ClientService.IClientService _clientService;
     public AccreditationController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;  
        }
        public ActionResult Index()
        {
            AccreditationViewModel _objAccreditationResults = new AccreditationViewModel();
            _objAccreditationResults.AccreditationResults = Mapper.Map<IEnumerable<Accreditation>>(_NewsService.GetAllAccreditationsByOrganizationID(0, HCRGCLIENT.ClientID));

            if (HCRGCLIENT.ClientTypeID == 1)
                _objAccreditationResults.IsHCRGAdmin = true;
            else
                _objAccreditationResults.IsHCRGAdmin = false;
            foreach (Accreditation _accreditation in _objAccreditationResults.AccreditationResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _accreditation.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_accreditation.AccreditationContent), String.Empty);
                _accreditation.DescriptionShort = _accreditation.DescriptionShort.Replace("&nbsp;", "");
                if (_accreditation.DescriptionShort.Length > 1000)
                    _accreditation.DescriptionShort = _accreditation.DescriptionShort.Substring(0, 1000);                      
            }

            Editor Editor = new Editor(System.Web.HttpContext.Current, "EditorAccreditation");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;
            Editor.FullScreen = false;


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

            Editor.DisabledItems = "save, help";
            string content = Request.Form["EditorAccreditation"];
            Editor.MvcInit();
            ViewData["EditorAccreditation"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return View(_objAccreditationResults);
        }

        [HttpPost]
        public ActionResult Update(Accreditation accreditation, string hdAccreditationID)
        {            
            if (hdAccreditationID == "")
            {
                accreditation.OrganizationID = HCRGCLIENT.OrganizationID;
                var accreditationID = _NewsService.AddAccreditation(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Accreditation>(accreditation));
                accreditation.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                accreditation.AccreditationID = accreditationID;
                accreditation.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                accreditation.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(accreditation.AccreditationContent), String.Empty);
                accreditation.DescriptionShort = accreditation.DescriptionShort.Replace("&nbsp;", "");
                if (accreditation.DescriptionShort.Length > 1000)
                    accreditation.DescriptionShort = accreditation.DescriptionShort.Substring(0, 1000);                    
            }
            else
            {
                accreditation.AccreditationID = Convert.ToInt32(hdAccreditationID);
                var accreditationID = _NewsService.UpdateAccreditation(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Accreditation>(accreditation));
                accreditation.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                accreditation.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(accreditation.AccreditationContent), String.Empty);
                accreditation.DescriptionShort = accreditation.DescriptionShort.Replace("&nbsp;", "");
                if (accreditation.DescriptionShort.Length > 1000)
                    accreditation.DescriptionShort = accreditation.DescriptionShort.Substring(0, 1000);
            }
            return Json(accreditation, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetAccreditationByOrganizationID(int organizationID)
        {
            AccreditationViewModel _objAccreditationResults = new AccreditationViewModel();
            _objAccreditationResults.AccreditationResults = Mapper.Map<IEnumerable<Accreditation>>(_NewsService.GetAllAccreditationsByOrganizationID(organizationID, HCRGCLIENT.ClientID));

            foreach (Accreditation _accreditation in _objAccreditationResults.AccreditationResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _accreditation.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_accreditation.AccreditationContent), String.Empty);
                _accreditation.DescriptionShort = _accreditation.DescriptionShort.Replace("&nbsp;", "");
                if (_accreditation.DescriptionShort.Length > 1000)
                    _accreditation.DescriptionShort = _accreditation.DescriptionShort.Substring(0, 1000);                    
            }
            Editor Editor = new Editor(System.Web.HttpContext.Current, "EditorAccreditation");

            Editor.ClientFolder = "/richtexteditor/";
            Editor.Width = Unit.Pixel(1050);
            Editor.Height = Unit.Pixel(660);
            Editor.ResizeMode = RTEResizeMode.Disabled;
            Editor.FullScreen = false;


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

            Editor.DisabledItems = "save, help";
            string content = Request.Form["EditorAccreditation"];
            Editor.MvcInit();
            ViewData["EditorAccreditation"] = Editor.MvcGetString();
            Editor.ResizeMode = RTEResizeMode.Disabled;
            return Json(_objAccreditationResults);
        }
    }
}