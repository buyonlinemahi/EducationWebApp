using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Security.Cryptography;
using System.Text;

namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices
{
    public class EncryptionService : IEncryption
    {
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }

        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        public string GenratePassword(int length)
        {
            string allowedLetterChars = GlobalConst.Message.allowedLetterChars;
            string allowedNumberChars = GlobalConst.Message.allowedNumberChars;
            char[] chars = new char[length];
            Random rd = new Random();

            bool useLetter = true;
            for (int i = 0; i < length; i++)
            {
                if (useLetter)
                {
                    chars[i] = allowedLetterChars[rd.Next(0, allowedLetterChars.Length)];
                    useLetter = false;
                }
                else
                {
                    chars[i] = allowedNumberChars[rd.Next(0, allowedNumberChars.Length)];
                    useLetter = true;
                }

            }
            return new string(chars);
        }
      

       
    }
}
