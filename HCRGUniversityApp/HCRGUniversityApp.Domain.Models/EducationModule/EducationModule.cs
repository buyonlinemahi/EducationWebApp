using System;

namespace HCRGUniversityApp.Domain.Models.EducationModuleModel
{
    public class EducationModule
    {
        public int EducationModuleID { get; set; }
        public int EducationID { get; set; }
        public string EducationModuleName { get; set; }
        public string EducationModuleDescription { get; set; }
        public string EducationModuleTime { get; set; }
        public DateTime EducationModuleDate { get; set; }
        public int EducationModulePosition { get; set; }
       
    }
}
