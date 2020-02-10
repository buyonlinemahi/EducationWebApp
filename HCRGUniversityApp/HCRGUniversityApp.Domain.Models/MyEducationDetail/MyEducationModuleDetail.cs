using System;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.Models.MyEducationDetailModel
{
    public class MyEducationModuleDetail
    {
        public IEnumerable<EducationModuleFile.EducationModuleFile> EducationModuleFileDetail { get; set; }
        public int EducationModuleID { get; set; }
        public int EducationID { get; set; }
        public int EducationModulePosition { get; set; }
        public int MEMID { get; set; }
        public int MEID { get; set; }
        public string EnCryptMEID { get; set; }
        public string CourseName { get; set; }
        public string CourseFile { get; set; }
        public string CourseFilePath { get; set; }
        public string EducationModuleDescription { get; set; }
        public string EducationModuleName { get; set; }
        public bool Completed { get; set; }
        public DateTime? CompletedDate { get; set; }
        public string FileTypeName { get; set; }
        public string ModuleFile { get; set; }
        public string ModuleFileExtension { get; set; }
        public string percent { get; set; }
        public string TimeLeft { get; set; }
        public int MEMPositiontoStart { get; set; }
        public string FilePath { get; set; }
        public int CountDirectoryFiles { get; set; }
        public string EducationModuleTime { get; set; }
        public bool IsModuleInProcess { get; set; }
        public int? ExpireDaysLeft { get; set; }
        public bool? IsTimerRequired { get; set; }   
        
    }
}
