
namespace HCRGUniversityMgtApp.Domain.Models.EvaluationModel
{
  public class EducationEvaluation
    {
        public int CourseEvaluationID { get; set; }
        public int EvaluationID { get; set; }
        public int EducationID { get; set; }
        public bool? IsActive { get; set; }
    }
}
