using System;

namespace HCRGUniversityApp.Domain.Models.EducationShoppingTempModel
{
  public class ProductShoppingTemp
    {
        public int ProductShoppingTempID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int? CoupanID { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public int? ShippingPaymentID { get; set; }
        public bool? IsProcessed { get; set; }
        public decimal? TaxPercentage { get; set; }
    }
}
