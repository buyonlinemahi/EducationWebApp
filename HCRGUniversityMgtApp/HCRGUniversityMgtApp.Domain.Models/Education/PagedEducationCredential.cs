using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EducationCredential
{
    public class PagedEducationCredential
    {
        public IEnumerable<EducationCredential> EducationCredentials { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
