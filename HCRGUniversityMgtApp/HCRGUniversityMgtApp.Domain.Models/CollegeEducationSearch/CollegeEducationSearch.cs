
namespace HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel
{
    public class CollegeEducationSearch
    {
        public int CollegeID { get; set; }
        public string CollegeName { get; set; }
        public int? CollegeCourseID { get; set; }
        public int? EducationID { get; set; }
        public bool IsActive { get; set; }
        public bool flag { get; set; }
    }
}