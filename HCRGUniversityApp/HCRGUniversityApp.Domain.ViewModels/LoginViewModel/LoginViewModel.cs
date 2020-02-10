﻿using HCRGUniversityApp.Domain.Models.UserModel;

namespace HCRGUniversityApp.Domain.ViewModels.LoginViewModel
{
   public  class LoginViewModel
    {
        public User User { get; set; }
        public int FailedAttemptCount { get; set; }
        public string InvalidMsg { get; set; }
        public string PreviousLog { get; set; }
    }
}