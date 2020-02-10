using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.LogSessionModel
{
    public class PagedLogSessionDetail
    {
        public IEnumerable<LogSessionDetail> LogSessionDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
