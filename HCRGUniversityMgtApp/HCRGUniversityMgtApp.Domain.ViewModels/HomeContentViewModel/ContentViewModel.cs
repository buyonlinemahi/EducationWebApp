using System.Collections.Generic;
using HCRGUniversityMgtApp.Domain.Models.HomeContentModel;
namespace HCRGUniversityMgtApp.Domain.ViewModels.HomeContentViewModel
{
    public class ContentViewModel
    {
          public IEnumerable<HomeContent> HomeContentResults { get; set; }
           public bool IsHCRGAdmin { get; set; }
    }
}
