using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.AccreditorModel
{
    public class PagedAccreditor
    {
        public IEnumerable<Accreditor> AccreditorRecords { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
