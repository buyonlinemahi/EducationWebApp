
namespace HCRGUniversityMgtApp.Domain.Models.ReturnPolicy
{
    public class ReturnPolicy : Base.BaseColumn
    {
        public int ReturnPolicyID { get; set; }
        public string ReturnPolicyContent { get; set; }
        public string DescriptionShort { get; set; }
        public bool flag { get; set; }
    }
}
