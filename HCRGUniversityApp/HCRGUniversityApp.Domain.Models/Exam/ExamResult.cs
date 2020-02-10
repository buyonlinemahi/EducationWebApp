
namespace HCRGUniversityApp.Domain.Models.ExamModel
{
    public class ExamResult
    {
        public int ExamResultID { get; set; }
        public int UID { get; set; }
        public bool IsPass { get; set; }
        public int MEID { get; set; }
        public int ExamID { get; set; }
    }
}
