using HCRGUniversityApp.Domain.Models.CollegeModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.CollegeViewModel
{
    public class CollegeViewModel
    {
        public IEnumerable<College> CollegeResults { get; set; }
    }
}
