using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister
{
    public class PagedEnterprisePackageRegister
    {
        public IEnumerable<EnterprisePackageRegister> EnterprisePackageRegisterDetails { get; set; }
        public int PagedRecords { get; set; }
        public int TotalCount { get; set; }
    }
}
