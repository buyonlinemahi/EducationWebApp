using System;

namespace HCRGUniversityApp.Domain.Models.Product
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
    }
}
