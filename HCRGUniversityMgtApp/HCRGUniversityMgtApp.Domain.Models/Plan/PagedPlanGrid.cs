using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Plan
{
    public class PagedPlanGrid
    {
        public IEnumerable<PlanGrid> PlanRecords { get; set; }
        public int TotalCount { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
