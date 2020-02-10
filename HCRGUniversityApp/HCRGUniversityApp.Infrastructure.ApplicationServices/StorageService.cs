using HCRGUniversityApp.Infrastructure.ApplicationServices.Contracts;
using System;
using System.DirectoryServices;
using System.IO;
using System.Web;

namespace HCRGUniversityApp.Infrastructure.ApplicationServices
{
    public class StorageService : IStorage
    {
        public const string CollegeLogo = "CollegeLogo";
        public const string Resume = "Resume";
        public const string Certificate = "Certificate";
        public const string CoursePDF = "CoursePDF";
        public const string LogoImage = "LogoImage";

        public string GetUserInfoStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, CollegeLogo);
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }

        public string GetCoursePDFStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(getVirtualPath(), CoursePDF);            
            path = Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
            //if (Directory.Exists(path))
            return getVirtualPath();
            //else
              //  return "abc";
        }

        public string SetUserInfoStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, CollegeLogo);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }
        public string SetResumeUploadStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, Resume);
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }

        public string SetCertificateStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, Certificate);
            CreateDirectory(path);
            return Path.Combine(path, fileName);
        }
        
        private static bool CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return true;
            }
            return false;
        }

        public string SetOrganizationStoragePath(string storagePath, string fileName)
        {
            string path = Path.Combine(storagePath, LogoImage);
            CreateDirectory(path);
            return Path.Combine(path, Guid.NewGuid().ToString() + Path.GetExtension(fileName));
        }

        public string getVirtualPath()
        {
            DirectoryEntry w3svc = new DirectoryEntry("IIS://localhost/w3svc");
            HttpRequest httprequest = System.Web.HttpContext.Current.Request;
            DirectoryEntry entry = new DirectoryEntry("IIS://localhost/W3SVC/" + httprequest.ServerVariables["INSTANCE_ID"].ToString().Trim() + "/Root/Storage");
            return entry.Properties["Path"].Value.ToString().Trim();
            //return "E:\\TFS\\Education\\Web\\HCRGUniversityMgtApp\\HCRGUniversityMgtApp\\Storage";

           //return "D:\\TFS_TR\\HCRG University\\Web\\HCRGUniversityApp\\HCRGUniversityApp\\Storage";
            //return "E:\\Mahi_TFS\\HCRG University\\Web\\HCRGUniversityApp\\HCRGUniversityApp\\Storage";
        }

    }
}
