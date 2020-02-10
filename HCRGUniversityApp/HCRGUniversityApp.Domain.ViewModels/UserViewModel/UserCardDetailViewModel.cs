using HCRGUniversityApp.Domain.Models.UserModel;
using System.Collections.Generic;

namespace HCRGUniversityApp.Domain.ViewModels.UserViewModel
{
    public class UserCardDetailViewModel
    {
        public IEnumerable<UserCardDetail> UserCardDetailResults { get; set; }
    }
}
