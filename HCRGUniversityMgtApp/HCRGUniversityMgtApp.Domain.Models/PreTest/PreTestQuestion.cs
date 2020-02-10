
namespace HCRGUniversityMgtApp.Domain.Models.PreTestModel
{
    public class PreTestQuestion
    {
        public int PreTestQuestionID { get; set; }
        public int PreTestID { get; set; }
        public string PreTestQues { get; set; }
        public string PreTestOptionA { get; set; }
        public string PreTestOptionB { get; set; }
        public string PreTestOptionC { get; set; }
        public string PreTestOptionD { get; set; }
        public string PreTestAnswer { get; set; }
        public string PreTestAnswerType { get; set; }
        public bool? PreTestAnswerTrueFalse { get; set; }
        public bool flag { get; set; }
    }
}
