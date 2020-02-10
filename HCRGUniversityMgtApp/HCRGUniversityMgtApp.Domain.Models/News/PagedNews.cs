using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.NewsModel
{
    public class PagedNews
    {
        public IEnumerable<News> NewsRecords { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
