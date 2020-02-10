using HCRGUniversityMgtApp.Domain.Models.NewsModel;
using HCRGUniversityMgtApp.Domain.Models.NewsSectionModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.NewsViewModel
{
   public  class NewsViewModel
    {
       public IEnumerable<News> NewsResults { get; set; }
       public IEnumerable<NewsSection> NewsSectionResults { get; set; }
       public PagedNews PagedNewsResults { get; set; }
       public bool IsHCRGAdmin { get; set; }
    }
}
