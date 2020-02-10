using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EventModel
{
    public class PagedEvents
    {
        public IEnumerable<Event> _events { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
