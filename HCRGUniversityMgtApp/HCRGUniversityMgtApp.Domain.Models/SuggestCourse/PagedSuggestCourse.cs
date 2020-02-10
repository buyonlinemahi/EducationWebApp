using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.SuggestCourse
{
    public class PagedSuggestCourse
    {
        public IEnumerable<SuggestCourse> SuggestCoursesDetails { get; set; }
        public int PagedRecords { get; set; }
        public int TotalCount { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
