using HCRGUniversityApp.Domain.Models.NewsModel;
using HCRGUniversityApp.Domain.Models.NewsSectionModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.NewsDetailViewModel
{
    public class NewsFullDetailViewModel
    {
        public IEnumerable<NewsSection> NewsSectionResults { get; set; }
        public IEnumerable<NewsFullDetail> NewsResults { get; set; }
    }
}
