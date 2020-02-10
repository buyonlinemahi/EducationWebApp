using System.Web;

namespace HCRGUniversityMgtApp.Domain.Models.ProductModel
{
   public class Product : Base.BaseColumn
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImage { get; set; }
        public string ProductFile { get; set; }
        public string ProductType { get; set; }
        public HttpPostedFileBase ProductImageFile { get; set; }
        public HttpPostedFileBase ProductFile_File { get; set; }
        public string Message { get; set; }
        public int? ProductTotalQuantity { get; set; }
        public int? ProductCurrentBalance { get; set; }
        public int? ProductPaidCount { get; set; } 
    }

   public class Product1
   {
       public int ProductID { get; set; }
       public string ProductName { get; set; }
       public string ProductDesc { get; set; }
       public decimal ProductPrice { get; set; }
       public string ProductImage { get; set; }
       public string ProductFile { get; set; }
       public string ProductType { get; set; }     
       public string Message { get; set; }
       public int? ProductTotalQuantity { get; set; }
       public int? ProductCurrentBalance { get; set; } 
   }
    
}
