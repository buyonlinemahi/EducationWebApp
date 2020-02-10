using HCRGUniversityMgtApp.Domain.Models.UserModel;
using System.Collections.Generic;

namespace HCRGUniversityMgtApp.Domain.ViewModels.UserViewModel
{
    public class UserViewModel
    {
        public IEnumerable<User> UserResults { get; set; }
    }
}
