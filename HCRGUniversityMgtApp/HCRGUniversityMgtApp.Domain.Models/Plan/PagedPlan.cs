using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.PlanModel
{
    public class PagedPlan
    {
        public IEnumerable<Plan> PlanRecords { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
