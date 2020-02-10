using AutoMapper;
using HCRGUniversityMgtApp.Domain.Models.CollegeModel;
using HCRGUniversityMgtApp.Domain.Models.Faculty;
using HCRGUniversityMgtApp.Domain.ViewModels.CollegeViewModel;
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
    public class CollegeController : BaseController
    {
        private readonly NEPService.CollegeService.ICollegeService _CollegeService;
        private readonly NEPService.ClientService.IClientService _clientService;
        private readonly IStorage _storageService;

        //
        // GET: /College/
        public CollegeController(NEPService.CollegeService.ICollegeService collegeService, IStorage storageService, NEPService.ClientService.IClientService clientService)
        {
            _CollegeService = collegeService;
            _storageService = storageService;
            _clientService = clientService;

        }
        public ActionResult Index()
        {
            // return View();
            CollegeViewModel collegeModel = new CollegeViewModel();
            collegeModel.CollegeResults = Mapper.Map<IEnumerable<College>>(_CollegeService.getAllCollege(HCRGCLIENT.ClientID));
            if (HCRGCLIENT.ClientTypeID == 1)
                collegeModel.IsHCRGAdmin = true;
            else
                collegeModel.IsHCRGAdmin = false;
            return View(collegeModel);
        }

        [HttpPost]
        public JsonResult GetAllCollegeByOrganizationID(int OrgID)
        {
            var data = Mapper.Map<IEnumerable<College>>(_CollegeService.GetAllCollegeByOrganizationID(OrgID));           
            return Json(data);
        }
        [HttpPost]
        public ActionResult Add(College college, string CollegeID)
        {
            if (CollegeID == "")
            {
                college.OrganizationName = (_clientService.GetOrganizationByID(HCRGCLIENT.OrganizationID)).OrganizationName;
                college.ClientID = HCRGCLIENT.ClientID;
                college.IsActive = true;
                college.flag = true;
                college.ClientID = HCRGCLIENT.ClientID;
                var collegeID = _CollegeService.AddCollege(Mapper.Map<HCRGUniversityMgtApp.NEPService.CollegeService.College>(college));
                college.CollegeID = collegeID;


            }
            else
            {              
                var collegeID = _CollegeService.UpdateCollege(Mapper.Map<HCRGUniversityMgtApp.NEPService.CollegeService.College>(college));
                college.CollegeID = Convert.ToInt32(CollegeID);
                college.flag = false;
            }
            return Json(college, GlobalConst.Message.text_html);
        }

        [HttpPost]
        public JsonResult DeleteCollege(College College)
        {
            if (College.IsActive == true)
                College.IsActive = false;
            else
                College.IsActive = true;
            _CollegeService.DeleteCollege(College.CollegeID, College.IsActive);
            return Json(College, GlobalConst.Message.text_html);
        }

        [HttpGet]
        public JsonResult getColleges()
        {
            CollegeViewModel collegeModel = new CollegeViewModel();
            collegeModel.CollegeResults = Mapper.Map<IEnumerable<College>>(_CollegeService.getAllCollegeActive(HCRGCLIENT.ClientID));
            return Json(collegeModel.CollegeResults, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }


        public ActionResult getAllFaculties(int? skip)
        {
            PagedFaculty pagedFacultyModel = new PagedFaculty();

            if (HCRGCLIENT.ClientTypeID == 1)
                pagedFacultyModel.IsHCRGAdmin = true;
            else
                pagedFacultyModel.IsHCRGAdmin = false;

            if (skip == null)
            {
                var facultyList = _CollegeService.GetAllPagedFaculty(GlobalConst.Records.Skip, GlobalConst.Records.Take15, 0, HCRGCLIENT.ClientID);
                pagedFacultyModel.FacultyDetails = Mapper.Map<IEnumerable<Faculty>>(facultyList.FacultyDetails);
                pagedFacultyModel.FacultyDetails.ToList().ForEach(
                faculty =>
                {
                    faculty.FullName = pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).FirstName + " " + pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).LastName;

                });
                pagedFacultyModel.TotalCount = facultyList.TotalCount;
                return View(pagedFacultyModel);
            }
            else
            {
                var facultyList = _CollegeService.GetAllPagedFaculty(skip.Value, GlobalConst.Records.Take15, 0, HCRGCLIENT.ClientID);
                pagedFacultyModel.FacultyDetails = Mapper.Map<IEnumerable<Faculty>>(facultyList.FacultyDetails);
                pagedFacultyModel.FacultyDetails.ToList().ForEach(
                faculty =>
                {
                    faculty.FullName = pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).FirstName + pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).LastName;

                });
                pagedFacultyModel.TotalCount = facultyList.TotalCount;
                return Json(pagedFacultyModel, GlobalConst.Message.text_html);
            }
        }


        [HttpPost]
        public JsonResult GetAllFacultiesByOrganizationID(int skip, int OrganizationID)
        {
            PagedFaculty pagedFacultyModel = new PagedFaculty();

            if (HCRGCLIENT.ClientTypeID == 1)
                pagedFacultyModel.IsHCRGAdmin = true;
            else
                pagedFacultyModel.IsHCRGAdmin = false;
            var facultyList = _CollegeService.GetAllPagedFaculty(skip, GlobalConst.Records.Take15, OrganizationID, HCRGCLIENT.ClientID);
            pagedFacultyModel.FacultyDetails = Mapper.Map<IEnumerable<Faculty>>(facultyList.FacultyDetails);
            pagedFacultyModel.FacultyDetails.ToList().ForEach(
            faculty =>
            {
                faculty.FullName = pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).FirstName + pagedFacultyModel.FacultyDetails.ToList().Find(faculties => faculties.FacultyID == faculty.FacultyID).LastName;

            });
            pagedFacultyModel.TotalCount = facultyList.TotalCount;
            return Json(pagedFacultyModel);
        }
        [HttpPost]
        public ActionResult GetFacultyByID(int ID)
        {
            var facultyModel = Mapper.Map<Faculty>(_CollegeService.GetFacultyById(ID));
            return Json(facultyModel);
        }

        public FileResult Download(int ID)
        {
            var facultyModel = Mapper.Map<Faculty>(_CollegeService.GetFacultyById(ID));

            facultyModel.ResumeLink = GlobalConst.FolderName.Storage + "/" + GlobalConst.FolderName.Org + facultyModel.OrganizationID + "/" + GlobalConst.MgmtDirectoryStructure.Resume + "/" + facultyModel.Resume;

            return File(facultyModel.ResumeLink, "application/word", facultyModel.Resume);
        }

    }
}
