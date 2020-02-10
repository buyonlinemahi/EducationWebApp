using HCRGUniversityApp.Domain.Models.FAQModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.FAQViewModel
{
  public  class FAQViewModel
    {
      public IEnumerable<FAQ> FAQDetailResults { get; set; }
      public IEnumerable<FAQCategory> FAQCategoryDetaildResults { get; set; }
    }
}
