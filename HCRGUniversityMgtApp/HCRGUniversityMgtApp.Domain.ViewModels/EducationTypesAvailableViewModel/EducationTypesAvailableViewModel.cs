using HCRGUniversityMgtApp.Domain.Models.EducationTypesAvailableModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationTypesAvailableViewModel
{
   public  class EducationTypesAvailableViewModel
    {
       public IEnumerable<EducationTypesAvailable> EducationTypesAvailableResults { get; set; }
    }
}
