using System;

namespace HCRGUniversityMgtApp.Domain.Models.ProductModel
{
    public class ProductQuantity
    {
        public int ProductQuantityID { get; set; }
        public int ProductID { get; set; }
        public int ProdQuantity { get; set; }
        public DateTime ProdQuantityDate { get; set; }
        public int UserID { get; set; }
        public string Mode { get; set; }
    }
}
