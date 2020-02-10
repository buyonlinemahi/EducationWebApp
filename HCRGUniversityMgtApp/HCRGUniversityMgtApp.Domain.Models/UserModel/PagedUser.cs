
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.UserModel
{
    public class PagedUser
    {
        public IEnumerable<User> Users { get; set; }
       
        public int TotalCount { get; set; }
        public int PagedRecords { get; set; }
    }
}
