using AutoMapper;
using HCRGUniversityApp.Domain.Models.EducationModel;
using HCRGUniversityApp.Domain.ViewModels.EducationTypeAvailableViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
namespace HCRGUniversityApp.Controllers
{
    public class CertificationProgramController : BaseController
    {
        private readonly NEPService.EducationService.IEducationService _educationService;
        public readonly IEncryption _encryptionService;
        public CertificationProgramController(NEPService.EducationService.IEducationService educationService, IEncryption encryptionService)
        {
            _educationService = educationService;
            _encryptionService = encryptionService;
        }
        [HttpPost]
        public JsonResult ShowDetail(int Educationid)
        {
            EducationID = Educationid;
            return Json(EducationID, GlobalConst.Message.text_html);
        }

        public ActionResult Index(string eid)
        {
            eid = _encryptionService.DecryptString2(eid);
            Int32 eduId = 0;
            if (eid == "" || eid == null)
                eduId = EducationID;
            else
                eduId = Convert.ToInt32(eid);
            EducationID = eduId;
            EducationTypesAvailableViewModel EducationTypesAvailableViewModel = new Domain.ViewModels.EducationTypeAvailableViewModel.EducationTypesAvailableViewModel();
            EducationTypesAvailableViewModel.EducationTypesAvailableResults = Mapper.Map<IEnumerable<EducationTypesAvailable>>(_educationService.GetEducationTypeByEducationID(eduId));
            EducationTypesAvailableViewModel.EducationDetailPageResults = Mapper.Map<EducationDetailPageWithEducation>(_educationService.GetEducationDetailPageContent(eduId));
            EducationTypesAvailableViewModel.EducationCredentialResults = Mapper.Map<IEnumerable<EducationCredential>>(_educationService.GetEducationCredentialByEducationID(eduId));
            EducationTypesAvailableViewModel.Education = Mapper.Map<Education>(_educationService.GetEducationByID(eduId));
            //   string a = EducationTypesAvailableViewModel.Education.CourseStartDate.ToString("yyyy-MM-dd");

            if (EducationTypesAvailableViewModel.Education.CourseStartDate <= DateTime.Now && EducationTypesAvailableViewModel.Education.CourseEndDate > DateTime.Now)
                EducationTypesAvailableViewModel.Education.CanPurchase = true;
            else
                EducationTypesAvailableViewModel.Education.CanPurchase = false;
            string decodedHTML = HttpUtility.HtmlDecode(EducationTypesAvailableViewModel.EducationDetailPageResults.PContent);
            if (decodedHTML != null)
            {
                EducationTypesAvailableViewModel.EducationDetailPageResults.PContent = decodedHTML.Replace(GlobalConst.Message.SlashStoragePath, GlobalConst.Message.StoragePath);
                EducationTypesAvailableViewModel.EducationDetailPageResults.PContent = EducationTypesAvailableViewModel.EducationDetailPageResults.PContent.Replace("/richtexteditor/", GlobalConst.Extension.http + GlobalConst.ConstantChar.Colon + GlobalConst.ConstantChar.ForwardSlash + GlobalConst.ConstantChar.ForwardSlash + Request.Url.Host.ToLower() + GlobalConst.ConstantChar.Colon + Request.Url.Port + "/richtexteditor/");
                EducationTypesAvailableViewModel.EducationDetailPageResults.PContent = EducationTypesAvailableViewModel.EducationDetailPageResults.PContent.Replace("&nbsp;", "");
            }
            else
                EducationTypesAvailableViewModel.EducationDetailPageResults.PContent = EducationTypesAvailableViewModel.EducationDetailPageResults.PContent;
            return View(EducationTypesAvailableViewModel);
        }
        [HttpPost]
        public JsonResult EncryptQueryString(string plainText)
        {
            return Json(_encryptionService.EncryptString2(plainText), GlobalConst.Message.text_html);
        }
    }
}
