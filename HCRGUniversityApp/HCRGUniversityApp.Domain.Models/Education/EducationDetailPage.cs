using System;

namespace HCRGUniversityApp.Domain.Models.EducationModel
{
    public class EducationDetailPage
    {
        public int EPageID { get; set; }
        public int EducationID { get; set; }
        public string PContent { get; set; }
        public DateTime PDate { get; set; }
    }
}
