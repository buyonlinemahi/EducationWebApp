using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.ProfessionModel
{
    public class PagedProfession
    {
        public IEnumerable<Profession> Professions { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
