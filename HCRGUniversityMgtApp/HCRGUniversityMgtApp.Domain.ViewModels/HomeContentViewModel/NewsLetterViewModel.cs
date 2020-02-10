using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.NewsLetterModel;

namespace HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel
{
    public class NewsLetterViewModel
    {
        public IEnumerable<NewsLetter> NewsLetterResutls { get; set; }
        public bool IsHCRGAdmin { get; set; }
    }
}
