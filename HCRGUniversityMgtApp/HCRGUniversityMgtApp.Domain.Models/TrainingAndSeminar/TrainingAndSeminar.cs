
namespace HCRGUniversityMgtApp.Domain.Models.TrainingAndSeminar
{
    public class TrainingAndSeminar : Base.BaseColumn
    {
        public int TrainingAndSeminarID { get; set; }
        public string TrainingAndSeminarDesc { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }     
        public string OrganizationName { get; set; }
    }
}
