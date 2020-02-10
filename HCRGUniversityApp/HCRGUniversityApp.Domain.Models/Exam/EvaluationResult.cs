
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
   public class EvaluationResult
    {
        public int UID { get; set; }
        public int EvaluationResultID { get; set; }
        public bool IsPass { get; set; }
        public int MEID { get; set; }
        public int EvaluationID { get; set; }
        public string Comments { get; set; }
        public string Suggestions { get; set; }
    }
}
