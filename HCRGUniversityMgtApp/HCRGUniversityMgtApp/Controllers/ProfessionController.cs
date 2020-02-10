using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.ProfessionModel;
using HCRGUniversityMgtApp.Domain.ViewModels.ProfessionViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class ProfessionController : BaseController
    {
        //
        // GET: /Profession/
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.ClientService.IClientService _clientService;

        public ProfessionController(NEPService.EducationService.IEducationService eductionService, NEPService.ClientService.IClientService clientService)
        {
            _educationService = eductionService;
            _clientService = clientService;
        }

        public ActionResult Index()
        {
            PagedProfession professionModel = new PagedProfession();
            professionModel = Mapper.Map<PagedProfession>(_educationService.GetAllPagedProfession(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID));
            professionModel.PagedRecords = GlobalConst.Records.LandingTake;
            if (HCRGCLIENT.ClientTypeID == 1)
                professionModel.IsHCRGAdmin = true;
            else
                professionModel.IsHCRGAdmin = false;
            return View(professionModel);
        }

        [HttpPost]
        public JsonResult GetAllProfessionByOrganizationID(int OrgID)
        {
             var data = Mapper.Map<PagedProfession>(_educationService.GetAllPagedProfessionByOrganizationID(GlobalConst.Records.Skip, GlobalConst.Records.LandingTake,OrgID));
            return Json(data);
        }
        public ActionResult GetAllPagedProfession(int skip,int ?take)
        {
            PagedProfession professionModel = new PagedProfession();
            if (take == null)
                professionModel = Mapper.Map<PagedProfession>(_educationService.GetAllPagedProfession(skip, GlobalConst.Records.LandingTake, HCRGCLIENT.ClientID));
            else
                professionModel = Mapper.Map<PagedProfession>(_educationService.GetAllPagedProfession(GlobalConst.Records.Skip, take.Value, HCRGCLIENT.ClientID));
             professionModel.PagedRecords = GlobalConst.Records.LandingTake;
            return Json(professionModel, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult Add(Profession profession, string hdProfessionID)
        {
            ProfessionViewModel ProfessionModel = new ProfessionViewModel();            
            if (hdProfessionID == "")
            {
                profession.ClientID = HCRGCLIENT.ClientID;
                profession.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                profession.IsActive = true;
                 var ProfessionID = _educationService.AddProfession(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Profession>(profession));
                profession.ProfessionID = ProfessionID;
               
                profession.flag = true;
            }
            else
            {
                profession.ProfessionID = Convert.ToInt32(hdProfessionID);
                var ProfessionID = _educationService.UpdateProfession(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Profession>(profession));
                profession.flag = false;
            }
            return Json(profession, GlobalConst.Message.text_html);
        }
        [HttpPost]
        public JsonResult DisableProfession(Profession profession)
        {
            profession.ProfessionID = profession.ProfessionID;
            if (profession.IsActive == false)
                profession.IsActive = true;
            else
                profession.IsActive = false;
            profession.ClientID = HCRGCLIENT.ClientID;
            var ProfessionID = _educationService.UpdateProfession(Mapper.Map<HCRGUniversityMgtApp.NEPService.EducationService.Profession>(profession));
            profession.flag = false;
            return Json(profession, GlobalConst.Message.text_html);

        }

        [HttpGet]
        public JsonResult getProfessions()
        {
            ProfessionViewModel ProfessionModel = new ProfessionViewModel();
            ProfessionModel.ProfessionResults = Mapper.Map<IEnumerable<Profession>>(_educationService.getAllProfessionActive(HCRGCLIENT.ClientID));
            return Json(ProfessionModel.ProfessionResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

    }
}
