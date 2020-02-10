
namespace HCRGUniversityMgtApp.Domain.Models.ExamModel
{
    public class Exam : Base.BaseColumn
    {
        public int ExamID { get; set; }
        public string ExamName { get; set; }
        public bool ExamStatus { get; set; }
        public string EncryptedExamID { get; set; }
        public string OrganizationName { get; set; }
    }
}
