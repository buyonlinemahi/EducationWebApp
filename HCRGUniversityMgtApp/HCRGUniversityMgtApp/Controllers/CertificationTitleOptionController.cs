using AutoMapper;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Configuration;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices;
using HCRGUniversityMgtApp.Domain.Models.Faculty;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System.Net;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using RTE;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using HCRGUniversityMgtApp.Infrastructure.Base;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class CertificationTitleOptionController : BaseController
    {
        private readonly NEPService.CertificationTitleOptionService.ICertificationTitleOptionService _CertificationTitleOptionService;

        public CertificationTitleOptionController(NEPService.CertificationTitleOptionService.ICertificationTitleOptionService  CertificationTitleOptionService)
        {
            _CertificationTitleOptionService = CertificationTitleOptionService;
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
            PagedCertificationTitleOption _pagedCertificationTitleOption = new PagedCertificationTitleOption();
            var _CertificationOptionList =  _CertificationTitleOptionService.GetAllPagedCertificationTitleOption(GlobalConst.Records.Skip, GlobalConst.Records.Take);
            _pagedCertificationTitleOption.CertificationTitleOptionRecords = Mapper.Map<IEnumerable<CertificationTitleOption>>(_CertificationOptionList.CertificationTitleOptionDetails);          
            _pagedCertificationTitleOption.PagedRecords = GlobalConst.Records.Take;
            _pagedCertificationTitleOption.TotalCount = _CertificationOptionList.TotalCount;
            return View(_pagedCertificationTitleOption);
        }

        [HttpPost]
        public ActionResult Add(CertificationTitleOption _certificationTitleOption)
        {
            if (_certificationTitleOption.EducationId != 0)
            {
                if (_certificationTitleOption.CertificationTitleOptionID == 0)
                {
                    _certificationTitleOption.CertificationTitleOptionID = _CertificationTitleOptionService.AddCertificationTitleOption(Mapper.Map<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption>(_certificationTitleOption));
                    _certificationTitleOption.flag = "Add";
                }
                else
                {
                    int certificationTitleOptionID = _CertificationTitleOptionService.UpdateCertificationTitleOption(Mapper.Map<HCRGUniversityMgtApp.NEPService.CertificationTitleOptionService.CertificationTitleOption>(_certificationTitleOption));
                    _certificationTitleOption.flag = "Update";
                }
            }
            else
                _certificationTitleOption.flag = "NotSaved";
            return Json(_certificationTitleOption, GlobalConst.Message.text_html);
        }

        public ActionResult GetAllPagedCertificationTitleOption(int skip, int? take)
        {
            
            PagedCertificationTitleOption _pagedCertificationTitleOption = new PagedCertificationTitleOption();
            if (take == null)
            {
                var _certification = _CertificationTitleOptionService.GetAllPagedCertificationTitleOption(skip, GlobalConst.Records.Take);
                _pagedCertificationTitleOption.CertificationTitleOptionRecords = Mapper.Map<IEnumerable<CertificationTitleOption>>(_certification.CertificationTitleOptionDetails);
            }
            else
            {
                var _certificationList = _CertificationTitleOptionService.GetAllPagedCertificationTitleOption(skip, take.Value);
                _pagedCertificationTitleOption.CertificationTitleOptionRecords = Mapper.Map<IEnumerable<CertificationTitleOption>>(_certificationList.CertificationTitleOptionDetails);
            }
            
            _pagedCertificationTitleOption.PagedRecords = GlobalConst.Records.Take;
            return Json(_pagedCertificationTitleOption, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult DeleteCertificationTitleOption(CertificationTitleOption _certificationTitleOption)
        {
            _CertificationTitleOptionService.DeleteCertificationTitleOption(_certificationTitleOption.CertificationTitleOptionID);
            return Json(_certificationTitleOption, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public JsonResult GetAllCertificationTitleOption()
        {
            IEnumerable<CertificationTitleOption> _certificationTitleOptionRecords = Mapper.Map<IEnumerable<CertificationTitleOption>>(_CertificationTitleOptionService.GetAllCertificationTitleOptions());
            return Json(_certificationTitleOptionRecords, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult GetAllCourseDropDownList()
        {
            IEnumerable<CourseNameDropDownList> _certificationTitleOptionRecords = Mapper.Map<IEnumerable<CourseNameDropDownList>>(_CertificationTitleOptionService.GetCourseDropDownList(HCRGCLIENT.OrganizationID));
            return Json(_certificationTitleOptionRecords, GlobalConst.Message.text_html);
        }

        public ActionResult ShowEditor()
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

            Editor1.FullScreen = false;
            Editor1.DisabledItems = "save, help";
            string content = Request.Form["Editor1"];

            Editor1.MvcInit();
            ViewData["Editor"] = Editor1.MvcGetString();
            Editor1.ResizeMode = RTEResizeMode.Disabled;

            return View();
        }
    }
}