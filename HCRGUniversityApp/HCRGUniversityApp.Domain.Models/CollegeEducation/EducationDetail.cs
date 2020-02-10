using HCRGUniversityApp.Domain.Models.EducationFormatModel;
using System;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.Models.CollegeEducationModel
{
    public class EducationDetail
    {
        public int CollegeID { get; set; }
        public string CollegeLogo { get; set; }
        public string CollegeName { get; set; }
        public string CourseCode { get; set; }
        public string CourseDescription { get; set; }
        public string CourseName { get; set; }
        public decimal CoursePrice { get; set; }
        public string CourseTime { get; set; }
        public System.DateTime CourseUploadDate { get; set; }
        public int EducationID { get; set; }
        public bool IsActive { get; set; }
        public string[]  EducationFormatImagePath { get; set; }
        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }
        public int? CourseAllotedTime { get; set; }
        public string CouseAllotedDaysMonth { get; set; }
        public IEnumerable<EducationFormat> EducationFormat { get; set; }
        public int Count { get; set; }

    }
}
