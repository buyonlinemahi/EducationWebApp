using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.ProductModel
{
  public class PagedProduct : Base.BaseColumn
    {
        public IEnumerable<Product> Products { get; set; }    
        public int PagedRecords { get; set; }
        public int TotalCount { get; set; }
    }
}
