using HCRGUniversityApp.Domain.Models.NewsModel;
using HCRGUniversityApp.Domain.Models.NewsSectionModel;
using HCRGUniversityApp.Domain.Models.ResourceModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.NewsDetailViewModel
{
    public class NewsDetailViewModel
    {
        public IEnumerable<NewsSection> NewsSectionResults { get; set; }
        public IEnumerable<NewsFullDetail> NewsResults { get; set; }
        public NewsLetter newsLetter { get; set; }
        public int SectionID { get; set; }
        public int TotalCount { get; set; }
    }
}
