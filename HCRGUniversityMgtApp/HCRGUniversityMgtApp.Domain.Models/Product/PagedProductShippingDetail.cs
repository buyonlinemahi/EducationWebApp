using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Product
{
    public class PagedProductShippingDetail
    {
       
        public int TotalCount { get; set; }      
        public IEnumerable<ProductShippingDetail> ProductShippingDetails { get; set; }
    }
}
