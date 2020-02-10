using System;

namespace HCRGUniversityMgtApp.Domain.Models.Product
{
   public class ProductShippingDetail
    {
        public int ProductShippingDetailID { get; set; }
        public DateTime ProductShippedOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public int ProductShoppingID { get; set; }
        public int? ShippingPaymentID { get; set; }
        public string Message { get; set; }
    }  
}
