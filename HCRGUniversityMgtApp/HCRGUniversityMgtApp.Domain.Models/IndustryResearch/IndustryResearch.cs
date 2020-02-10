
namespace HCRGUniversityMgtApp.Domain.Models.IndustryResearchModel
{
    public class IndustryResearch : Base.BaseColumn
    {
        public int IndustryResearchID { get; set; }
        public string IndustryResearchContent { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }
        public string OrganizationName { get; set; }
    }
}
