
namespace HCRGUniversityMgtApp.Domain.Models.PlanModel
{
    public class PlanPackages
    {
        public int PlanID { get; set; }
        public int ClientID { get; set; }  
        public string PlanName { get; set; } 
        public int UserID { get; set; }  
        public string UserName { get; set; }   
        public int UserPlanID { get; set; }  
        public int CoursePlanID { get; set; }    
        public string CourseName { get; set; }    
        public int EducationID { get; set; }
        public bool? IsPlanApplied { get; set; }
    }
}
