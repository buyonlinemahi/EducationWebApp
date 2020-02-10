using HCRGUniversityMgtApp.Domain.Models.PlanModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.PlanViewModel
{
    public class PlanViewModel
    {
        public IEnumerable<Plan> PlanResults { get; set; }
        public IEnumerable<PlanPackages> UserPlanRecords { get; set; }
        public IEnumerable<PlanPackages> CoursePlanRecords { get; set; }
        public int PlanID { get; set; }
        public int ClientID { get; set; }
        public bool IsPlanAppliedCheck { get; set; }
        public int ClientTypeID { get; set; }
    }
}
