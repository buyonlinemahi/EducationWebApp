using HCRGUniversityMgtApp.Domain.Models.CollegeEducationSearchModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.CollegeEducationSearchViewModel
{
    public class CollegeEducationSearchViewModel
    {
       public IEnumerable<CollegeEducationSearch> CollegeEducationSearchResults { get; set; }
    }
}
