using HCRGUniversityApp.Domain.Models.NewsModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.NewsDetailViewModel
{
    public class NewsDetailFullViewModel
    {
        public IEnumerable<NewsFullDetail> NewsFullDetailRecords { get; set; }
        public int TotalCount { get; set; }
    }
}
