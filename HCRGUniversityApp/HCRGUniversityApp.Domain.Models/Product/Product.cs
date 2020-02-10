
namespace HCRGUniversityApp.Domain.Models.ProductModel
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductFile { get; set; }
        public string ProductType { get; set; }


        public int? ProductTotalQuantity { get; set; }
        public int? ProductCurrentBalance { get; set; }  
  
    }
}
