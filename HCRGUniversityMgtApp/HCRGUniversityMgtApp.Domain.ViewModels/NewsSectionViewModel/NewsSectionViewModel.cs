using HCRGUniversityMgtApp.Domain.Models.NewsSectionModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.NewsSectionViewModel
{
   public class NewsSectionViewModel
    {
       public IEnumerable<NewsSection> NewsSectionResults { get; set; }
    }
}
