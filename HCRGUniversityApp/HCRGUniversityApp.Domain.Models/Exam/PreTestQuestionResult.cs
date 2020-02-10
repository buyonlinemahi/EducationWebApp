
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
    public class PreTestQuestionResult
    {
        public string PreTestAnswer { get; set; }
        public int PreTestQuestionID { get; set; }
        public int PreTestQuestionResultID { get; set; }
        public int PreTestResultID { get; set; }
        public bool ?PreTestAnswerTrueFalse { get; set; }
        public string PreTestAnswerType { get; set; }
    }
}
