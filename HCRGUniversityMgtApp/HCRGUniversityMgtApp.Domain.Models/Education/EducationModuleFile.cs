using System;
using System.Web;

namespace HCRGUniversityMgtApp.Domain.Models.EducationModel
{
    public class EducationModuleFile
    {
        public int EducationModuleFileID { get; set; }
        public string EncryptedEDUModuleID { get; set; }
        public int EducationModuleID { get; set; }
        public string ModuleFile { get; set; }
        public int FileTypeID { get; set; }       
        public HttpPostedFileBase EducationModulePPT { get; set; }
        public HttpPostedFileBase EducationModuleVideo { get; set; }
        public string EducationModuleText { get; set; }
        public string EducationModulePPTName { get; set; }
        public string EducationModuleVideoName { get; set; }
        public int CountDirectoryFiles { get; set; }
        public string DocumentName { get; set; }
        public DateTime? DocumentUploadedDate { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string UploadFilePath { get; set; }
        public string ModuleUploadFilePath { get; set; }
        public string ModuleFileExtension { get; set; }
    }

   
}
