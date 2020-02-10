using HCRGUniversityMgtApp.Domain.Models.EducationFormatModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EducationDetailModel
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
        public string CourseTitle { get; set; }
        public System.DateTime CourseUploadDate { get; set; }  
        public int EducationID { get; set; }
        public bool IsActive { get; set; }
        public string[] EducationFormatImagePath { get; set; }
        public IEnumerable<EducationFormat> EducationFormat { get; set; }
        public int CollegeCourseID { get; set; }      
        public bool flag { get; set; }
       


    }
}
