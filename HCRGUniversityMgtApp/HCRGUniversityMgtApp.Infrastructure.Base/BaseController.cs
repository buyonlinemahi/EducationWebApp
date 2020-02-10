using HCRGUniversityMgtApp.Domain.Models.Client;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices;
using HCRGUniversityMgtApp.Infrastructure.ApplicationServices.Contracts;
using HCRGUniversityMgtApp.Infrastructure.Global;
using System;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace HCRGUniversityMgtApp.Infrastructure.Base
{
    public abstract class BaseController : Controller
    {
        private IStorage _storageService;
        public Client HCRGCLIENT
        {
            get { return (Client)Session[GlobalConst.SessionKeys.CLIENT]; }
            set { Session[GlobalConst.SessionKeys.CLIENT] = value; }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
        }

        string _key = "qwertyuiopasdfghjklzxcvbnm1234567890";
        public string EncryptString(string encryptString)
        {
            string _sessionID = "";
            if (HCRGCLIENT != null)
                _sessionID = HCRGCLIENT.ClientID + "," + "qwerty12345asdfgh67890";
            string concatenateEncryptString = encryptString + "," + _sessionID;
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

        public string DecryptString(string cipherText)
        {
            cipherText = cipherText.Replace("!", "/").Replace("]", "+");
            TripleDESCryptoServiceProvider objDESCrypto = new TripleDESCryptoServiceProvider();
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash, byteBuff;
            string strTempKey = _key;
            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey));
            objHashMD5 = null;
            objDESCrypto.Key = byteHash;
            objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(cipherText);
            string strDecrypted = ASCIIEncoding.ASCII.GetString
            (objDESCrypto.CreateDecryptor().TransformFinalBlock
            (byteBuff, 0, byteBuff.Length));
            objDESCrypto = null;
            string _sessionID = "";
            if (HCRGCLIENT != null)
                _sessionID = HCRGCLIENT.ClientID.ToString();
            var _onlyDecryptedId = strDecrypted.Split(',');

            if (_onlyDecryptedId[1].ToString() == _sessionID)
            {
                return _onlyDecryptedId[0];
            }
            else
            {
                return "0";
            }
        }
    }
}
