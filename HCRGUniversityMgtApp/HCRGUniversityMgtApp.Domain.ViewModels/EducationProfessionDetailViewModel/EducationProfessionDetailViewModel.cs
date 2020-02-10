using HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel;
using System.Collections.Generic;


namespace HCRGUniversityMgtApp.Domain.ViewModels.EducationProfessionDetailViewModel
{
    public class EducationProfessionDetailViewModel
    {
        public IEnumerable<EducationProfessionDetail> EducationProfessionDetailResults { get; set; }
    }
}
