using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCRGUniversityApp.Domain.Models.UserModel
{
    public class UserCardDetail
    {
        public int UserCardDetailID { get; set; }
        public int UserID { get; set; }
        public string UserCardName { get; set; }
        public string UserCardNumber { get; set; }
        public string UserCardExpiry { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? DeletedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
