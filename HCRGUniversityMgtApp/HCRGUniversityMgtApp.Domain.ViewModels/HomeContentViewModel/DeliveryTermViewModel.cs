using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.DeliveryTerm;

namespace HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel
{
    public class DeliveryTermViewModel
    {
        public IEnumerable<DeliveryTerm> DeliveryTermResults { get; set; }
    }
}
