
namespace HCRGUniversityMgtApp.Domain.Models.EvaluationModel
{
    public class EvaluationQuestion
    {
        public int EvaluationQuestionID { get; set; }
        public int EvaluationID { get; set; }
        public string EvaluationQues { get; set; }
        public bool flag { get; set; }
        public bool IsStatus { get; set; }
    }
}
