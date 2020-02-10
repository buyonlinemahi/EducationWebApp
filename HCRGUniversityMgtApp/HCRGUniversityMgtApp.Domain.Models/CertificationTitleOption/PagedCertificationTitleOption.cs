using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption
{
    public class PagedCertificationTitleOption
    {
        public IEnumerable<CertificationTitleOption> CertificationTitleOptionRecords { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
