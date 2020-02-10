using System;

namespace HCRGUniversityMgtApp.Domain.Models.PlanModel
{
    public class UserPlan
    {   
        public int UserPlanID { get; set; }   
        public int PlanID { get; set; }   
        public int ClientID { get; set; }  
        public int UserID { get; set; }
        public bool? IsPlanApplied { get; set; }
        public DateTime? PlanAppliedOn { get; set; }
        public int[] MultipleUserID { get; set; }
    }
}
