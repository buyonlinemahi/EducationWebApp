
namespace HCRGUniversityMgtApp.Domain.Models.UserModel
{
    public class ClientAndUserbySearchCriterias
    {
        public int ID { get; set; }
        public int OrganizationID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string ClientTypeName { get; set; }
        public string OrganizationName { get; set; }
        public bool? IsActive { get; set; }
        public string Status { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
