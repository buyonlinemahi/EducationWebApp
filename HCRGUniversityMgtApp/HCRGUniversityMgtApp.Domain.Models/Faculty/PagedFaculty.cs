using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Faculty
{
    public class PagedFaculty
    {
        public IEnumerable<Faculty> FacultyDetails { get; set; }
        public int PagedRecords { get; set; }
        public int TotalCount { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
