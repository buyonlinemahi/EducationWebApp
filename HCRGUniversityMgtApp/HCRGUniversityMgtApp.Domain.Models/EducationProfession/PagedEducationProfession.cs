using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.Models.EducationProfessionDetailModel
{
    public class PagedEducationProfession
    {
        public IEnumerable<EducationProfessionDetail> EducationProfessions { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
