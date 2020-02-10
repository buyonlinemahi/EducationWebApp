
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
    public class ExamResultCard
    {
        public int MEID { get; set; }
        public int EducationID { get; set; }
        public string EncryptedMEID { get; set; }
        public string EncryptedEducationID { get; set; }
        public decimal TotalPercentage { get; set; }
        public int totalAttemptByUser { get; set; }
        public bool IsEvalutionAvailable { get; set; }
    }
}
