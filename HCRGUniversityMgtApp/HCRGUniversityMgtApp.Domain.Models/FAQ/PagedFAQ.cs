using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.FAQModel
{
    public class PagedFAQ
    {
        public IEnumerable<FAQ> FAQDetails { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
