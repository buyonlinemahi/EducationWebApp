using HCRGUniversityApp.Domain.Models.EducationFormatModel;
using System.Collections.Generic;
namespace HCRGUniversityApp.Domain.ViewModels.EducationFormatViewModel
{
   public class EducationFormatViewModel
    {
       public IEnumerable<EducationFormat> EducationFormatResults { get; set; }
    }
}

