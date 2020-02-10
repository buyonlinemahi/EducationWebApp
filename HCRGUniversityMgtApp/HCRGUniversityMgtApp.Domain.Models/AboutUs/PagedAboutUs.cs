using System.Collections.Generic;
namespace HCRGUniversityMgtApp.Domain.Models.AboutUsModel
{
    public class PagedAboutUs
    {
        public IEnumerable<AboutUs> AboutUsRecords { get; set; }
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
