using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.Product
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
