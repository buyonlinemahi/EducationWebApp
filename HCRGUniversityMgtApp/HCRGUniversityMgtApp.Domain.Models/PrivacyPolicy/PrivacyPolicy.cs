
namespace HCRGUniversityMgtApp.Domain.Models.Privacy
{
    public class PrivacyPolicy 
    {
        public int PrivacyPolicyID { get; set; }
        public string PrivacyPolicyContent { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }
        public int OrganizationID { get; set; }
        public string OrganizationName { get; set; }
    }
}
