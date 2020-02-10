using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.Models.ProductModel
{
   public class PagedProduct
    {
        public IEnumerable<Product> Products { get; set; }
        public int TotalCount { get; set; }
    }
}
