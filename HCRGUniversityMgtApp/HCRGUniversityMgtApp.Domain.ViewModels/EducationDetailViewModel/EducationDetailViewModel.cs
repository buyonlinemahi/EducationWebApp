using HCRGUniversityMgtApp.Domain.Models.EducationDetailModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationDetailViewModel
{
   public class EducationDetailViewModel
    {
       public IEnumerable<EducationDetail> EducationDetailResults { get; set; }
    }
}
