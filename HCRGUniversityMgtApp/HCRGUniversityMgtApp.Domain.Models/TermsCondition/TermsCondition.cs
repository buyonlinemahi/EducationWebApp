
namespace HCRGUniversityMgtApp.Domain.Models.TermsCondition
{
    public class TermsCondition : Base.BaseColumn
    {
        public int TermsConditionID { get; set; }
        public string TermsConditionContent { get; set; }

        public string DescriptionShort { get; set; }

        public bool flag { get; set; }
    }
}
