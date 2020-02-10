using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.FAQModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.FAQViewModel
{
    public class FAQViewModel
    {
   
        public IEnumerable<FAQ> FAQAllResults { get; set; }
        public IEnumerable<FAQCategory> FAQCategoryResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
