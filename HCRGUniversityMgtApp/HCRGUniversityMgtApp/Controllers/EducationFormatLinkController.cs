using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatAvailableModel;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatModel;
using HCRGUniversityMgtApp.Domain.Models.EducationModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationFormatDetailViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationFormatViewModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;


namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EducationFormatLinkController : BaseController
    {
        //
        // GET: /EducationFormatLink/
        private readonly NEPService.EducationService.IEducationService _educationService;
        public EducationFormatLinkController(NEPService.EducationService.IEducationService educationService)
        {
             _educationService = educationService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult getAllEducationActive()
        {
            EducationViewModel educationModel = new EducationViewModel();
            educationModel.EducationResults = Mapper.Map<IEnumerable<Education>>(_educationService.getAllEducationActive());
            return Json(educationModel.EducationResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetEducationAndEducationFormat()
        {

            EducationFormatDetailViewModel educationFormatDetailViewModel = new EducationFormatDetailViewModel();
            educationFormatDetailViewModel.EducationFormatDetailResults = Mapper.Map<IEnumerable<EducationFormatDetail>>(_educationService.GetEducationAndEducationFormat(HCRGCLIENT.ClientID));
            return Json(educationFormatDetailViewModel.EducationFormatDetailResults, GlobalConst.Message.text_html);
        }

        public ActionResult GetEducationFormatNotAssociateWithEducation(int educationID)
        {
            EducationFormatViewModel educationFormatViewModel = new EducationFormatViewModel();
            educationFormatViewModel.EducationFormatResults = Mapper.Map<IEnumerable<EducationFormat>>(_educationService.GetEducationFormatNotAssociateWithEducation(educationID));
            return Json(educationFormatViewModel.EducationFormatResults, GlobalConst.Message.text_html);
          
        }

        public ActionResult LinkEducationFormatEducation(IEnumerable<EducationFormatAvailable> myList, int EducationID)
        {
            foreach (var item in myList)
            {
                EducationFormatAvailable educationFormatAvailable = new EducationFormatAvailable();
                educationFormatAvailable.EducationFormatID = item.EducationFormatID;
                educationFormatAvailable.EducationID = EducationID;
                educationFormatAvailable.IsActive = true;
                var EducationFormatlinkID = _educationService.AddEducationFormatAvailable(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>(educationFormatAvailable));
            }
            return Json("Education Format linked Successfully");

        }

        [HttpPost]
        public JsonResult DisableEducationEducationFormat(EducationFormatAvailable educationFormatAvailable)
        {

            educationFormatAvailable.EducationFormatID = educationFormatAvailable.EducationFormatID;
            educationFormatAvailable.EducationID = educationFormatAvailable.EducationID;
            educationFormatAvailable.EducationAvailableID = educationFormatAvailable.EducationAvailableID;

            if (educationFormatAvailable.IsActive == false)
                educationFormatAvailable.IsActive = true;
            else
                educationFormatAvailable.IsActive = false;
            var EducationFormatlinkID = _educationService.UpdateEducationFormatAvailable(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormatAvailable>(educationFormatAvailable));
            educationFormatAvailable.IsActive = false;
            return Json("Change done  Successfully");
        }
    }
}
