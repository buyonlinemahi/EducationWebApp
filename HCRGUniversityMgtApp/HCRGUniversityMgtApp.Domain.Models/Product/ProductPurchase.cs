using System;

namespace HCRGUniversityMgtApp.Domain.Models.ProductModel
{
  public class ProductPurchase
    {
        public int ProductID { get; set; }        
        public string ProductName { get; set; }        
        public string ProductDesc { get; set; }        
        public decimal ProductPrice { get; set; }        
        public string ProductImage { get; set; }        
        public string ProductFile { get; set; }        
        public string ProductType { get; set; }        
        public DateTime Date { get; set; }
        public int? ProductTotalQuantity { get; set; }
        public int? ProductCurrentBalance { get; set; }
        public string UserFirstname { get; set; }
        public string UserLastname { get; set; }
        public int UserID { get; set; }
        public int ProductShoppingID { get; set; }       
        public int? ShippingPaymentID { get; set; }

    }
}
