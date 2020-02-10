using AutoMapper;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Base;
using HCRGUniversityMgtApp.Infrastructure.Global;
using HCRGUniversityMgtApp.NEPService.CommonService;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Model = HCRGUniversityMgtApp.Domain.Models.SuggestCourse;

namespace HCRGUniversityMgtApp.Controllers
{
    public class CommonController :  BaseController
    {
        private readonly ICommonService _commonService;
        private readonly IStorage _storageService;

        public CommonController(ICommonService commonService, IStorage storageService)
        {
            _commonService = commonService;
            _storageService = storageService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public bool IsPdf()
        {
            var pdfString = "%PDF-";
            var pdfBytes = System.Text.Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            Stream strFile = Request.Files[0].InputStream;

            while (remaining > 0)
            {
                var amtRead = strFile.Read(buf, pos, remaining);
                if (amtRead == 0) return false;
                remaining -= amtRead;
                pos += amtRead;
            }
            return pdfBytes.SequenceEqual(buf);
        }

        [HttpGet]
        public JsonResult getAllState()
        {
            return Json(_commonService.getAllState(), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult getAllIndustry()
        {
            return Json(_commonService.getAllIndustry(), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getAllMenu()
        {
            return Json(_commonService.getAllMenu(), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult getAllClientType()
        {
           return Json(_commonService.getAllClientType(), GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult getAllClientTypeInSearch()
        {
            var _getAllClientType = _commonService.getAllClientType();
            if (HCRGCLIENT.ClientTypeID == 3 || HCRGCLIENT.ClientTypeID == 4)
            {
                var _result = _getAllClientType.Where(mahi => mahi.ClientTypeID != 1 && mahi.ClientTypeID != 2 && mahi.ClientTypeID != 3).Select(mahi => new { mahi.ClientTypeID, mahi.ClientTypeName }).Distinct();
                return Json(_result, GlobalConst.Message.text_html, JsonRequestBehavior.AllowGet);
            }
            else if (HCRGCLIENT.ClientTypeID == 1)
                return Json(_commonService.getAllClientType(), GlobalConst.Message.text_html);
            else
            {
                var _result = _getAllClientType.Where(mahi => mahi.ClientTypeID != 1).Select(mahi => new { mahi.ClientTypeID, mahi.ClientTypeName }).Distinct();
                return Json(_result, GlobalConst.Message.text_html);
            }
        }
        [HttpPost]
        public JsonResult getAllClientTypeWithoutHcrgAdmin()
        {
            
            var _getAllClientType = _commonService.getAllClientType();
            if (HCRGCLIENT.ClientTypeID == 3 || HCRGCLIENT.ClientTypeID == 4)
            {
                var _result = _getAllClientType.Where(mahi => mahi.ClientTypeID != 1 && mahi.ClientTypeID != 2 && mahi.ClientTypeID != 3).Select(mahi => new { mahi.ClientTypeID, mahi.ClientTypeName }).Distinct();
               return Json(_result, GlobalConst.Message.text_html);
            }
            else
            {
               var  _result = _getAllClientType.Where(mahi => mahi.ClientTypeID != 1).Select(mahi => new { mahi.ClientTypeID, mahi.ClientTypeName }).Distinct();
               return Json(_result, GlobalConst.Message.text_html);
            }
           
        }

        public ActionResult ErrorViewer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetFiles()
        {
            string VirtualStoragePath = _storageService.getVirtualPath();
            string path = VirtualStoragePath + "/" + GlobalConst.MgmtDirectoryStructure.ErrorLogMgt + "/";         

            string dirPath = path;
            List<string> files = new List<string>();
            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
            foreach (FileInfo fInfo in dirInfo.GetFiles())
            {
                files.Add(fInfo.Name);
            }
            
            return Json(files.ToArray());
        }

        [HttpPost]
        public JsonResult getSuggestCourseByOrganizationID(int orgID) 
        {
            var data = Mapper.Map<IEnumerable<Model.SuggestCourse>>(_commonService.GetAllSuggestCoursesByOrganizationID(HCRGCLIENT.ClientID,orgID));
            return Json(data);
        }

        #region SuggestCourse
        public ActionResult getAllSuggestCourses(int? skip)
        {
            int _organizationID = 0;
            if (HCRGCLIENT.ClientTypeID == 1)
                _organizationID = 0;
            else
                _organizationID = HCRGCLIENT.OrganizationID;
            Model.PagedSuggestCourse pagedSuggestCourseModel = new Model.PagedSuggestCourse();
            if (HCRGCLIENT.ClientTypeID == 1)
                pagedSuggestCourseModel.IsHCRGAdmin = true;
            else
                pagedSuggestCourseModel.IsHCRGAdmin = false;
            pagedSuggestCourseModel.SuggestCoursesDetails = Mapper.Map<IEnumerable<Model.SuggestCourse>>(_commonService.GetAllSuggestCoursesByOrganizationID(HCRGCLIENT.ClientID,_organizationID));
            return View(pagedSuggestCourseModel);
        }

        [HttpPost]
        public ActionResult GetSuggestCourseByID(int ID)
        {
            var SuggestCourseModel = Mapper.Map<Model.SuggestCourse>(_commonService.getSuggestCourseByID(ID));           
            var stateNameList = _commonService.getAllState();
            var stateName = stateNameList.Where(st => st.StateID == SuggestCourseModel.SCStateID).Select(st => new { st.StateName}).SingleOrDefault();
            string _st = stateName.StateName;
            SuggestCourseModel.SCStateName = _st;
            return Json(SuggestCourseModel);
        }
        #endregion
   
        public ActionResult Unauthorized()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }


        
    }
}