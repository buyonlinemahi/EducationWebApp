
namespace HCRGUniversityMgtApp.Domain.Models.ExamModel
{
  public class EducationExamQuestion
    {
        public int CourseExamID { get; set; }
        public int ExamID { get; set; }
        public int EducationID { get; set; }
        public bool? IsActive { get; set; }
    }
}
