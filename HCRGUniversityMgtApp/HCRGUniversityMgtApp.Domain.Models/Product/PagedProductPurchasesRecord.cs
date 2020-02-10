using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Product
{
   public class PagedProductPurchasesRecord
    {
        public int TotalCount { get; set; }
        
        public IEnumerable<ProductModel.ProductPurchase> ProductPurchasesRecords { get; set; }
    }
}
