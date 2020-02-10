using HCRGUniversityMgtApp.Domain.Models.EducationFormatDetailModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationFormatDetailViewModel
{
    public class EducationFormatDetailViewModel
    {
        public IEnumerable<EducationFormatDetail> EducationFormatDetailResults { get; set; }
    }
}
