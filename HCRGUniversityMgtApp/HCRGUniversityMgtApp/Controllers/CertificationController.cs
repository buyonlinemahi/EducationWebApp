using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Certification;
using HCRGUniversityMgtApp.Domain.ViewModels.CertificationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using RTE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HCRGUniversityMgtApp.Infrastructure.Global;

namespace HCRGUniversityMgtApp.Controllers
{
    public class CertificationController : BaseController
    {
        // GET: Certification
        private readonly NEPService.NewsService.INewsService _NewsService;
        private readonly NEPService.ClientService.IClientService _clientService;
        public CertificationController(NEPService.NewsService.INewsService newsService, NEPService.ClientService.IClientService clientService)
        {
            _NewsService = newsService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            CertificationViewModel _Certification = new CertificationViewModel();
            _Certification.CertificationResults = Mapper.Map<IEnumerable<Certification>>(_NewsService.GetAllCertificationsByOrganizationID(0, HCRGCLIENT.ClientID));
            if (HCRGCLIENT.ClientTypeID == 1)
                _Certification.IsHCRGAdmin = true;
            else
                _Certification.IsHCRGAdmin = false;
            foreach (Certification _certification in _Certification.CertificationResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _certification.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_certification.CertificationContent), String.Empty);
                _certification.DescriptionShort = _certification.DescriptionShort.Replace("&nbsp;", "");
                if (_certification.DescriptionShort.Length > 1000)
                    _certification.DescriptionShort = _certification.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorCertification = new Editor(System.Web.HttpContext.Current, "EditorCertification");
            EditorCertification.ClientFolder = "/richtexteditor/";
            EditorCertification.Width = Unit.Pixel(1050);
            EditorCertification.Height = Unit.Pixel(660);
            EditorCertification.ResizeMode = RTEResizeMode.Disabled;
            EditorCertification.FullScreen = false;


            EditorCertification.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");
            
            EditorCertification.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");
            
            EditorCertification.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");
            
            EditorCertification.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");
            
            EditorCertification.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorCertification.DisabledItems = "save, help";
            string content = Request.Form["EditorCertification"];
            EditorCertification.MvcInit();
            ViewData["EditorCertification"] = EditorCertification.MvcGetString();
            EditorCertification.ResizeMode = RTEResizeMode.Disabled;
            return View(_Certification);
        }

        [HttpPost]
        public ActionResult Update(Certification Certification, string hdCertificationID)
        {
            if (hdCertificationID == "")
            {
                Certification.OrganizationID = HCRGCLIENT.OrganizationID;
                var certificateID = _NewsService.AddCertification(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Certification>(Certification));
                Certification.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                Certification.CertificationID = certificateID;
                Certification.flag = true;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                Certification.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(Certification.CertificationContent), String.Empty);
                Certification.DescriptionShort = Certification.DescriptionShort.Replace("&nbsp;", "");
                if (Certification.DescriptionShort.Length > 1000)
                    Certification.DescriptionShort = Certification.DescriptionShort.Substring(0, 1000);
            }
            else
            {
                Certification.CertificationID = Convert.ToInt32(hdCertificationID);
                var certificateID = _NewsService.UpdateCertification(Mapper.Map<HCRGUniversityMgtApp.NEPService.NewsService.Certification>(Certification));
                Certification.flag = false;
                Regex regex = new Regex("\\<[^\\>]*\\>");
                Certification.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(Certification.CertificationContent), String.Empty);
                Certification.DescriptionShort = Certification.DescriptionShort.Replace("&nbsp;", "");
                if (Certification.DescriptionShort.Length > 1000)
                    Certification.DescriptionShort = Certification.DescriptionShort.Substring(0, 1000);
            }
            return Json(Certification, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetAllCertificationsByOrganizationID(int organizationID)
        {
            CertificationViewModel _Certification = new CertificationViewModel();
            _Certification.CertificationResults = Mapper.Map<IEnumerable<Certification>>(_NewsService.GetAllCertificationsByOrganizationID(organizationID, HCRGCLIENT.ClientID));
            foreach (Certification _certification in _Certification.CertificationResults)
            {
                Regex regex = new Regex("\\<[^\\>]*\\>");
                _certification.DescriptionShort = regex.Replace(HttpUtility.HtmlDecode(_certification.CertificationContent), String.Empty);
                _certification.DescriptionShort = _certification.DescriptionShort.Replace("&nbsp;", "");
                if (_certification.DescriptionShort.Length > 1000)
                    _certification.DescriptionShort = _certification.DescriptionShort.Substring(0, 1000);
            }
            Editor EditorCertification = new Editor(System.Web.HttpContext.Current, "EditorCertification");
            EditorCertification.ClientFolder = "/richtexteditor/";
            EditorCertification.Width = Unit.Pixel(1050);
            EditorCertification.Height = Unit.Pixel(660);
            EditorCertification.ResizeMode = RTEResizeMode.Disabled;
            EditorCertification.FullScreen = false;

            EditorCertification.SetSecurity("Gallery", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Gallery", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Gallery", "newimagepath ", "StorageName", "Image Files");

            EditorCertification.SetSecurity("Image", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Image", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Image", "newimagepath ", "StorageName", "Image Files");

            EditorCertification.SetSecurity("Video", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Video", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Video", "newimagepath ", "StorageName", "Video Files");

            EditorCertification.SetSecurity("Document", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Document", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Document", "newimagepath ", "StorageName", "Document Files");

            EditorCertification.SetSecurity("Template", "newimagepath", "AllowAccess", "true");
            EditorCertification.SetSecurity("Template", "newimagepath", "StoragePath", "~" + GlobalConst.FolderName.Storage + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.Org + HCRGCLIENT.OrganizationID + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.FolderName.RTEUpload, true);
            EditorCertification.SetSecurity("Template", "newimagepath ", "StorageName", "Templates");

            EditorCertification.DisabledItems = "save, help";
            string content = Request.Form["EditorCertification"];
            EditorCertification.MvcInit();
            ViewData["EditorCertification"] = EditorCertification.MvcGetString();
            EditorCertification.ResizeMode = RTEResizeMode.Disabled;
            return Json(_Certification);
        }
    }
}