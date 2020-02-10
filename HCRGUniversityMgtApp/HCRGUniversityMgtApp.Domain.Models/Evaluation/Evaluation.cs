
namespace HCRGUniversityMgtApp.Domain.Models.EvaluationModel
{
 public class Evaluation : Base.BaseColumn
    {
        public int EvaluationID { get; set; }
        public string EvaluationName { get; set; }
        public bool EvaluationStatus { get; set; }
        public string EncryptedEvaluationID { get; set; }
        public string OrganizationName { get; set; }
    }
}
