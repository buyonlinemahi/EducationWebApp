using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.EducationFormatModel;
using HCRGUniversityMgtApp.Domain.ViewModels.EducationFormatViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class EducationFormatController : BaseController
    {
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.ClientService.IClientService _clientService;

        public EducationFormatController(NEPService.EducationService.IEducationService educationService, NEPService.ClientService.IClientService clientService)
        {
            _educationService = educationService;
            _clientService = clientService;

        }

        public ActionResult Index()
        {
            EducationFormatViewModel educationformatModel = new EducationFormatViewModel();         
            educationformatModel.EducationFormatResults = Mapper.Map<IEnumerable<EducationFormat>>(_educationService.GetAllEducationFormatByClientID(HCRGCLIENT.ClientID));
            if (HCRGCLIENT.ClientTypeID == 1)
                educationformatModel.IsHCRGAdmin = true;
            else
                educationformatModel.IsHCRGAdmin = false;
            return View(educationformatModel);
        }

        [HttpPost]
        public JsonResult GetAllEducationFormatByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<EducationFormat>>(_educationService.GetAllEducationFormatByOrganizationID(OrgID));
            return Json(data);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(EducationFormat educationformat, string hdEducationFormatID)
        {
            EducationFormatViewModel educationModel = new EducationFormatViewModel();

            if (hdEducationFormatID == "")
            {
                educationformat.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                educationformat.ClientID = HCRGCLIENT.ClientID;
                var educationFormatID = _educationService.AddEducationFormat(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormat>(educationformat));
                educationformat.EducationFormatID = educationFormatID;
                
                educationformat.flag = true;
            }
            else
            {               
                educationformat.EducationFormatID = Convert.ToInt32(hdEducationFormatID);
                var aboutusID = _educationService.UpdateEducationFormat(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.EducationFormat>(educationformat));
                educationformat.flag = false;
            }
            return Json(educationformat, GlobalConst.Message.text_html);
        }

     
    }
}
