using System;

namespace HCRGUniversityMgtApp.Domain.Models.EnterprisePackageRegister
{
    public class EnterprisePackageRegister : Base.BaseColumn
    {
        public int EPRID { get; set; }
        public string EPRName { get; set; }
        public string EPRPhone { get; set; }
        public string EPREmail { get; set; }
        public string EPRCompany { get; set; }
        public DateTime? EPRCreatedDate { get; set; }
    }
}
