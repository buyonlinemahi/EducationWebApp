using HCRGUniversityMgtApp.Domain.Models.CertificationTitleOption;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.CertificationTitleOptionViewModel
{
    public class CertificationTitleOptionViewModel
    {
        public PagedCertificationTitleOption PagedCertificationTitleOptionResults { get; set; }
        public IEnumerable<CertificationTitleOption> CertificationTitleOptionResults { get; set; }
    }
}
