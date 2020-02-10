
namespace HCRGUniversityMgtApp.Domain.Models.PreTestModel
{
    public class PreTest : Base.BaseColumn
    {
        public int PreTestID { get; set; }
        public string PreTestName { get; set; }
        public bool PreTestStatus { get; set; }
        public string EncryptedPreTestID { get; set; }
        public string OrganizationName { get; set; }
    }
}
