using HCRGUniversityMgtApp.Domain.Models.Common;
using HCRGUniversityMgtApp.Domain.Models.Enterprise;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EnterpriseViewModel
{
    public class EnterpriseViewModel
    {
        public Enterprise Enterprise { get; set; }

        public IEnumerable<State> StateResults { get; set; }
    }
}
