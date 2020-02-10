using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.ProductModel
{
    public class PagedProductPurchase
    {
     
        public int TotalCount { get; set; }
        public IEnumerable<ProductPurchase> ProductPurchaseDetails { get; set; }
    }
}
