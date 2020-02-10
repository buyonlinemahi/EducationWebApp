using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.TermsCondition;
namespace HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel
{
    public class TermConditionViewModel
    {
        public IEnumerable<TermsCondition> TermConditionsResults { get; set; }
    }
}
