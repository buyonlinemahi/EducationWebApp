using AutoMapper;
using HCRGUniversityApp.Domain.Models.CollegeEducationModel;
using HCRGUniversityApp.Domain.Models.CollegeModel;
using HCRGUniversityApp.Domain.Models.EducationFormatModel;
using HCRGUniversityApp.Domain.Models.ProfessionModel;
using HCRGUniversityApp.Domain.ViewModels.CollegeEducationViewModel;
using HCRGUniversityApp.Domain.ViewModels.CollegeViewModel;
using HCRGUniversityApp.Domain.ViewModels.EducationFormatViewModel;
using HCRGUniversityApp.Domain.ViewModels.ProfessionViewModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using HCRGUniversityApp.NEPService.ClientService;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
namespace HCRGUniversityApp.Controllers
{
    public class CollegeEducationController : BaseController
    {
        //
        // GET: /CollegeEducation/
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly NEPService.CollegeService.ICollegeService _collegeService;
        public readonly IEncryption _encryptionService;
        private readonly IClientService _clientService;

        public CollegeEducationController(NEPService.CollegeService.ICollegeService collegeService, IClientService clientService, NEPService.EducationService.IEducationService educationService, IEncryption encryptionService)
        {
            _collegeService = collegeService;
            _educationService = educationService;
            _encryptionService = encryptionService;
            _clientService = clientService;
        }
        public ActionResult Index(string id)
        {
            var _res = _clientService.GetOrganizationByID(HCRGUser != null ? HCRGUser.OrganizationID : int.Parse(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            if (_res != null)
            {
                if (_res.MenuIDs != null)
                {
                    if (!_res.MenuIDs.Contains("1"))
                        return RedirectToAction(GlobalConst.Actions.UserController.UnauthorisePage, GlobalConst.Controllers.User, new { area = "" });
                }
            }

            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            if (id == null)
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, null, null, null, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    collegeeducationviewmodel.ShowOnlineCourseOnly = false;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, null, null, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    collegeeducationviewmodel.ShowOnlineCourseOnly = false;
                }
            }
            else
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, 2, null, null, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    collegeeducationviewmodel.ShowOnlineCourseOnly = true;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, 2, null, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    collegeeducationviewmodel.ShowOnlineCourseOnly = true;
                }
            }
            return View(collegeeducationviewmodel);
        }
        [HttpGet]
        public JsonResult getColleges()
        {
             var _organizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
             var colleges = Mapper.Map<IEnumerable<College>>(_collegeService.GetAllCollegeWeb(_organizationID));
            CollegeViewModel collegeModel = new CollegeViewModel();
            collegeModel.CollegeResults = Mapper.Map<IEnumerable<College>>(colleges);
            return Json(collegeModel.CollegeResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getEducationFormat()
        {
            var educationformats = _educationService.GetAllEducationFormatByOrganizationID(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            EducationFormatViewModel educationformatModel = new EducationFormatViewModel();
            educationformatModel.EducationFormatResults = Mapper.Map<IEnumerable<EducationFormat>>(educationformats);
            return Json(educationformatModel.EducationFormatResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getProfession()
        {
            var professions = _educationService.getAllProfessionActiveWeb(HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
            ProfessionViewModel professionModel = new ProfessionViewModel();
            professionModel.ProfessionResults = Mapper.Map<IEnumerable<Profession>>(professions);
            return Json(professionModel.ProfessionResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        public ActionResult getEducationDetailBycollegeID(int collegeID, int? skip)
        {
            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            if (skip != null)
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByCollegeIDPaged(collegeID, 0, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByCollegeIDPaged(collegeID, HCRGUser.UID, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            else
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByCollegeIDPaged(collegeID, 0, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByCollegeIDPaged(collegeID, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            return Json(collegeeducationviewmodel, GlobalConst.Message.text_html);
        }
        public ActionResult GetEducationCollegeByEducationFormatID(int EducationFormatID, int? skip)
        {
            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            if (skip != null)
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDPaged(EducationFormatID, 0, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDPaged(EducationFormatID, HCRGUser.UID, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            else
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDPaged(EducationFormatID, 0, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDPaged(EducationFormatID, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            return Json(collegeeducationviewmodel, GlobalConst.Message.text_html);
        }
        public ActionResult GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfID(int? collegeID, int? EducationFormatID, int? professionID, int? skip)
        {
            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            if (skip != null)
            {
                if (professionID == null)
                {
                    if (HCRGUser == null)
                    {

                        var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDAndCollegeIDPaged(Convert.ToInt32(collegeID), Convert.ToInt32(EducationFormatID), 0, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                    else
                    {
                        var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDAndCollegeIDPaged(Convert.ToInt32(collegeID), Convert.ToInt32(EducationFormatID), HCRGUser.UID, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                }
                else
                {
                    if (HCRGUser == null)
                    {
                        var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(collegeID, EducationFormatID, professionID, null, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                    else
                    {
                        var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(collegeID, EducationFormatID, professionID, HCRGUser.UID, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                }

            }
            else
            {
                if (professionID == null)
                {
                    if (HCRGUser == null)
                    {
                        var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDAndCollegeIDPaged(Convert.ToInt32(collegeID), Convert.ToInt32(EducationFormatID), 0, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                    else
                    {
                        var getCollegeEducation = _educationService.GetEducationCollegeByEducationFormatIDAndCollegeIDPaged(Convert.ToInt32(collegeID), Convert.ToInt32(EducationFormatID), HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                }
                else
                {
                    if (HCRGUser == null)
                    {
                        var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(collegeID, EducationFormatID, professionID, null, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                    else
                    {
                        var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(collegeID, EducationFormatID, professionID, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
                        collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                        collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                    }
                }


            }
            return Json(collegeeducationviewmodel, GlobalConst.Message.text_html);
        }
        public ActionResult GetAllEducation(int? skip)
        {
            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            int _skipValue = 0;
            if (skip != null)
                _skipValue = skip.Value;
            if (HCRGUser == null)
            {
                var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, null, null, null, _skipValue, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
            }
            else
            {
                var getCollegeEducation = _educationService.GetEducationByEducationFormatIDORCollegeIDORDeptIDORPrfIDPaged(null, null, null, HCRGUser.UID, _skipValue, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
            }
            return Json(collegeeducationviewmodel, GlobalConst.Message.text_html);
        }
        public ActionResult GetEducationByProfessionID(int professionID, int? skip)
        {
            CollegeEducationViewModel collegeeducationviewmodel = new CollegeEducationViewModel();
            if (skip != null)
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationByProfessionIDPaged(professionID, 0, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationByProfessionIDPaged(professionID, HCRGUser.UID, skip.Value, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            else
            {
                if (HCRGUser == null)
                {
                    var getCollegeEducation = _educationService.GetEducationByProfessionIDPaged(professionID, 0, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
                else
                {
                    var getCollegeEducation = _educationService.GetEducationByProfessionIDPaged(professionID, HCRGUser.UID, GlobalConst.Records.Skip, GlobalConst.Records.LandingTake, HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"]))));
                    collegeeducationviewmodel.EducationDetailResults = Mapper.Map<IEnumerable<EducationDetail>>(getCollegeEducation.educationsDetail);
                    collegeeducationviewmodel.TotalCount = getCollegeEducation.TotalCount;
                }
            }
            return Json(collegeeducationviewmodel, GlobalConst.Message.text_html);
        }

    }
}
