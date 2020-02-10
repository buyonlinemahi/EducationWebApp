using AutoMapper;
using CaptchaMvc.HtmlHelpers;
using HCRGUniversityApp.Domain.Models.FacultyModel;
using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityApp.Infrastructure.Base;
using HCRGUniversityApp.Infrastructure.Global;
using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace HCRGUniversityApp.Controllers
{
    public class CollegeController : BaseController
    {
        private readonly NEPService.CollegeService.ICollegeService _CollegeService;
        private readonly NEPService.EducationService.IEducationService _educationService;
        private readonly IStorage _storageService;
        public readonly IEncryption _encryptionService;
        public CollegeController(NEPService.CollegeService.ICollegeService collegeService, IStorage storageService, NEPService.EducationService.IEducationService educationService, IEncryption encryptionService)
        {
            _CollegeService = collegeService;
            _storageService = storageService;
            _educationService = educationService;
            _encryptionService = encryptionService;
        }
        //
        // GET: /College/
        [HttpGet]
        public ActionResult JoinFaculty()
        {
            Faculty obj = new Faculty();
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult JoinFaculty(Faculty Faculty)
        {
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                Faculty.Resume = UpdateDocumentFile(Faculty.ResumeFile, Faculty.FirstName + "_" + Faculty.LastName + DateTime.Now.ToString("ddMMyyyyss"));
                Faculty.ResumeFile = null;
                Faculty.OrganizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
                Faculty.FacultyID = _CollegeService.AddFaculty(Mapper.Map<HCRGUniversityApp.NEPService.CollegeService.Faculty>(Faculty));
                ViewBag.ErrMessage = "Success";
                Faculty = new Faculty();
                ModelState.Clear();
                ModelState.Remove("key");
                return View(new Faculty());
            }
            else
                ViewBag.ErrMessage = "Captcha Invalid.";
            return View(Faculty);
        }

        private string UpdateDocumentFile(HttpPostedFileBase file, string filename)
        {
            var _organizationID = HCRGUser != null ? HCRGUser.OrganizationID : (Convert.ToInt32(_encryptionService.DecryptString2(System.Configuration.ConfigurationManager.AppSettings["OrganizationID"])));
            var StoragePath = GlobalConst.Message.StoragePath + GlobalConst.ConstantChar.DoubleBackSlash + GlobalConst.FolderName.Org + _organizationID  + GlobalConst.ConstantChar.DoubleBackSlash;
            var path = _storageService.SetResumeUploadStoragePath(Server.MapPath(StoragePath), filename + Path.GetExtension(file.FileName));
            var fileName = Path.GetFileName(path);
            file.SaveAs(path);
            return fileName;
        }
    }
}
