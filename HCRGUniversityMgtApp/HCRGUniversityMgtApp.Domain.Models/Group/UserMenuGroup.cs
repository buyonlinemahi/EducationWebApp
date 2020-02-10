
namespace HCRGUniversityMgtApp.Domain.Models.Group
{
    public class UserMenuGroup : Base.BaseColumn
     {
        public int UserMenuGroupID { get; set; }
        public string UserMenuGroupName { get; set; }
        public string EncryptedUserMenuGroupID { get; set; }
    }
}
