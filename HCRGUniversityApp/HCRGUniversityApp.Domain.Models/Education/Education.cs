using System;

namespace HCRGUniversityApp.Domain.Models.EducationModel
{
    public class Education:Base.BaseModel
    {
        public int EducationID { get; set; }
        public string EncrptedEducationID { get; set; }
        public string CourseName { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public string CourseTime  { get; set; }
        public bool CourseCredential { get; set; }
        public DateTime CourseUploadDate { get; set; }
        public decimal CoursePrice { get; set; }
        public bool IsActive { get; set; }
        public bool flag { get; set; }
        public string CourseFile { get; set; }
        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }     
        public int? CourseAllotedTime { get; set; }
        public string CouseAllotedDaysMonth { get; set; }
        public bool? ReadyToDisplay { get; set; }
        public bool? CanPurchase { get; set; }
    }
}
