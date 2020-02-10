
namespace HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel
{
    public class EducationProfessionDetail
    {
        public string CourseName { get; set; }
        public string ProfessionTitle { get; set; }
        public int EducationProfessionID { get; set; }
        public int EducationID { get; set; }
        public int ProfessionID { get; set; }
        public bool IsActive { get; set; }
    }
}
