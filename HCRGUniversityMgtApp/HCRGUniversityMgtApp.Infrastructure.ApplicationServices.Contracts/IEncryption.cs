
namespace HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts
{
   public interface IEncryption
    {
        string HashPassword(string password);

        /// <summary>
        /// Verifies the current plain text password with the hashed password provided
        /// </summary>
        /// <param name="password">plain text password to verify</param>
        /// <param name="hashedPassword">hashed password</param>
        /// <returns></returns>
        bool VerifyHashedPassword(string password, string hashedPassword);

        string GenratePassword(int length);

    }
}
