using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.Privacy;

namespace HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel
{
    public class PrivacyPolicyViewModel
    {
        public IEnumerable<PrivacyPolicy> PrivacyPolicyResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
