using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.Models.Product
{
    public class PagedProductPurchase
    {
        public IEnumerable<ProductPurchase> ProductPurchaseDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
