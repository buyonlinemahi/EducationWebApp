
namespace HCRGUniversityMgtApp.Domain.Models.PlanModel
{
    public class Plan
    {
        public int PlanID { get; set; }
        public string PlanName { get; set; }
        public int ClientID { get; set; }
        public string  EncryptedPlanID { get; set; }       
    }
}
