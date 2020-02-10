
using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.IndustryResearchModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.IndustryResearchViewModel
{
    public class IndustryResearchViewModel
    {
        public IEnumerable<IndustryResearch> IndustryResearchResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
