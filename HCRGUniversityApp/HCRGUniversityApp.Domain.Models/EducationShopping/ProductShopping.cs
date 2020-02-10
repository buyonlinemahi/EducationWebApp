using System;

namespace HCRGUniversityApp.Domain.Models.EducationShoppingModel
{
 public  class ProductShopping
    {
        public int ProductShoppingID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int? CoupanID { get; set; }
        public decimal Grandtotal { get; set; }
        public int OrderID { get; set; }
        public DateTime Date { get; set; }
    }
}
