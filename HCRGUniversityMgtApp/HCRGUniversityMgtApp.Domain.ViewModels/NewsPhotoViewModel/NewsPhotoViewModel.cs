using HCRGUniversityMgtApp.Domain.Models.NewsPhotoModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.NewsPhotoViewModel
{
   public class NewsPhotoViewModel
    {
       public IEnumerable<NewsPhoto> NewsPhotoResults { get; set; }
    }
}
