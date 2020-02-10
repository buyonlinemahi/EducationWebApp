using HCRGUniversityMgtApp.Domain.Models.NewsModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.NewsViewModel
{
    public class NewsSearchCarouselViewModel
    {
        public IEnumerable<NewsSearchCarousel> NewsSearchCarouselResult { get; set; }
    }
}
