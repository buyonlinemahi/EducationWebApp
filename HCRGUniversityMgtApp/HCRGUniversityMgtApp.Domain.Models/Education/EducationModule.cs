using System;
using System.Collections.Generic;
using System.Web;

namespace HCRGUniversityMgtApp.Domain.Models.EducationModel
{
    public class EducationModule
    {
        public int EducationModuleID { get; set; }
        public int EducationID { get; set; }
        public string EncryptedEDUModuleID { get; set; }
        public string EducationModuleName { get; set; }
        public string EducationModuleTime { get; set; }
        public int EducationModulePosition { get; set; }
        public DateTime EducationModuleDate { get; set; }
        public string EducationModuleDescription { get; set; }
        public string EducationModuleShortDesc { get; set; }
        public string EducationModuleVideoName { get; set; }
        public string EducationModulePDFName { get; set; }
        public string EducationModulePPTName { get; set; }
        public int EducationModuleTypeFile { get; set; }
        public HttpPostedFileBase EducationModuleVideo { get; set; }
        public HttpPostedFileBase EducationModulePDF { get; set; }
        public HttpPostedFileBase EducationModulePPT { get; set; }
        public IEnumerable<UploadDocPath> EducationModuleFileID { get; set; }       
        public string EducationModuleText { get; set; }      
        public bool flag { get; set; }
        public string UploadFile { get; set; }
        public string ModuleFile { get; set; }
        public string UploadFilePath { get; set; }
        public int FileTypeID { get; set; }
      
        public IEnumerable<EducationModuleFile> EducationModuleFileResults { get; set; }

    }

    public class EducationModule1
    {
        public int EducationModuleID { get; set; }
        public int EducationID { get; set; }
        public string EducationModuleName { get; set; }
        public string EducationModuleTime { get; set; }
        public int EducationModulePosition { get; set; }
        public DateTime EducationModuleDate { get; set; }
        public string EducationModuleDescription { get; set; }
        public string EducationModuleShortDesc { get; set; }
        public string EducationModuleVideoName { get; set; }
        public string EducationModulePDFName { get; set; }
        public string EducationModulePPTName { get; set; }
        public int EducationModuleTypeFile { get; set; }      
        public bool flag { get; set; }
        public bool? ReadyToDisplay { get; set; }
        public string EncryptedEducationID { get; set; }
    }

    public class UploadDocPath
    {
        public int EducationModuleFileID { get; set; }
    }

}
