using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.AccreditorModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System.Collections.Generic;
using System.Web.Mvc;

namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class AccreditorController : BaseController
    {
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.ClientService.IClientService _clientService;
        
        public AccreditorController(NEPService.EducationService.IEducationService educationService, NEPService.ClientService.IClientService clientService)
        {
            _educationService = educationService;
            _clientService = clientService;
        }
        public ActionResult Index()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedAccreditor pagedAccreditor = new PagedAccreditor();
            if (HCRGCLIENT.ClientTypeID == 1)
                pagedAccreditor.IsHCRGAdmin = true;
            else
                pagedAccreditor.IsHCRGAdmin = false;
            pagedAccreditor.AccreditorRecords = Mapper.Map<IEnumerable<Accreditor>>(_educationService.GetAllAccreditorsByOrganizationID(HCRGCLIENT.ClientID,_organizationID));
            return View(pagedAccreditor);
        }
        [HttpPost]
        public JsonResult GetAllAccreditorsByOrganizationID(int orgID)
        {
            PagedAccreditor pagedAccreditor = new PagedAccreditor();
            pagedAccreditor.AccreditorRecords = Mapper.Map<IEnumerable<Accreditor>>(_educationService.GetAllAccreditorsByOrganizationID(HCRGCLIENT.ClientID, orgID));
            return Json(pagedAccreditor);
        }
        [HttpPost]
        public ActionResult Add(Accreditor accreditor)
        {
            if (accreditor.AccreditorID == 0)
            {
                accreditor.IsActive = true;
                accreditor.OrganizationID = HCRGCLIENT.OrganizationID;
                accreditor.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                accreditor.AccreditorID= _educationService.AddAccreditor(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Accreditor>(accreditor));
                accreditor.flag = true;
            }
            else
            {                
                int accreditorId = _educationService.UpdateAccreditor(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Accreditor>(accreditor));
                accreditor.flag = false;
                
            }
            return Json(accreditor, GlobalConst.Message.text_html);
        }
        public ActionResult GetAllPagedAccreditor()
        {
            PagedAccreditor pagedAccreditor = new PagedAccreditor();
            pagedAccreditor.AccreditorRecords = Mapper.Map<IEnumerable<Accreditor>>(_educationService.GetAllAccreditorsByOrganizationID(HCRGCLIENT.ClientID,HCRGCLIENT.OrganizationID));
            return Json(pagedAccreditor);
        }
        [HttpPost]
        public JsonResult DeleteAccreditor(Accreditor accreditor)
        {
            if (accreditor.IsActive == true)
                accreditor.IsActive = false;
            else
                accreditor.IsActive = true;
            _educationService.DeleteAccreditor(accreditor.AccreditorID, accreditor.IsActive);
            return Json(accreditor, GlobalConst.Message.text_html);
        }
        [HttpGet]
        public JsonResult GetAllAccreeditor()
        {
            IEnumerable<Accreditor> AccreditorRecords = Mapper.Map<IEnumerable<Accreditor>>(_educationService.getAll());
            return Json(AccreditorRecords, GlobalConst.Message.text_html);
        }
    }
}