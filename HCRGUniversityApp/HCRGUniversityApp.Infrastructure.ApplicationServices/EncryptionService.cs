using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
namespace HCRGUniversityApp.Infrastructure.ApplicationServices
{
    public class EncryptionService : IEncryption
    {
        string _key = "qwertyuiopasdfghjklzxcvbnm1234567890";
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }
        public bool VerifyHashedPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public string EncryptString(string plainText)
        {
            System.Text.Encoding encoding = System.Text.Encoding.Unicode;
            Byte[] stringBytes = encoding.GetBytes(plainText);
            StringBuilder sbBytes = new StringBuilder(stringBytes.Length * 2);
            foreach (byte b in stringBytes)
            {
                sbBytes.AppendFormat("{0:X2}", b);
            }
            return sbBytes.ToString();
        }

        public string DecryptString(string cipherText)
        {
            System.Text.Encoding encoding = System.Text.Encoding.Unicode;
            int numberChars = cipherText.Length;
            byte[] bytes = new byte[numberChars / 2];
            for (int i = 0; i < numberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(cipherText.Substring(i, 2), 16);
            }
            return encoding.GetString(bytes);
        }

        public string EncryptString2(string strToEncrypt)
        {
            string _sessionID = HttpContext.Current.Session.SessionID;
            string concatenateEncryptString = strToEncrypt + "," + _sessionID;
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = ASCIIEncoding.ASCII.GetBytes(concatenateEncryptString);
            return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
            TransformFinalBlock(byteBuff, 0, byteBuff.Length)).Replace("/", "!").Replace("+", "]");
        }

        public string DecryptString2(string strEncrypted)
        {
            strEncrypted = strEncrypted.Replace("!", "/").Replace("]", "+");
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(strEncrypted);
            string strDecrypted = ASCIIEncoding.ASCII.GetString
            (objDESCrypto.CreateDecryptor().TransformFinalBlock
            (byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;
            var _onlyDecryptedId = strDecrypted.Split(',');
            if(_onlyDecryptedId.Length==4)
                return strDecrypted;
            else
                return _onlyDecryptedId[0];
        }
    }
}