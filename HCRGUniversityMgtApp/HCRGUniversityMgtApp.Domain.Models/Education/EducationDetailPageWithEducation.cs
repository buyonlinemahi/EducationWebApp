using System;

namespace HCRGUniversityMgtApp.Domain.Models.EducationModel
{
    public class EducationDetailPageWithEducation
    {
        public int EPageID { get; set; }
        public int EducationID { get; set; }
        public string PContent { get; set; }
        public string CourseName { get; set; }
        public DateTime PDate { get; set; }
    }
}
