using HCRGUniversityMgtApp.Domain.Models.CollegeModel;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.CollegeViewModel
{
    public class CollegeViewModel
    {
        public IEnumerable<College> CollegeResults { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
