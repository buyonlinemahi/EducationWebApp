using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Enterprise
{
    public class PagedEnterprise
    {
        public IEnumerable<Enterprise> EnterpriseDetails { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
