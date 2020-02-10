using HCRGUniversityMgtApp.Domain.Models.EducationFormatModel;
using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationFormatViewModel
{
   public class EducationFormatViewModel
    {
       public IEnumerable<EducationFormat> EducationFormatResults { get; set; }
       public bool IsHCRGAdmin { get; set; }
    }
}

