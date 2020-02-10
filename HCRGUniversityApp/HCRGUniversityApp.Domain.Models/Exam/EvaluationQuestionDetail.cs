
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
    public class EvaluationQuestionDetail
    {
        public int EvaluationQuestionID { get; set; }
        public int EvaluationID { get; set; }
        public string EvaluationQues { get; set; }
        public int CourseEvaluationID { get; set; }
        public int EducationID { get; set; }
        public string CourseName { get; set; }
    }
}
