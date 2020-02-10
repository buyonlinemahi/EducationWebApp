using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.PreTestModel
{
    public class PagedPreTest
    {
        public IEnumerable<PreTest> PreTests { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
