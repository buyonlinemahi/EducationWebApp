
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
    public class ExamQuestionDetail
    {
        public int EducationID { get; set; }
        public int ExamID { get; set; }
        public int ExamQuestionID { get; set; }
        public string ExamQues { get; set; }
        public string ExamOptionA { get; set; }
        public string ExamOptionB { get; set; }
        public string ExamOptionC { get; set; }
        public string ExamOptionD { get; set; }
        public string ExamAnswer { get; set; }
        public string ExamAnswerType { get; set; }
        public bool? ExamAnswerTrueFalse { get; set; }
    }
}
