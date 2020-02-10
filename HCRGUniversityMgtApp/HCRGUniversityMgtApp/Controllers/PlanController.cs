using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.Plan;
using HCRGUniversityMgtApp.Domain.Models.PlanModel;
using HCRGUniversityMgtApp.Domain.ViewModels.PlanViewModel;
using HCRGUniversityMgtApp.Infrastructure.ApplicationFilters;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace HCRGUniversityMgtApp.Controllers
{
    [AuthorizedUserCheck]
    public class PlanController : BaseController
    {
        private readonly NEPService.PlanService.IPlanService _planService;
        public readonly IEncryption _encryptionService;

        public PlanController(NEPService.PlanService.IPlanService planService, IEncryption encryptionService)
        {
            _planService = planService;
            _encryptionService = encryptionService;
        }
        public ActionResult Index()
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            PagedPlanGrid objPagedplan = new PagedPlanGrid();
            if (HCRGCLIENT.ClientTypeID == 1)
                objPagedplan.IsHCRGAdmin = true;
            else
                objPagedplan.IsHCRGAdmin = false;
            var objPlanList = _planService.GetAllPagedPlanByClientID(HCRGCLIENT.ClientID, _organizationID, GlobalConst.Records.Skip, GlobalConst.Records.Take);
            objPagedplan.PlanRecords = Mapper.Map<IEnumerable<PlanGrid>>(objPlanList.PlanRecords);
            foreach (var objorganizationResult in objPagedplan.PlanRecords)
                objorganizationResult.EncryptedPlanID = EncryptString(objorganizationResult.PlanID.ToString());
            objPagedplan.TotalCount = objPlanList.TotalCount;
            return View(objPagedplan);
        }

        [HttpGet]
        public JsonResult GetAllPlans()
        {
            return Json(_planService.GetAllPlansByClientID(HCRGCLIENT.ClientID), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetAllPlanByClientID(int? skip, int? orgID)
        {
            if (HCRGCLIENT.ClientTypeID == 1)
            {
                if (orgID == null)
                    orgID = 0;
            }
            else
                orgID = HCRGCLIENT.OrganizationID;

            PagedPlanGrid objPagedplan = new PagedPlanGrid();
            var objPlanList = _planService.GetAllPagedPlanByClientID(HCRGCLIENT.ClientID, orgID.Value, skip.Value, GlobalConst.Records.Take);
            objPagedplan.PlanRecords = Mapper.Map<IEnumerable<PlanGrid>>(objPlanList.PlanRecords);
            foreach (var objorganizationResult in objPagedplan.PlanRecords)
                objorganizationResult.EncryptedPlanID = EncryptString(objorganizationResult.PlanID.ToString());
            objPagedplan.TotalCount = objPlanList.TotalCount;
            return Json(objPagedplan, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult AddPlan(Plan objPlan)
        {
            string msg;
            if (objPlan.EncryptedPlanID != null)
            {
                objPlan.PlanID = Convert.ToInt32(DecryptString(objPlan.EncryptedPlanID));
                objPlan.PlanID = _planService.UpdatePlan(Mapper.Map<HCRGUniversityMgtApp.NEPService.PlanService.Plan>(objPlan));
                msg = GlobalConst.Message.UpdateSucessfully;
            }
            else
            {
                objPlan.ClientID = HCRGCLIENT.ClientID;
                objPlan.PlanID = _planService.AddPlan(Mapper.Map<HCRGUniversityMgtApp.NEPService.PlanService.Plan>(objPlan));
                msg = GlobalConst.Message.AddSucessfully;
            }
            return Json(msg, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public ActionResult AddPackage(string id)
        {
            
            PlanViewModel objplanViewModel = new PlanViewModel();
            objplanViewModel.ClientTypeID = HCRGCLIENT.ClientTypeID;

            if (id == null)
            {
                objplanViewModel.UserPlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(_planService.GetAllUsersPlan());
                objplanViewModel.CoursePlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(_planService.GetAllCoursesPlan());
            }
            else
            {
                int _ID = Convert.ToInt16(DecryptString(id.ToString()));
                var _plan = _planService.GetPlanById(_ID);
                var getUsersPlanAccToPlan = _planService.GetUsersPlanAccToPlanID(_ID);
                var _isApplied = getUsersPlanAccToPlan.Where(mahi => mahi.IsPlanApplied == true).Select(mahi => new { mahi.IsPlanApplied }).FirstOrDefault();
                if (_isApplied == null)
                    objplanViewModel.IsPlanAppliedCheck = false;
                else
                    objplanViewModel.IsPlanAppliedCheck = Convert.ToBoolean(_isApplied.IsPlanApplied);
                objplanViewModel.UserPlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(getUsersPlanAccToPlan);
                objplanViewModel.CoursePlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(_planService.GetCoursesPlanAccToPlanID(_ID));
                foreach (PlanPackages obj in objplanViewModel.CoursePlanRecords)
                    obj.IsPlanApplied = _isApplied == null ? false : _isApplied.IsPlanApplied;
                objplanViewModel.PlanID = _ID;
                var _clientID = getUsersPlanAccToPlan.Where(mahi => mahi.IsPlanApplied == true).Select(mahi => new { mahi.ClientID }).FirstOrDefault();
                objplanViewModel.ClientID = _plan.ClientID;
            }
            return View(objplanViewModel);
        }
        [HttpPost]
        public ActionResult GetUserAndCourseAccordingToPlanID(int _planID)
        {
            PlanViewModel objplanViewModel = new PlanViewModel();
            var getUsersPlanAccToPlan = _planService.GetUsersPlanAccToPlanID(_planID);
            var _isApplied = getUsersPlanAccToPlan.Where(mahi => mahi.IsPlanApplied == true).Select(mahi => new { mahi.IsPlanApplied }).FirstOrDefault();
            if (_isApplied == null)
                objplanViewModel.IsPlanAppliedCheck = false;
            else
                objplanViewModel.IsPlanAppliedCheck = Convert.ToBoolean(_isApplied.IsPlanApplied);
            objplanViewModel.UserPlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(getUsersPlanAccToPlan);
            objplanViewModel.CoursePlanRecords = Mapper.Map<IEnumerable<PlanPackages>>(_planService.GetCoursesPlanAccToPlanID(_planID));
            foreach (PlanPackages obj in objplanViewModel.CoursePlanRecords)
                obj.IsPlanApplied = _isApplied == null ? false : _isApplied.IsPlanApplied;
            objplanViewModel.PlanID = _planID;
            return Json(objplanViewModel);
        }

        [HttpPost]
        public ActionResult GetPlanAccordingToOrganization(int OrganizationID, int? skip)
        {
            PagedPlanGrid objPagedplan = new PagedPlanGrid();
            var objPlanList = _planService.GetAllPagedPlanByClientID(HCRGCLIENT.ClientID, OrganizationID, skip.Value, GlobalConst.Records.Take);
            objPagedplan.PlanRecords = Mapper.Map<IEnumerable<PlanGrid>>(objPlanList.PlanRecords);
            foreach (var objorganizationResult in objPagedplan.PlanRecords)
                objorganizationResult.EncryptedPlanID = EncryptString(objorganizationResult.PlanID.ToString());
            objPagedplan.TotalCount = objPlanList.TotalCount;
            return Json(objPagedplan);
        }

        [HttpPost]
        public ActionResult AddUserPlan(UserPlan _userplan)
        {
            string msg;
            foreach (var objorganizationResult in _userplan.MultipleUserID)
            {
                _userplan.IsPlanApplied = false;
                _userplan.UserID = objorganizationResult;
                _userplan.UserPlanID = _planService.AddUserPlan(Mapper.Map<HCRGUniversityMgtApp.NEPService.PlanService.UserPlan>(_userplan));
            }
            if (_userplan.UserPlanID != 0)
                msg = GlobalConst.Message.UserPlanAdded;
            else
                msg = GlobalConst.Message.UserPlanExist;
            return Json(msg, GlobalConst.Message.text_html);
        }


        [HttpPost]
        public ActionResult AddCourcePlan(CoursePlan _coursePlan)
        {
            string msg;
            foreach (var objorganizationResult in _coursePlan.MultipleCourseID)
            {
                _coursePlan.CourseID = objorganizationResult;
                _coursePlan.CoursePlanID = _planService.AddCoursePlan(Mapper.Map<HCRGUniversityMgtApp.NEPService.PlanService.CoursePlan>(_coursePlan));
            }
            if (_coursePlan.CoursePlanID != 0)
                msg = GlobalConst.Message.CourcePlanAdded;
            else
                msg = GlobalConst.Message.CourcePlanExist;
            return Json(msg, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public ActionResult DeleteUserByUsersPlanID(int userPlanID)
        {
            try
            {
                _planService.DeleteUserPlan(userPlanID);
                return Json("Deleted Successfully");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public ActionResult DeleteCoursesByCoursePlanID(int coursesPlanID)
        {
            try
            {
                _planService.DeleteCoursePlan(coursesPlanID);
                return Json("Deleted Successfully");
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

        [HttpPost]
        public ActionResult AppliedPackageMyEducationRecordByPlanID(int PlanID)
        {
            try
            {
                int resut = _planService.AddMyEducationRecordByPlanID(PlanID);
                return Json(resut);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }
        }

    }
}