
namespace HCRGUniversityMgtApp.Domain.Models.Plan
{
    public class PlanGrid
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public int ClientID { get; set; }
        public string OrganizationName { get; set; }
        public int OrganizationID { get; set; }
        public string EncryptedPlanID { get; set; }       
    }
}
