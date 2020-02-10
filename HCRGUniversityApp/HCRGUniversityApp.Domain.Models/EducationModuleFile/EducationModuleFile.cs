using System;

namespace HCRGUniversityApp.Domain.Models.EducationModuleFile
{
    public class EducationModuleFile
    {
        public int EducationModuleFileID { get; set; }
        public int EducationModuleID { get; set; }
        public string ModuleFile { get; set; }
        public string ModuleFileExtension { get; set; }
        public int FileTypeID { get; set; }
        public string DocumentName { get; set; }
        public DateTime? DocumentUploadedDate { get; set; }
        public int UserID { get; set; }
        public string EnCryptMEMID { get; set; }
    }
}
