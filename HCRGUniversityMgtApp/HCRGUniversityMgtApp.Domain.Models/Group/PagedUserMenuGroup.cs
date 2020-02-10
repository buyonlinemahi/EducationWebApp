using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.Models.Group
{
    public class PagedUserMenuGroup
    {
        public IEnumerable<UserMenuGroup> UserMenuGroupDetails { get; set; }
        public int TotalCount { get; set; }
    }
}
